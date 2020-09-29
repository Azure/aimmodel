// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents a command message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class CommandMessage : Message
    {
        /// <summary>
        /// Constructs an instance of the <see cref="CommandMessage"/> class.
        /// </summary>
        public CommandMessage()
            : base(MessageType.Command)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="CommandMessage"/> class with the content type.
        /// </summary>
        /// <param name="contentType">The content type of the message.</param>
        public CommandMessage(MessageContentType contentType)
            : base(MessageType.Command, contentType)
        {
        }
    }
}
