using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is implemented as an Ordered Delivery queue that
    /// uses message sessions to guarantee order.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class OrderedDeliveryQueueChannel : QueueChannel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="OrderedDeliveryQueueChannel"/> class.
        /// </summary>
        public OrderedDeliveryQueueChannel()
            : base(ChannelType.OrderedDelivery)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="OrderedDeliveryQueueChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public OrderedDeliveryQueueChannel(string name)
            : base(name, ChannelType.OrderedDelivery)
        {
        }

        /// <summary>
        /// Gets or sets a dead letter queue for this queue, if applicable.
        /// </summary>
        public DeadLetterQueueChannel DeadLetterQueue { get; set; }
    }
}
