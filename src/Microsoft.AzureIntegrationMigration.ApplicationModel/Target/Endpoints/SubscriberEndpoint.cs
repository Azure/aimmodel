using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents an endpoint that subscribes to messages from a pub-sub channel.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class SubscriberEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="SubscriberEndpoint"/> class.
        /// </summary>
        public SubscriberEndpoint()
            : base(EndpointType.Subscriber)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="SubscriberEndpoint"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        public SubscriberEndpoint(string name)
            : base(name, EndpointType.Subscriber)
        {
        }

        /// <summary>
        /// Gets a dicationary of topics and related subscription names associated with a publish-subscribe
        /// channel that this endpoint relies on.
        /// </summary>
        public IDictionary<string, string> TopicSubscriptions { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets a value indicating whether the underlying subscription is durable or
        /// non-durable, that is, does the subscription survive the lifetime of the subscriber.
        /// </summary>
        public bool IsDurable { get; set; }
    }
}
