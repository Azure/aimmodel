// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a subscription for a subscriber endpoint.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class Subscription
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Subscription"/> class.
        /// </summary>
        public Subscription()
        {
            IsDurable = false;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Subscription"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the subscription.</param>
        public Subscription(string name)
            : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Subscription"/> class with its name and the topic
        /// that this subscription is subscribing to.
        /// </summary>
        /// <param name="name">The name of the subscription.</param>
        /// <param name="topic">The name of the topic for this subscription.</param>
        public Subscription(string name, string topic)
            : this(name)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
        }

        /// <summary>
        /// Gets or sets the name of the subscription.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the topic for this subscription.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets the filters for the subscription.
        /// </summary>
        public IList<SubscriptionFilter> Filters { get; } = new List<SubscriptionFilter>();

        /// <summary>
        /// Gets or sets a value indicating whether the subscription is durable or
        /// non-durable, that is, does the subscription survive the lifetime of the subscriber.
        /// </summary>
        public bool IsDurable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the subscription should maintain
        /// the order of messages received when delivering to a consumer.
        /// </summary>
        public bool IsOrdered { get; set; }
    }
}
