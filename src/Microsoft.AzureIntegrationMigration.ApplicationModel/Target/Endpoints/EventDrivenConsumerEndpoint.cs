using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents an event driver consumer endpoint that is notified when messages are available
    /// to be received from a channel.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class EventDrivenConsumerEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="EventDrivenConsumerEndpoint"/> class.
        /// </summary>
        public EventDrivenConsumerEndpoint()
            : base(EndpointType.EventDrivenConsumer)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="EventDrivenConsumerEndpoint"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        public EventDrivenConsumerEndpoint(string name)
            : base(name, EndpointType.EventDrivenConsumer)
        {
        }
    }
}
