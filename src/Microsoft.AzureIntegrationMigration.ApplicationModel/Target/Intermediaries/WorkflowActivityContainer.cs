using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines a workflow activity container which is used to group child activities together
    /// which can form a hierarchy of activities within the model because the container itself
    /// is also an activity.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class WorkflowActivityContainer : WorkflowActivity
    {
        /// <summary>
        /// Gets a list of variables defined at the scope of this container.
        /// </summary>
        public IList<WorkflowVariable> Variables { get; } = new List<WorkflowVariable>();

        /// <summary>
        /// Defines a list of messages defined at the scope of this container.
        /// </summary>
        public IList<WorkflowMessage> Messages { get; } = new List<WorkflowMessage>();

        /// <summary>
        /// Defins a list of activities defined at the scope of this container.
        /// </summary>
        public IList<WorkflowActivity> Activities { get; } = new List<WorkflowActivity>();
    }
}
