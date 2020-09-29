// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents an interface channel that is implemented using some form of a memory, file or network stream.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class StreamChannel : InterfaceChannel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="StreamChannel"/> class.
        /// </summary>
        public StreamChannel()
            : base(ChannelType.Datatype)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="StreamChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public StreamChannel(string name)
            : base(name, ChannelType.Datatype)
        {
        }
    }
}
