using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Represents a resource template whose files will be created as part of conversion.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class TargetResourceTemplate
    {
        /// <summary>
        /// Gets or sets the resource key from the template configuration for this resource.
        /// </summary>
        public string TemplateKey { get; set; }

        /// <summary>
        /// Gets or sets the resource type from the template configuration for this resource.
        /// </summary>
        public string TemplateType { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This name is used to name the resource in Azure.  Please be aware that some
        /// resource types in Azure require this name to be globally unique.
        /// </para>
        /// <para>
        /// Use the Cloud Adoption Framework as guidance for naming and tagging resources:
        /// https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging
        /// </para>
        /// </remarks>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <remarks>
        /// The type can be used to identify the type of the resources that wil be created by the template.
        /// </remarks>
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets a dictionary of tags that can be applied to a resource in Azure.
        /// </summary>
        public IDictionary<string, string> Tags { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets a dictionary of parameters and their values used for template generation.
        /// </summary>
        public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the output path from the template configuration for this resource.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets the list of files associated with this resource in the template configuration.
        /// </summary>
        public IList<string> ResourceTemplateFiles { get; } = new List<string>();
    }
}
