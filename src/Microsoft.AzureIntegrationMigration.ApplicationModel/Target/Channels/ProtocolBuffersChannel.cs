using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a RPC channel that is implemented using protocol buffers as the data serialization mechanism
    /// over gRPC.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ProtocolBuffersChannel : RpcChannel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ProtocolBuffersChannel"/> class.
        /// </summary>
        public ProtocolBuffersChannel()
            : base(ChannelType.Datatype)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ProtocolBuffersChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public ProtocolBuffersChannel(string name)
            : base(name, ChannelType.Datatype)
        {
        }
    }
}
