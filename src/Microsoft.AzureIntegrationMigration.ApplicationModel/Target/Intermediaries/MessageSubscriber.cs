// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that subscribes to messages from a pub-sub topic.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageSubscriber : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageSubscriber"/> class.
        /// </summary>
        public MessageSubscriber()
            : base(MessageRouterType.MessageSubscriber)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageSubscriber"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the message subscriber.</param>
        public MessageSubscriber(string name)
            : base(name, MessageRouterType.MessageSubscriber)
        {
        }

        /// <summary>
        /// Gets a dicationary of topics and related subscription names associated with a publish-subscribe
        /// channel that this intermediary relies on.
        /// </summary>
        public IDictionary<string, string> TopicSubscriptions { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets a value indicating whether the underlying subscription is durable or
        /// non-durable, that is, does the subscription survive the lifetime of the subscriber.
        /// </summary>
        public bool IsDurable { get; set; }
    }
}
