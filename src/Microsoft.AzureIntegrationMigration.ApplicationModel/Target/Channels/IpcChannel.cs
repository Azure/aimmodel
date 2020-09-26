using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is implemented an inter-process communication technology
    /// such as named pipes.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class IpcChannel : Channel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="IpcChannel"/> class.
        /// </summary>
        protected IpcChannel(ChannelType type)
            : base(type)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="IpcChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        /// <param name="type">The type of the channel.</param>
        protected IpcChannel(string name, ChannelType type)
            : base(name, type)
        {
        }
    }
}
