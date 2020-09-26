using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Defines the types of channel supported by the messaging system.
    /// </summary>
    public enum ChannelType
    {
        /// <summary>
        /// Represents a channel that receives messages from a single publisher and
        /// send the messages to a single consumer.
        /// </summary>
        PointToPoint,

        /// <summary>
        /// Represents a channel that receives messages from a publisher and delivers
        /// the messages to zero or more consumers based on their interest in the messages
        /// using filters to ignore messages not needed.
        /// </summary>
        PublishSubscribe,

        /// <summary>
        /// Represents a channel that receives and send messages of a particular type only.
        /// </summary>
        Datatype,

        /// <summary>
        /// Represents a channel that receives messages that cannot be delivered to a consumer
        /// from another channel.
        /// </summary>
        DeadLetter,

        /// <summary>
        /// Represents a channel that receives messages that are invalid and cannot be processed
        /// by a downstream system.
        /// </summary>
        InvalidMessage,

        /// <summary>
        /// Represents a channel that guarantees delivery of a message, even if the consumer is
        /// not always available. Typically channels like this will utilise a mechanism called
        /// store and forward.
        /// </summary>
        GuaranteedDelivery,

        /// <summary>
        /// Represents a channel that guarantees that messages will be delivered in sequence.
        /// </summary>
        OrderedDelivery
    }
}
