using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines the types of message router.
    /// </summary>
    public enum MessageRouterType
    {
        /// <summary>
        /// Represents a message router that uses a routing slip attached to the message to determine
        /// the next intermediary that the message should be delivered to.
        /// </summary>
        RoutingSlip,

        /// <summary>
        /// Represents a message router that publishes a message to a pub-sub topic (queue) and promotes
        /// message body values to routing properties so that subscribers can filter on the published messages.
        /// </summary>
        ContentBasedRouter,

        /// <summary>
        /// Represents a message router that publishes a message to a pub-sub topic (queue) and ensures
        /// any routing properties are promoted so that subscribers can filter on the published messages.
        /// </summary>
        MessagePublisher,

        /// <summary>
        /// Represents a message router that subscribes to messages from a pub-sub topic (queue) based on
        /// a subscription filter to receive messages with specific properties.
        /// </summary>
        MessageSubscriber,

        /// <summary>
        /// Represents a message router that splits a message into multiple fragments, such as de-batching
        /// a message group, and sends each message to the output channel.
        /// </summary>
        Splitter,

        /// <summary>
        /// Represents a message router that aggregates multiple messages from a channel into a single
        /// message.  This is often called scatter-gather when combined with the splitter message router.
        /// </summary>
        Aggregator,

        /// <summary>
        /// Represents a message router that ensures messages are delivered in sequence to an output
        /// channel by resequencing messages received out of order on the input channel.
        /// </summary>
        Resequencer,

        /// <summary>
        /// Represents a message router that can drop messages that are not needed and only deliver
        /// required messages to the output channel.
        /// </summary>
        MessageFilter,

        /// <summary>
        /// Represents a message router that defines a complex set of decisions and message flow that
        /// can send messages received on one or more channels to different output channels, that is,
        /// it acts as a workflow.
        /// </summary>
        ProcessManager
    }
}
