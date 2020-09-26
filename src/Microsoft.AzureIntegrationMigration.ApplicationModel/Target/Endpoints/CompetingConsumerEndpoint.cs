using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents a competing consumer endpoint that supports multiple parallel processes
    /// receiving messages from a channel.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class CompetingConsumerEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="CompetingConsumerEndpoint"/> class.
        /// </summary>
        public CompetingConsumerEndpoint()
            : base(EndpointType.CompetingConsumer)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="CompetingConsumerEndpoint"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        public CompetingConsumerEndpoint(string name)
            : base(name, EndpointType.CompetingConsumer)
        {
        }
    }
}
