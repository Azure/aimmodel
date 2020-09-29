// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Represents an idempotent receiver that ensures duplicate messages received from a channel
    /// are handled in an idempotent manner.  This receiver will handle messages are being delivered
    /// using an AtLeastOnce delivery semantic.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class IdempotentReceiverEndpoint : Endpoint
    {
        /// <summary>
        /// Constructs an instance of the <see cref="IdempotentReceiverEndpoint"/> class.
        /// </summary>
        public IdempotentReceiverEndpoint()
            : base(EndpointType.IdempotentReceiver)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="IdempotentReceiverEndpoint"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the endpoint.</param>
        public IdempotentReceiverEndpoint(string name)
            : base(name, EndpointType.IdempotentReceiver)
        {
        }
    }
}
