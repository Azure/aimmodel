using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Report;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Source
{
    /// <summary>
    /// Defines a base resource node in the resource container hierarchy.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class ResourceNode : ReportAnnotation
    {
        /// <summary>
        /// Constructs a new instance of the <see cref="ResourceNode"/> class.
        /// </summary>
        public ResourceNode() : base()
        {
            RefId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets a unique ID representing this node.
        /// </summary>
        /// <remarks>
        /// This reference is used for report navigation.
        /// </remarks>
        public string RefId { get; set; }

        /// <summary>
        /// Gets or sets the parent of this node.
        /// </summary>
        public string ParentRefId { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the unique key for the resource.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the resource.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A pointer to the source object that the resource was created from.
        /// </summary>
        public object SourceObject { get; set; }
    }
}
