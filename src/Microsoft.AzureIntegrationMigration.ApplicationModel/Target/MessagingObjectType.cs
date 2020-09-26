using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Defines an enumeration representing the basic building blocks of the messaging system.
    /// </summary>
    public enum MessagingObjectType
    {
        /// <summary>
        /// Defines a default value.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Defines a message bus which is the root object for a messaging system.
        /// </summary>
        MessageBus = 1,

        /// <summary>
        /// Defines an application that represents an integration solution that uses the message
        /// bus to send and receive messages between endpoints.
        /// </summary>
        Application = 2,

        /// <summary>
        /// Defines a message which represents a data structure that is sent and received on the
        /// message bus.
        /// </summary>
        Message = 3,

        /// <summary>
        /// Defines an endpoint that represents an application that connects to the message bus and
        /// provides the ability to send and receive from message channels using different strategies.
        /// </summary>
        Endpoint = 4,

        /// <summary>
        /// Defines a channel that acts as a conduit for messages that are sent and received on the
        /// message bus.  Channels provide different strategies for sending and receiving messages
        /// and can restrict the types of messages that are allowed to traverse the channel.  Channels
        /// connect endpoints and intermediaries.
        /// </summary>
        Channel = 5,

        /// <summary>
        /// Defines an intermediary, or processing step, that acts on messages as they flow between
        /// endpoints.  Intermediaries send and receive messages using channels.
        /// </summary>
        Intermediary = 6
    }
}
