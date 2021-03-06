using System;
using System.IO;
using System.Linq;
using Lucene.Net.Index;
using Lucene.Net.Linq;
using Lucene.Net.Store;
using Moq;
using NuGet;
using NuGet.Server.Infrastructure.Lucene;
using Version = Lucene.Net.Util.Version;

namespace Server.Test.Lucene
{
    public abstract class TestBase
    {
        protected readonly Mock<ILucenePackageRepository> loader;
        protected readonly Mock<IPackagePathResolver> packagePathResolver;
        protected readonly Mock<IFileSystem> fileSystem;
        protected readonly LuceneDataProvider provider;
        protected readonly IQueryable<LucenePackage> datasource;
        protected readonly IndexWriter indexWriter;

        protected TestBase()
        {
            packagePathResolver = new Mock<IPackagePathResolver>();
            loader = new Mock<ILucenePackageRepository>(MockBehavior.Strict);
            fileSystem = new Mock<IFileSystem>();

            packagePathResolver.Setup(p => p.GetPackageDirectory(It.IsAny<IPackage>())).Returns("package-dir");
            packagePathResolver.Setup(p => p.GetPackageFileName(It.IsAny<IPackage>())).Returns((Func<IPackage, string>)(pkg => pkg.Id));

            var dir = new RAMDirectory();
            var analyzer = new PackageAnalyzer();
            indexWriter = new IndexWriter(dir, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            provider = new LuceneDataProvider(dir, analyzer, Version.LUCENE_30, indexWriter);
            datasource = provider.AsQueryable(() => new LucenePackage(fileSystem.Object));
        }

        protected LucenePackage MakeSamplePackage(string id, string version)
        {
            var p = new LucenePackage(fileSystem.Object, path => new MemoryStream())
                        {
                            Id = id,
                            Version = version != null ? new StrictSemanticVersion(version) : null,
                            DownloadCount = -1,
                            VersionDownloadCount = -1
                        };

            if (p.Id != null && version != null)
            {
                p.Path = Path.Combine(packagePathResolver.Object.GetPackageDirectory(p),
                                      packagePathResolver.Object.GetPackageFileName(p));
            }

            return p;
        }

        protected void InsertPackage(string id, string version)
        {
            var p = MakeSamplePackage(id, version);

            p.DownloadCount = 0;
            p.VersionDownloadCount = 0;

            InsertPackage(p);
        }

        protected void InsertPackage(LucenePackage p)
        {
            p.Path = Path.Combine(packagePathResolver.Object.GetPackageDirectory(p),
                                  packagePathResolver.Object.GetPackageFileName(p));

            using (var s = provider.OpenSession(() => new LucenePackage(fileSystem.Object)))
            {
                s.Add(p);
            }
        }
    }
}