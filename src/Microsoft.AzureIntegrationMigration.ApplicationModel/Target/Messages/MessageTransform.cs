using System;
using System.Collections.Generic;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents a transform for a message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageTransform
    {
        /// <summary>
        /// Gets or sets the name of the transform.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a reference to the transform resource.
        /// </summary>
        public string ResourceKeyRef { get; set; }

        /// <summary>
        /// Gets the list of references to source schemas for the transform.
        /// </summary>
        public IList<string> SourceSchemaKeyRefs { get; } = new List<string>();

        /// <summary>
        /// Gets the list of references to target schemas for the transform.
        /// </summary>
        public IList<string> TargetSchemaKeyRefs { get; } = new List<string>();

        /// <summary>
        // Gets a dictionary of arbitrary transform properties and their values used for template generation.
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
    }
}
