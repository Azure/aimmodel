using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that enriches a message with new information.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ContentEnricher : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ContentEnricher"/> class.
        /// </summary>
        public ContentEnricher()
            : base(MessageProcessorType.ContentEnricher)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ContentEnricher"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the content enricher.</param>
        public ContentEnricher(string name)
            : base(name, MessageProcessorType.ContentEnricher)
        {
        }

        /// <summary>
        /// Gets or sets a reference to the map that this content enricher uses to transform messages.
        /// </summary>
        public string MapKeyRef { get; set; }
    }
}
