using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines a workflow channel used to bind to channels in the target model where
    /// messages flow in and out of a process manager intermediary.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class WorkflowChannel : WorkflowObject
    {
        /// <summary>
        /// Gets or sets the name of the operation related to this channel.
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the channel is used to activate a new instance
        /// of a process manager or whether the message should correlate to an existing instance.
        /// </summary>
        public bool Activator { get; set; }

        /// <summary>
        /// Gets or sets a subscription when receiving messages and activating a new instance
        /// of a process manager.  Correlating to an existing instance would require a
        /// dynamic subscription when a message is received because the correlation properties
        /// will come from the message.
        /// </summary>
        public Subscription Subscription { get; set; }

        /// <summary>
        /// Gets or sets the pattern for this channel, depending on whether a message
        /// is being received or sent and is one-way or two-way.
        /// </summary>
        public MessageExchangePattern Direction { get; set; }

        /// <summary>
        /// Gets a list of the messages being received into the channel from a channel in
        /// the target model.
        /// </summary>
        public IList<WorkflowMessage> MessagesIn { get; } = new List<WorkflowMessage>();

        /// <summary>
        /// Gets a list of the messages being sent from the channel by a channel in the
        /// target model.
        /// </summary>
        public IList<WorkflowMessage> MessagesOut { get; } = new List<WorkflowMessage>();

        /// <summary>
        /// Gets a list of references to channels in the target model for messages
        /// flowing into the workflow.
        /// </summary>
        public IList<string> ChannelKeyRefIn { get; } = new List<string>();

        /// <summary>
        /// Gets a list of references to channels in the target model for messages
        /// flowing out of the workflow.
        /// </summary>
        public IList<string> ChannelKeyRefOut { get; } = new List<string>();
    }
}
