// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that routes a message from one or more channels
    /// to zero, one or more channels using different strategies.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class MessageRouter : Intermediary
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageRouter"/> class.
        /// </summary>
        protected MessageRouter()
            : this(MessageRouterType.ContentBasedRouter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageRouter"/> class with its type.
        /// </summary>
        /// <param name="messageProcessorType">The type of the message router.</param>
        protected MessageRouter(MessageRouterType messageRouterType)
            : base(IntermediaryType.MessageRouter)
        {
            MessageRouterType = messageRouterType;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageRouter"/> class with a name and its type.
        /// </summary>
        /// <param name="name">The name of the message processor.</param>
        /// <param name="messageProcessorType">The type of the message router.</param>
        protected MessageRouter(string name, MessageRouterType messageRouterType)
            : base(name, IntermediaryType.MessageRouter)
        {
            MessageRouterType = messageRouterType;
        }

        /// <summary>
        /// Gets or sets the type of the message router.
        /// </summary>
        public MessageRouterType MessageRouterType { get; set; }
    }
}
