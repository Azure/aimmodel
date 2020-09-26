using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents an IPC channel that is implemented using named pipes.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class NamedPipesChannel : IpcChannel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="NamedPipesChannel"/> class.
        /// </summary>
        public NamedPipesChannel()
            : base(ChannelType.PointToPoint)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="NamedPipesChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public NamedPipesChannel(string name)
            : base(name, ChannelType.PointToPoint)
        {
        }
    }
}
