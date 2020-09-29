// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Represents the target integration solution on AIS as a conceptual message bus.
    /// </summary>
    /// <remarks>
    /// A message bus represents a common messaging platform that allows multiple applications to
    /// interact with each other in a loosely couple manner and inteface to the message bus using
    /// adapters and service activators if unable to natively connect using a channel.  The message
    /// bus will typically support a canonical data model and is responsible for routing messages.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageBus : MessagingObject
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageBus"/> class.
        /// </summary>
        public MessageBus()
            : base(MessagingObjectType.MessageBus)
        {
        }

        /// <summary>
        /// Gets a list of the applications that are supported by the message bus.
        /// </summary>
        public IList<Application> Applications { get; } = new List<Application>();
    }
}
