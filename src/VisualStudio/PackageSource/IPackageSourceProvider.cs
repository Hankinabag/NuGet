using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NuGet.VisualStudio {
    public interface IPackageSourceProvider {
        PackageSource ActivePackageSource { get; set; }
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = "This method is potentially expensive.")]
        IEnumerable<PackageSource> GetPackageSources();
        void AddPackageSource(PackageSource source);
        bool RemovePackageSource(PackageSource source);
        void SetPackageSources(IEnumerable<PackageSource> sources);
    }
}