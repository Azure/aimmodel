// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents an endpoint that coordinates dispatching messages from a channel to a
    /// number of processes, or performers, that may be specialised in their processing and
    /// thus require a dispatcher to send messages to the correct performer.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageDispatcherEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageDispatcherEndpoint"/> class.
        /// </summary>
        public MessageDispatcherEndpoint()
            : base(EndpointType.MessageDispatcher)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageDispatcherEndpoint"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        public MessageDispatcherEndpoint(string name)
            : base(name, EndpointType.MessageDispatcher)
        {
        }
    }
}
