// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is implemented by a queue. 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class QueueChannel : Channel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="QueueChannel"/> class.
        /// </summary>
        protected QueueChannel(ChannelType type)
            : base(type)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="QueueChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        /// <param name="type">The type of the channel.</param>
        protected QueueChannel(string name, ChannelType type)
            : base(name, type)
        {
        }

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Gets or sets the guarantee semantics for message delivery.
        /// </summary>
        /// <remarks>
        /// <para>
        /// AtMostOnce defines a receive mode of Receive and Delete.
        /// </para>
        /// <para>
        /// AtLeastOnce defines a receive mode of Peek and Lock.
        /// </para>
        /// </remarks>
        public MessageDeliveryGuarantee MessageDeliveryGuarantee { get; set; }
    }
}
