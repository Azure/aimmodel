using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that processes a message from a channel and outputs
    /// the same or a different message after having performed some processing, such as
    /// validation of the content of the message or decryption.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class MessageProcessor : Intermediary
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageProcessor"/> class.
        /// </summary>
        protected MessageProcessor()
            : this(MessageProcessorType.GenericFilter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageProcessor"/> class with its type.
        /// </summary>
        /// <param name="messageProcessorType">The type of the message processor.</param>
        protected MessageProcessor(MessageProcessorType messageProcessorType)
            : base(IntermediaryType.MessageProcessor)
        {
            MessageProcessorType = messageProcessorType;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageProcessor"/> class with a name and its type.
        /// </summary>
        /// <param name="name">The name of the message processor.</param>
        /// <param name="messageProcessorType">The type of the message processor.</param>
        protected MessageProcessor(string name, MessageProcessorType messageProcessorType)
            : base(name, IntermediaryType.MessageProcessor)
        {
            MessageProcessorType = messageProcessorType;
        }

        /// <summary>
        /// Gets or sets the type of the message processor.
        /// </summary>
        public MessageProcessorType MessageProcessorType { get; set; }
    }
}
