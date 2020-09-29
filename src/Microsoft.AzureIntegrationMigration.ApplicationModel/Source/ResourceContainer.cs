// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Source
{
    /// <summary>
    /// Defines a container where one or more resource definitions can be found.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ResourceContainer : ResourceNode
    {
        /// <summary>
        /// Gets or sets the location of the container.
        /// </summary>
        public string ContainerLocation { get; set; }

        /// <summary>
        /// Gets a list of resource containers in the resource container associated with the source application.
        /// </summary>
        public IList<ResourceContainer> ResourceContainers { get; } = new List<ResourceContainer>();

        /// <summary>
        /// Gets a list of resource definitions in the resource container associated with the source application.
        /// </summary>
        /// <remarks>
        /// Resource definitions are found in the 'Discover' stage and are parsed and
        /// expanded on in the 'Parse' stage.
        /// </remarks>
        public IList<ResourceDefinition> ResourceDefinitions { get; } = new List<ResourceDefinition>();
    }
}
