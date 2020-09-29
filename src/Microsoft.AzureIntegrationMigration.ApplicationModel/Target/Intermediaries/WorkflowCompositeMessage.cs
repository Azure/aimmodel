// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines a workflow message containing multiple parts.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class WorkflowCompositeMessage : WorkflowMessage
    {
        /// <summary>
        /// Gets or sets the message parts associated with the message.
        /// </summary>
        public IList<WorkflowMessage> MessageParts { get; } = new List<WorkflowMessage>();
    }
}
