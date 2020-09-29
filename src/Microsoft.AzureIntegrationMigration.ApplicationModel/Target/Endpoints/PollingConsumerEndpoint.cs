// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents a polling consumer endpoint that polls a channel for available messages.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class PollingConsumerEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="PollingConsumerEndpoint"/> class.
        /// </summary>
        public PollingConsumerEndpoint()
            : base(EndpointType.PollingConsumer)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="PollingConsumerEndpoint"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        public PollingConsumerEndpoint(string name)
            : base(name, EndpointType.PollingConsumer)
        {
        }
    }
}
