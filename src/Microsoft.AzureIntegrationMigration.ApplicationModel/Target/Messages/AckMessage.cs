using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents an acknowledgement message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class AckMessage : Message
    {
        /// <summary>
        /// Constructs an instance of the <see cref="AckMessage"/> class.
        /// </summary>
        public AckMessage()
            : base(MessageType.Ack)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="AckMessage"/> class with the content type.
        /// </summary>
        /// <param name="contentType">The content type of the message.</param>
        public AckMessage(MessageContentType contentType)
            : base(MessageType.Ack, contentType)
        {
        }
    }
}
