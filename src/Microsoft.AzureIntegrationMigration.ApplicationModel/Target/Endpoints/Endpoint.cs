// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents an endpoint that connects an application to the messaging system, either
    /// in-process or out-of-process.  It is responsible for sending or receiving messages on
    /// behalf of the application and acts as a bridge between the application and the messge
    /// bus.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class Endpoint : MessagingObject
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Endpoint"/> class.
        /// </summary>
        protected Endpoint()
            : this(EndpointType.Adapter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Endpoint"/> class with the type of the endpoint.
        /// </summary>
        /// <param name="endpointType">The type of the endpoint.</param>
        protected Endpoint(EndpointType endpointType)
            : base(MessagingObjectType.Endpoint)
        {
            EndpointType = endpointType;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Endpoint"/> class with the name and type of the endpoint.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        /// <param name="endpointType">The type of the endpoint.</param>
        protected Endpoint(string name, EndpointType endpointType)
            : base(name, MessagingObjectType.Endpoint)
        {
            EndpointType = endpointType;
        }

        /// <summary>
        /// Gets or sets the type of the endpoint.
        /// </summary>
        public EndpointType EndpointType { get; set; }

        /// <summary>
        /// Gets or sets the direction that messages flow through the endpoint.
        /// </summary>
        public MessageExchangePattern MessageExchangePattern { get; set; }

        /// <summary>
        /// Gets or sets the message delivery semantics enforced by the endpoint.
        /// </summary>
        public MessageDeliveryGuarantee MessageDeliveryGuarantee { get; set; }

        /// <summary>
        /// Gets or sets whether the endpoint activates a new message when connecting to the message
        /// bus and receiving a request from an application.  If set to True, the endpoint should create
        /// a bus message containing the application message to send to a channel.
        /// </summary>
        public bool Activator { get; set; }

        /// <summary>
        /// Gets the related messages that this endpoint will send or receive.
        /// </summary>
        public IList<string> MessageKeyRefs { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the related channel that this endpoint sends messages on, depending
        /// on its message exchange pattern.
        /// </summary>
        public string OutputChannelKeyRef { get; set; }

        /// <summary>
        /// Gets or sets the related channel that this endpoint receives messages on, depending
        /// on its message exchange pattern.
        /// </summary>
        public string InputChannelKeyRef { get; set; }
    }
}
