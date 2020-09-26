using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Represents an event message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class EventMessage : Message
    {
        /// <summary>
        /// Constructs an instance of the <see cref="EventMessage"/> class.
        /// </summary>
        public EventMessage()
            : base(MessageType.Event)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="EventMessage"/> class with the content type.
        /// </summary>
        /// <param name="contentType">The content type of the message.</param>
        public EventMessage(MessageContentType contentType)
            : base(MessageType.Event, contentType)
        {
        }
    }
}
