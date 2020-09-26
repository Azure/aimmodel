using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Represents a resource snippet whose files will be created as part of conversion.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class TargetResourceSnippet
    {
        /// <summary>
        /// Gets or sets the resource key from the snippet configuration for this resource.
        /// </summary>
        public string SnippetKey { get; set; }

        /// <summary>
        /// Gets or sets the resource type from the snippet configuration for this resource.
        /// </summary>
        public string SnippetType { get; set; }

        /// <summary>
        /// Gets or sets the name of the snippet resource.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the type of the snippet resource.
        /// </summary>
        /// <remarks>
        /// The type can be used to identify the type of the snippet which is used to select
        /// the snippets when generating multiple snippets into a single resource template.
        /// </remarks>
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets a dictionary of parameters and their values used for snippet generation.
        /// </summary>
        public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the output path from the snippet configuration for this resource.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the snippet file associated with this resource in the snippet configuration.
        /// </summary>
        public string ResourceSnippetFile { get; set; }
    }
}
