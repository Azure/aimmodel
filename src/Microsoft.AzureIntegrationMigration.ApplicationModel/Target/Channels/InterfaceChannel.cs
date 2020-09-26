using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is represented as an interface that is implemented by a
    /// class in an OO programming language.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class InterfaceChannel : Channel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="InterfaceChannel"/> class.
        /// </summary>
        protected InterfaceChannel(ChannelType type)
            : base(type)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="InterfaceChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        /// <param name="type">The type of the channel.</param>
        protected InterfaceChannel(string name, ChannelType type)
            : base(name, type)
        {
        }

        /// <summary>
        /// Gets or sets the name of the interface used to implement this channel.
        /// </summary>
        public string InterfaceName { get; set; }
    }
}
