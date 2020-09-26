using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that uses some form of message sessions to allow multiple
    /// distinct but related messages to be correlated together on the same queue.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class CorrelatingQueueChannel : QueueChannel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="CorrelatingQueueChannel"/> class.
        /// </summary>
        public CorrelatingQueueChannel()
            : base(ChannelType.Datatype)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="CorrelatingQueueChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public CorrelatingQueueChannel(string name)
            : base(name, ChannelType.Datatype)
        {
        }

        /// <summary>
        /// Gets or sets a dead letter queue for this queue, if applicable.
        /// </summary>
        public DeadLetterQueueChannel DeadLetterQueue { get; set; }
    }
}
