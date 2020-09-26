using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that routes by publishing to a pub-sub topic.
    /// </summary>
    /// <remarks>
    /// A message publisher assumes that the routing properties have already been
    /// resolved for a message.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class MessagePublisher : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessagePublisher"/> class.
        /// </summary>
        public MessagePublisher()
            : base(MessageRouterType.MessagePublisher)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessagePublisher"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the message publisher.</param>
        public MessagePublisher(string name)
            : base(name, MessageRouterType.MessagePublisher)
        {
        }
    }
}
