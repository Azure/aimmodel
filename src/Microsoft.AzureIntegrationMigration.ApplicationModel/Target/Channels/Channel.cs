// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a conduit for messages that flow through the messaging system from applications,
    /// to endpoints, intermediaries and on to other endpoints and applications.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class Channel : MessagingObject
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Channel"/> class.
        /// </summary>
        protected Channel()
            : this(ChannelType.PointToPoint)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Channel"/> class with its type.
        /// </summary>
        /// <param name="channelType">The type of the channel.</param>
        protected Channel(ChannelType channelType)
            : base(MessagingObjectType.Channel)
        {
            ChannelType = channelType;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Channel"/> class with its name and type.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        /// <param name="channelType">The type of the channel.</param>
        protected Channel(string name, ChannelType channelType)
            : base(name, MessagingObjectType.Channel)
        {
            ChannelType = channelType;
        }

        /// <summary>
        /// Gets or sets the type of the channel.
        /// </summary>
        public ChannelType ChannelType { get; set; }

        /// <summary>
        /// Gets a dictionary of properties associated with the channel.
        /// </summary>
        public IDictionary<string, object> ChannelProperties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets a list of the associated messages that will be sent on this channel.
        /// </summary>
        public IList<string> MessageKeyRefs { get; } = new List<string>();
    }
}
