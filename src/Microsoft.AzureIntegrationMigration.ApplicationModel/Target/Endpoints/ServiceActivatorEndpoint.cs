// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents a service activator endpoint that sends messages to an application in a request-reply
    /// exchange pattern, where the application is available to be consumed as a service.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ServiceActivatorEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ServiceActivatorEndpoint"/> class.
        /// </summary>
        public ServiceActivatorEndpoint()
            : base(EndpointType.ServiceActivator)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ServiceActivatorEndpoint"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        public ServiceActivatorEndpoint(string name)
            : base(name, EndpointType.ServiceActivator)
        {
        }
    }
}
