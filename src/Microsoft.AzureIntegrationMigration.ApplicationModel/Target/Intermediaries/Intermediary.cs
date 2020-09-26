using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that receives messages from a channel, processes the messages and sends the messages
    /// onto the next channel.  It acts essentially as a processing step in a chain.  The channels betwen intermediaries
    /// may not necessarily represent a message queueing system, but could be a programming interface.  Thus, a message
    /// receiver intermediary could channel a message that it received from a message queue via a program call to a pipe
    /// intermediary.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class Intermediary : MessagingObject
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Intermediary"/> class.
        /// </summary>
        protected Intermediary()
            : this(IntermediaryType.MessageProcessor)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Intermediary"/> class with its type.
        /// </summary>
        /// <param name="intermediaryType">The type of the intermediary.</param>
        protected Intermediary(IntermediaryType intermediaryType)
            : base(MessagingObjectType.Intermediary)
        {
            IntermediaryType = intermediaryType;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Intermediary"/> class with its name and type.
        /// </summary>
        /// <param name="name">The name of the intermediary.</param>
        /// <param name="intermediaryType">The type of the intermediary.</param>
        protected Intermediary(string name, IntermediaryType intermediaryType)
            : base(name, MessagingObjectType.Intermediary)
        {
            IntermediaryType = intermediaryType;
        }

        /// <summary>
        /// Gets or sets whether the intermediary activates a new message.
        /// </summary>
        public bool Activator { get; set; }

        /// <summary>
        /// Gets or sets the type of the intermediary.
        /// </summary>
        public IntermediaryType IntermediaryType { get; set; }

        /// <summary>
        /// Gets a list of references to channels used as a source of messages to this intermediary.
        /// </summary>
        public IList<string> InputChannelKeyRefs { get; } = new List<string>();

        /// <summary>
        /// Gets a list of references to channels used as a sink of messages for this intermediary.
        /// </summary>
        public IList<string> OutputChannelKeyRefs { get; } = new List<string>();
    }
}
