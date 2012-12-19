﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;
using Ninject;
using NuGet.Server.DataServices;

namespace NuGet.Server.Infrastructure
{
    public class ServerPackageRepository : LocalPackageRepository, IServerPackageRepository
    {
        private readonly IDictionary<IPackage, DerivedPackageData> _derivedDataLookup = new Dictionary<IPackage, DerivedPackageData>(PackageEqualityComparer.IdAndVersion);
        private readonly ManualResetEvent _derivedDataComputed = new ManualResetEvent(false);
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private const int MaxWaitMs = 1000*60*2;

        public ServerPackageRepository(string path)
            : base(path)
        {
        }

        public ServerPackageRepository(IPackagePathResolver pathResolver, IFileSystem fileSystem)
            : base(pathResolver, fileSystem)
        {
        }

        [Inject]
        public IHashProvider HashProvider { get; set; }

        public IQueryable<Package> GetPackagesWithDerivedData()
        {
            return from package in base.GetPackages()
                   select GetMetadataPackage(package);
        }

        public override void AddPackage(IPackage package)
        {
            string fileName = GetPackageFilePath(package);
            using (Stream stream = package.GetStream())
            {
                FileSystem.AddFile(fileName, stream);
            }
        }

        public void RemovePackage(string packageId, SemanticVersion version)
        {
            IPackage package = FindPackage(packageId, version);
            if (package != null)
            {
                RemovePackage(package);
            }
        }

        public override void RemovePackage(IPackage package)
        {
            string fileName = GetPackageFilePath(package);
            FileSystem.DeleteFile(fileName);
            DeleteData(package);
        }

        public virtual void IncrementDownloadCount(IPackage package)
        {
            // not implemented.
        }

        protected override IPackage OpenPackage(string path)
        {
            IPackage package = base.OpenPackage(path);

            _cacheLock.EnterWriteLock();            
            try
            {                
                DateTime start = DateTime.Now;
                while (true)
                {
                    DerivedPackageData packageData;
                    if (!_derivedDataLookup.TryGetValue(package, out packageData))
                    {
                        // take ownership
                        _derivedDataLookup[package] = null;
                        break;
                    }
                    if (packageData != null)
                    {
                        // derived data has been computed and cached
                        return package;
                    }
                    if ((DateTime.Now - start).TotalSeconds > MaxWaitMs)
                    {
                        // we're giving up on waiting; potentially other thread is blocked, died, ... take ownership
                        _derivedDataLookup[package] = null;
                        break;
                    }
                    // about to wait; release locks
                    _cacheLock.ExitWriteLock();
                    _derivedDataComputed.WaitOne(MaxWaitMs);

                    _cacheLock.EnterWriteLock();
                }                                
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }

            try
            {
                // compute
                DerivedPackageData packageData = CalculateDerivedData(package, path);
                // write value
                SetData(package, packageData);
            }
            catch
            {
                // on failure, clean cache
                DeleteData(package);
                throw;
            }
            finally
            {
                // We've either failed or succeeded => wake up waiting threads, if any.
                _derivedDataComputed.Set();
            }
            
            return package;
        }

        private DerivedPackageData GetData(IPackage package)
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _derivedDataLookup[package];
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        private void SetData(IPackage package, DerivedPackageData packageData)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                _derivedDataLookup[package] = packageData;
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        private void DeleteData(IPackage package)
        {
            _cacheLock.EnterWriteLock();
            try
            {   // note: doesn't throw if not found in dict
                _derivedDataLookup.Remove(package);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public virtual Package GetMetadataPackage(IPackage package)
        {
            return new Package(package, GetData(package));
        }

        public virtual IQueryable<IPackage> Search(string searchTerm, IEnumerable<string> targetFrameworks, bool allowPrereleaseVersions)
        {
            var packages = GetPackages().Find(searchTerm)
                                        .FilterByPrerelease(allowPrereleaseVersions)
                                        .AsQueryable();

            // TODO: Enable this when we can make it faster
            //if (targetFrameworks.Any()) {
            //    // Get the list of framework names
            //    var frameworkNames = targetFrameworks.Select(frameworkName => VersionUtility.ParseFrameworkName(frameworkName));

            //    packages = packages.Where(package => frameworkNames.Any(frameworkName => IsCompatible(frameworkName, package)));
            //}

            return packages;
        }


        public IEnumerable<IPackage> GetUpdates(IEnumerable<IPackage> packages, bool includePrerelease, bool includeAllVersions, IEnumerable<FrameworkName> targetFramework)
        {
            return this.GetUpdatesCore(packages, includePrerelease, includeAllVersions, targetFramework);
        }

        protected virtual DerivedPackageData CalculateDerivedData(IPackage package, string path)
        {
            using (Stream stream = FileSystem.OpenFile(path))
            {
                return CalculateDerivedData(package, path, stream);
            }
        }

        protected virtual DerivedPackageData CalculateDerivedData(IPackage package, string path, Stream stream)
        {
            byte[] fileBytes = stream.ReadAllBytesAndDispose();

            return new DerivedPackageData
            {
                PackageSize = fileBytes.Length,
                PackageHash = Convert.ToBase64String(HashProvider.CalculateHash(fileBytes)),
                LastUpdated = FileSystem.GetLastModified(path),
                Published = FileSystem.GetLastModified(path),
                Created = GetZipArchiveCreateDate(new MemoryStream(fileBytes)),
                IsPrerelease = !package.IsReleaseVersion(),
                // TODO: Add support when we can make this faster
                // SupportedFrameworks = package.GetSupportedFrameworks(),
                Path = path,
                FullPath = FileSystem.GetFullPath(path)
            };
        }
        
        private DateTimeOffset GetZipArchiveCreateDate(Stream stream)
        {
            var f = new ZipFile(stream);
            foreach (ZipEntry file in f)
            {
                if (file.Name.EndsWith(".nuspec"))
                {
                    return file.DateTime;
                }
            }

            return DateTimeOffset.MinValue;
        }
    }
}
