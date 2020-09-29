// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that represents a topic in a publish-subscribe channel with dead letter
    /// queues per subscription if applicable.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class TopicChannel : Channel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="TopicChannel"/> class.
        /// </summary>
        public TopicChannel()
            : base(ChannelType.PublishSubscribe)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="TopicChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public TopicChannel(string name)
            : base(name, ChannelType.PublishSubscribe)
        {
        }

        /// <summary>
        /// Gets or sets the name of the topic associated with this channel.
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// Gets a list of subscriptions associated with this topic.
        /// </summary>
        public IList<Subscription> Subscriptions { get; } = new List<Subscription>();

        /// <summary>
        /// Gets a dead letter queue per subscription, if applicable.
        /// </summary>
        public IDictionary<string, DeadLetterQueueChannel> DeadLetterQueues { get; } = new Dictionary<string, DeadLetterQueueChannel>();
    }
}
