using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Source
{
    /// <summary>
    /// Defines a definition for a resource.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ResourceDefinition : ResourceNode
    {
        /// <summary>
        /// Gets or sets the content of the resource definition.
        /// </summary>
        public object ResourceContent { get; set; }

        /// <summary>
        /// Gets a list of resources.
        /// </summary>
        /// <remarks>
        /// Resources are found in the 'Parse' stage by parsing and expanding on the resource definition
        /// that these resources are related to.
        /// </remarks>
        public IList<ResourceItem> Resources { get; } = new List<ResourceItem>();
    }
}
