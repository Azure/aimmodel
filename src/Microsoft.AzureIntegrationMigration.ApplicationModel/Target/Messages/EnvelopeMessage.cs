using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents an envelope style message containing .
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class EnvelopeMessage : Message
    {
        /// <summary>
        /// Constructs an instance of the <see cref="EnvelopeMessage"/> class.
        /// </summary>
        public EnvelopeMessage()
            : base(MessageType.Envelope)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="EnvelopeMessage"/> class with the content type.
        /// </summary>
        /// <param name="contentType">The content type of the message.</param>
        public EnvelopeMessage(MessageContentType contentType)
            : base(MessageType.Envelope, contentType)
        {
        }

        /// <summary>
        /// Gets a list of schemas for the header schema of the envelope message, if any.
        /// </summary>
        public IList<MessageSchema> HeaderSchemas { get; } = new List<MessageSchema>();

        /// <summary>
        /// Gets a list of schemas for the body schema of the message.
        /// </summary>
        public IList<MessageSchema> BodySchemas { get; } = new List<MessageSchema>();

        /// <summary>
        /// Gets a list of schemas for the envelope message, if any.
        /// </summary>
        public IList<MessageSchema> TrailerSchemas { get; } = new List<MessageSchema>();
    }
}
