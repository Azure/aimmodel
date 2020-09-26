using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents a negative acknowledgement message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class NackMessage : Message
    {
        /// <summary>
        /// Constructs an instance of the <see cref="NackMessage"/> class.
        /// </summary>
        public NackMessage()
            : base(MessageType.Nack)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="NackMessage"/> class with the content type.
        /// </summary>
        /// <param name="contentType">The content type of the message.</param>
        public NackMessage(MessageContentType contentType)
            : base(MessageType.Nack, contentType)
        {
        }
    }
}
