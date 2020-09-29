// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a channel that is implemented as an endpoint that can be used by a
    /// caller to send a message to a URL.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class TriggerChannel : Channel
    {
        /// <summary>
        /// Constructs an instance of the <see cref="TriggerChannel"/> class.
        /// </summary>
        public TriggerChannel()
            : base(ChannelType.PointToPoint)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="TriggerChannel"/> class with its name.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public TriggerChannel(string name)
            : base(name, ChannelType.PointToPoint)
        {
        }


        /// <summary>
        /// Gets or sets the address that the channel represents to a message sender.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "To pass JSON deserialization.")]
        public string TriggerUrl { get; set; }
    }
}
