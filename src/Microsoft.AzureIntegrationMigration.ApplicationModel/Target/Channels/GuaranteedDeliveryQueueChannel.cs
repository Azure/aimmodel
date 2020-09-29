// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is implemented as a Guaranteed Delivery queue, that is,
    /// a normal store and forward queueing system with dead letter support.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class GuaranteedDeliveryQueueChannel : QueueChannel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="GuaranteedDeliveryQueueChannel"/> class.
        /// </summary>
        public GuaranteedDeliveryQueueChannel()
            : base(ChannelType.GuaranteedDelivery)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="GuaranteedDeliveryQueueChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public GuaranteedDeliveryQueueChannel(string name)
            : base(name, ChannelType.GuaranteedDelivery)
        {
        }

        /// <summary>
        /// Gets or sets a dead letter queue for this queue, if applicable.
        /// </summary>
        public DeadLetterQueueChannel DeadLetterQueue { get; set; }
    }
}
