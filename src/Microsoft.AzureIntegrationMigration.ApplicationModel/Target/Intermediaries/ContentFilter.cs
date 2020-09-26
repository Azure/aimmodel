using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that filters a message of unneeded content such as
    /// sanitising a message for downstream systems.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ContentFilter : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ContentFilter"/> class.
        /// </summary>
        public ContentFilter()
            : base(MessageProcessorType.ContentFilter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ContentFilter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the content filter.</param>
        public ContentFilter(string name)
            : base(name, MessageProcessorType.ContentFilter)
        {
        }

        /// <summary>
        /// Gets or sets a reference to the map that this content filter uses to strip messages
        /// of content.
        /// </summary>
        public string MapKeyRef { get; set; }
    }
}
