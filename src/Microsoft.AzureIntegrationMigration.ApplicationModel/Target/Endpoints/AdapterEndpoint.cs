// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents an adapter endpoint that typically runs out-of-process and allows an application
    /// to connect to the messaging system where it cannot natively do so.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class AdapterEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="AdapterEndpoint"/> class.
        /// </summary>
        public AdapterEndpoint()
            : base(EndpointType.Adapter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="AdapterEndpoint"/> class with its protocol.
        /// </summary>
        /// <param name="adapterProtocol">The protocol of the adapter.</param>
        public AdapterEndpoint(string adapterProtocol)
            : base(EndpointType.Adapter)
        {
            AdapterProtocol = adapterProtocol;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="AdapterEndpoint"/> class with its name and protocol.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        /// <param name="adapterProtocol">The protocol of the adapter.</param>
        public AdapterEndpoint(string name, string adapterProtocol)
            : base(name, EndpointType.Adapter)
        {
            AdapterProtocol = adapterProtocol;
        }

        /// <summary>
        /// Gets or sets the name of the wire or application protocol or other mechanism supported
        /// by the adapter to send and receive messages.
        /// </summary>
        public string AdapterProtocol { get; set; }
    }
}
