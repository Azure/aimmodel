using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is implemented as a Dead Letter queue that is used
    /// to queue messages that haven't been able to be delivered by a normal queue or topic.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class DeadLetterQueueChannel : QueueChannel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="DeadLetterQueueChannel"/> class.
        /// </summary>
        public DeadLetterQueueChannel()
            : base(ChannelType.DeadLetter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="DeadLetterQueueChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public DeadLetterQueueChannel(string name)
            : base(name, ChannelType.DeadLetter)
        {
        }
    }
}
