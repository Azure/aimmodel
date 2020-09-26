using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is implemented by a remote-procedure call technology
    /// such as protocol buffers.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class RpcChannel : Channel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="RpcChannel"/> class.
        /// </summary>
        protected RpcChannel(ChannelType type)
            : base(type)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="RpcChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        /// <param name="type">The type of the channel.</param>
        protected RpcChannel(string name, ChannelType type)
            : base(name, type)
        {
        }
    }
}
