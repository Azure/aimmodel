using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents a message that is sent to or received from the message bus.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A message is a data structure that is sent to or received from a message bus via
    /// a channel.  The message is typically in a structured form such as XML or JSON and
    /// often has a schema associated with it in order to allow the message to be validated
    /// and transformed.
    /// </para>
    /// <para>
    /// Messages can come in various flavours depending on their use.  Whilst technically
    /// they are all messages, their use can sometimes impose requirements on their structure.
    /// For example, an envelope message will have a body for a document message, but often
    /// also includes a header too.
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class Message : MessagingObject
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Message"/> class.
        /// </summary>
        protected Message()
            : this(MessageType.Document)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Message"/> class with the type of the message.
        /// </summary>
        /// <param name="messageType">The type of the message.</param>
        protected Message(MessageType messageType)
            : base(MessagingObjectType.Message)
        {
            MessageType = messageType;
            ContentType = MessageContentType.Json;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Message"/> class with the type of the message and its content type.
        /// </summary>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="contentType">The content type of the message.</param>
        protected Message(MessageType messageType, MessageContentType contentType)
            : base(MessagingObjectType.Message)
        {
            MessageType = messageType;
            ContentType = contentType;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Message"/> class with the name and type of the message and its content type.
        /// </summary>
        /// <param name="name">The name of the message.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="contentType">The content type of the message.</param>
        protected Message(string name, MessageType messageType, MessageContentType contentType)
            : base(name, MessagingObjectType.Message)
        {
            MessageType = messageType;
            ContentType = contentType;
        }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Gets or sets the content type of the message.
        /// </summary>
        public MessageContentType ContentType { get; set; }

        /// <summary>
        /// Gets or sets the schema of the message.
        /// </summary>
        public MessageSchema MessageSchema { get; set; }

        /// <summary>
        /// Gets the routing properties.
        /// </summary>
        public IDictionary<string, string> RoutingProperties { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the message transforms.
        /// </summary>
        public IList<MessageTransform> MessageTransforms { get; } = new List<MessageTransform>();
    }
}
