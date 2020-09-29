// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that routes based on a routing slip attached to a message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class RoutingSlipRouter : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="RoutingSlipRouter"/> class.
        /// </summary>
        public RoutingSlipRouter()
            : base(MessageRouterType.RoutingSlip)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="RoutingSlipRouter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the routing slip router.</param>
        public RoutingSlipRouter(string name)
            : base(name, MessageRouterType.RoutingSlip)
        {
        }
    }
}
