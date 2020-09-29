// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that drops unwanted messages and only routes messages
    /// that are needed.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageFilter : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessageFilter"/> class.
        /// </summary>
        public MessageFilter()
            : base(MessageRouterType.MessageFilter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessageFilter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the message filter.</param>
        public MessageFilter(string name)
            : base(name, MessageRouterType.MessageFilter)
        {
        }
    }
}
