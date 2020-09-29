// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that orchestrates the routing of messages to output channels
    /// using complex decision flow.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ProcessManager : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ProcessManager"/> class.
        /// </summary>
        public ProcessManager()
            : base(MessageRouterType.ProcessManager)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ProcessManager"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the process manager.</param>
        public ProcessManager(string name)
            : base(name, MessageRouterType.ProcessManager)
        {
        }

        /// <summary>
        /// Gets or sets the reference to the workflow model used by this intermediary.
        /// </summary>
        public WorkflowDefinition WorkflowModel { get; set; }
    }
}
