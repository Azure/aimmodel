using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that transforms a message from one format to another.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageTranslator : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageTranslator"/> class.
        /// </summary>
        public MessageTranslator()
            : base(MessageProcessorType.Translator)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageTranslator"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the message translator.</param>
        public MessageTranslator(string name)
            : base(name, MessageProcessorType.Translator)
        {
        }

        /// <summary>
        /// Gets or sets references to the maps that this translator uses to transform messages.
        /// </summary>
        public IList<string> MapKeyRefs { get; } = new List<string>();
    }
}
