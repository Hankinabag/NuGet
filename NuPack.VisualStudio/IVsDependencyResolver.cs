﻿using System;
using System.Collections.Generic;

namespace NuPack.VisualStudio {
    public interface IVsDependencyResolver {
        void Resolve(IPackage package);
        IEnumerable<PackageOperation> ProjectOperations { get; }
        IEnumerable<PackageOperation> SolutionOperations { get; }
        void AddSolutionOperation(PackageOperation operation);
        void AddProjectOperation(PackageOperation operation);
    }
}