// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents a business message, or document.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class DocumentMessage : Message
    {
        /// <summary>
        /// Constructs an instance of the <see cref="DocumentMessage"/> class.
        /// </summary>
        public DocumentMessage()
            : base(MessageType.Document)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="DocumentMessage"/> class with the content type.
        /// </summary>
        /// <param name="contentType">The content type of the message.</param>
        public DocumentMessage(MessageContentType contentType)
            : base(MessageType.Document, contentType)
        {
        }
    }
}
