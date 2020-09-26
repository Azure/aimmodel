using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines the workflow definition which is the top level activity container.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class WorkflowDefinition : WorkflowActivityContainer
    {
        /// <summary>
        /// Gets the list of channels defined at the top level scope of the workflow.
        /// </summary>
        public IList<WorkflowChannel> Channels { get; } = new List<WorkflowChannel>();
    }
}
