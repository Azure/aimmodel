using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Represents an application that connects to the message bus.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An application connects to the message bus either directly through a channel if the
    /// application is technically capable, or using an adapter or a service activator.
    /// </para>
    /// <para>
    /// From the perspective of an application that connects to the message bus, it doesn't
    /// represent the actual application, but instead represents the middleware components
    /// needed to allow the application to connect to the message bus.
    /// </para>
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class Application : MessagingObject
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Application"/> class.
        /// </summary>
        public Application()
            : base(MessagingObjectType.Application)
        {
        }

        /// <summary>
        /// Gets a list of messages supported by this application.
        /// </summary>
        public IList<Message> Messages { get; } = new List<Message>();

        /// <summary>
        /// Gets a list of channels supported by this application.
        /// </summary>
        public IList<Channel> Channels { get; } = new List<Channel>();

        /// <summary>
        /// Gets a list of intermediaries supported by this application.
        /// </summary>
        public IList<Intermediary> Intermediaries { get; } = new List<Intermediary>();

        /// <summary>
        /// Gets a list of endpoints supported by this application.
        /// </summary>
        public IList<Endpoint> Endpoints { get; } = new List<Endpoint>();
    }
}
