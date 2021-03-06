﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NuGet.ServerExtensions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NuGetResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NuGetResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NuGet.ServerExtensions.NuGetResources", typeof(NuGetResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; already present on &apos;{1}&apos;..
        /// </summary>
        internal static string Log_PackageAlreadyPresent {
            get {
                return ResourceManager.GetString("Log_PackageAlreadyPresent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Successfully mirrored &apos;{0}&apos; to &apos;{1}&apos;..
        /// </summary>
        internal static string Log_PackageMirroredSuccessfully {
            get {
                return ResourceManager.GetString("Log_PackageMirroredSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The API key for pushing to the target repository. If not specified, the one specified in the default NuGet config file is used..
        /// </summary>
        internal static string MirrorCommandApiKey {
            get {
                return ResourceManager.GetString("MirrorCommandApiKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mirrors a package and its dependencies from the specified source repositories to the target repository. .
        /// </summary>
        internal static string MirrorCommandDescription {
            get {
                return ResourceManager.GetString("MirrorCommandDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No packages found to check for mirroring..
        /// </summary>
        internal static string MirrorCommandDidNothing {
            get {
                return ResourceManager.GetString("MirrorCommandDidNothing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to By default a local cache is used as a fallback when a package or a package dependency is not found in the specified source(s). If you want to ensure only packages from the specified sources are used, set the NoCache option. If you want instead to maximize chances of finding packages, do not set this option..
        /// </summary>
        internal static string MirrorCommandNoCache {
            get {
                return ResourceManager.GetString("MirrorCommandNoCache", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Log what would be done without actually doing it. Assumes success for push operations. .
        /// </summary>
        internal static string MirrorCommandNoOp {
            get {
                return ResourceManager.GetString("MirrorCommandNoOp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Version should be specified in packages.config file instead..
        /// </summary>
        internal static string MirrorCommandNoVersionIfPackagesConfig {
            get {
                return ResourceManager.GetString("MirrorCommandNoVersionIfPackagesConfig", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your package was pushed..
        /// </summary>
        internal static string MirrorCommandPackagePushed {
            get {
                return ResourceManager.GetString("MirrorCommandPackagePushed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When set, &quot;latest&quot; when specifying no version for a package id (as command argument or in packages.config) includes pre-release packages..
        /// </summary>
        internal static string MirrorCommandPrerelease {
            get {
                return ResourceManager.GetString("MirrorCommandPrerelease", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pushing {0} to {1}....
        /// </summary>
        internal static string MirrorCommandPushingPackage {
            get {
                return ResourceManager.GetString("MirrorCommandPushingPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A list of packages sources to use for the finding packages to mirror. If no sources are specified, the ones defined in the default NuGet config file are used. If the default NuGet config file specifies no sources, uses the default NuGet feed..
        /// </summary>
        internal static string MirrorCommandSourceDescription {
            get {
                return ResourceManager.GetString("MirrorCommandSourceDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specifies the timeout for pushing to the target in seconds. Defaults to 300 seconds (5 minutes)..
        /// </summary>
        internal static string MirrorCommandTimeoutDescription {
            get {
                return ResourceManager.GetString("MirrorCommandTimeoutDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specify the id of the package to mirror, the url to query the target repository (list command) and the url to push packages to the target repository. If a path to a packages.config file is used instead of a package id, all the packages it contains are mirrored to the given version (if specified) or latest otherwise. Assuming you&apos;re targeting a private repository under http://machine/repo installed using NuGet.Server, the list and push urls will be http://machine/repo/nuget and http://machine/repo/api/v2/pac [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MirrorCommandUsageDescription {
            get {
                return ResourceManager.GetString("MirrorCommandUsageDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NuGet mirror Microsoft.AspNet.Mvc http://server/dir/nuget http://server/dir/api/v2/package 
        ///
        ///NuGet mirror Microsoft.AspNet.Razor http://server/dir/nuget http://server/dir/api/v2/package -ApiKey 4003d786-cc37-4004-bfdf-c4f3e8ef9b3a -version 1.0.20105.408.
        /// </summary>
        internal static string MirrorCommandUsageExamples {
            get {
                return ResourceManager.GetString("MirrorCommandUsageExamples", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to packageId|pathToPackagesConfig listUrlTarget publishUrlTarget [options].
        /// </summary>
        internal static string MirrorCommandUsageSummary {
            get {
                return ResourceManager.GetString("MirrorCommandUsageSummary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The version of the package to install. If not specified, latest version is mirrored..
        /// </summary>
        internal static string MirrorCommandVersionDescription {
            get {
                return ResourceManager.GetString("MirrorCommandVersionDescription", resourceCulture);
            }
        }
    }
}
