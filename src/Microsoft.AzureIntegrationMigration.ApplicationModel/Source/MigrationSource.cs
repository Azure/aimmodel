using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Source
{
    /// <summary>
    /// Defines a class that represents the migration source.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MigrationSource
    {
        /// <summary>
        /// Gets a list of resource containers associated with the source application.
        /// </summary>
        /// <remarks>
        /// Resource containers are unpacked in the 'Discover' stage to find resource
        /// definitions.
        /// </remarks>
        public IList<ResourceContainer> ResourceContainers { get; } = new List<ResourceContainer>();

        /// <summary>
        /// Gets or sets an arbitratry object representing the object model for the parsed resource definitions.
        /// </summary>
        /// <remarks>
        /// The 'Parse' stage expands the resource definitions into a migration source object model that is used
        /// by stage runners in the 'Analyze' stage to build the migration target object model
        /// </remarks>
        public object MigrationSourceModel { get; set; }
    }
}
