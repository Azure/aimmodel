// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents the message for the message bus that wraps all business messages published to the message bus
    /// and is unwrapped when business messages are consumed from the message bus.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All messages that traverse the message bus (representing the messaging system) are wrapped on entry and
    /// unwrapped on exit from the message bus.  The wrapped messages are the business messages that applications
    /// send and receive on the message bus.
    /// </para>
    /// <para>
    /// The message bus message itself is an envelope style of message.  It is designed to contain the original
    /// business message (or any subsequent transformations) plus additional state to allow the message bus objects
    /// (endpoints, intermediaries and channels) to route and process the message correctly.
    /// </para>
    /// <para>
    /// The additional state is held in separate blocks in the header of the message.  This additional state
    /// includes:
    /// <list type="bullet">
    /// <item>
    /// <term>Routing Slip</term>
    /// <description>The itinerary of intermediaries and endpoints and whether they have been visited.</description>
    /// </item>
    /// <item>
    /// <term>Routing Properties</term>
    /// <description>A dictionary of context properties that are used to route messages to recipients in a pub-sub channel.</description>
    /// </item>
    /// <item>
    /// <term>Message State</term>
    /// <description>A dictionary of arbitrary state that is used to aid processing the message as it traverses the bus.</description>
    /// </item>
    /// <item>
    /// <term>Message Properties</term>
    /// <description>A dictionary of arbitrary properties that are used to provide key identifiers for the message, such as trace-id, correlation-id and message-group-id.</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageBusMessage : Message
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageBusMessage"/> class.
        /// </summary>
        public MessageBusMessage()
            : base(MessageType.Envelope)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageBusMessage"/> class with the content type.
        /// </summary>
        /// <param name="contentType">The content type of the message.</param>
        public MessageBusMessage(MessageContentType contentType)
            : base(MessageType.Envelope, contentType)
        {
        }

        /// <summary>
        /// Gets or sets the header schema of the envelope message: Routing Slip,
        /// Routing Properties, Message State, Message Properties.
        /// </summary>
        public IList<MessageSchema> HeaderSchemas { get; } = new List<MessageSchema>();

        /// <summary>
        /// Gets or sets the schema for the body message.
        /// </summary>
        public MessageSchema BodySchema { get; set; }
    }
}
