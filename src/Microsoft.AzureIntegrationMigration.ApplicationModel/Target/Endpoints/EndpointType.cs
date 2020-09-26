using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Defines an enumeration of allowable values for the types of endpoints supported by
    /// the message bus.
    /// </summary>
    public enum EndpointType
    {
        /// <summary>
        /// Defines an endpoint that is represented by an adapter, which is an out-of-process component that
        /// is reponsible for providing the integration between an application and the messaging system.  An
        /// adapter would interface with an application using either a standard protocol such as HTTP or FTP
        /// or a specific application API.
        /// </summary>
        Adapter,

        /// <summary>
        /// Defines an endpoint that represents an application that is published as a service and is consumed
        /// by the messaging system, typically in a request-reply exchange pattern.
        /// </summary>
        ServiceActivator,

        /// <summary>
        /// Defines an endpoint that receives messages from the channel idempotently, that is, it handles
        /// duplicate messages without error and side effects.
        /// </summary>
        IdempotentReceiver,

        /// <summary>
        /// Defines an endpoint that receives messages by event, that is, the endpoint is triggered by an event
        /// that a message is available in a channel.
        /// </summary>
        EventDrivenConsumer,

        /// <summary>
        /// Defines an endpoint that receives messages using multiple parallel consumers that collectively
        /// compete with each other to receive messages from a channel.
        /// </summary>
        CompetingConsumer,

        /// <summary>
        /// Defines an endpoint that polls a channel for messages.
        /// </summary>
        PollingConsumer,

        /// <summary>
        /// Defines an endpoint that consumes messages on a channel and then directs each message to a particular
        /// consumer, or performer, to process the message.
        /// </summary>
        MessageDispatcher,

        /// <summary>
        /// Defines an endpoint that subscribes to messages on a publish-subscribe channel, either with a durable
        /// subscription that lives beyond the lifetime of a subscriber, or a non-durable subscription where the
        /// subscription lives only as long as the subscriber is active.  Subscriptions can define filters for
        /// selecting receiving messages with particular properties, generally promoted properties into the metadata,
        /// or context of a message.
        /// </summary>
        Subscriber
    }
}
