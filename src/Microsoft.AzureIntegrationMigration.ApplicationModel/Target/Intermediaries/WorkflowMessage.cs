// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines a workflow message that is sent and received by a process manager.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class WorkflowMessage : WorkflowObject
    {
        /// <summary>
        /// Gets or sets a value that indicates the type of workflow message.
        /// </summary>
        public WorkflowMessageType WorkflowMessageType { get; set; }

        /// <summary>
        /// Gets or sets the application specific message type used to identify the workflow message.
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Gets or sets the properties associated with the message when correlating.
        /// </summary>
        public IList<string> CorrelationProperties { get; } = new List<string>();
    }
}
