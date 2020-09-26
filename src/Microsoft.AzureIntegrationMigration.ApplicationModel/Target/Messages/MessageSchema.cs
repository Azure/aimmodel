using System;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents a schema for a message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageSchema
    {
        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a reference to the schema resource.
        /// </summary>
        public string ResourceKeyRef { get; set; }
    }
}
