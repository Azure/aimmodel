// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines a specialised variable used to define a correlation that is used by send and
    /// receive activities to correlate messages to the same instance of the process manager.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class WorkflowCorrelationVariable : WorkflowVariable
    {
        /// <summary>
        /// Gets a list of correlation properties.
        /// </summary>
        public IList<WorkflowVariable> CorrelationProperties { get; } = new List<WorkflowVariable>();

        /// <summary>
        /// Gets the activity that initializes the correlation with values from a message.
        /// </summary>
        public WorkflowActivity InitializingActivity { get; set; }

        /// <summary>
        /// Gets a list of activities that use the values in the correlation to either promote
        /// those values to the message before it is sent or used to define a subscription filter
        /// when messages are received.
        /// </summary>
        public IList<WorkflowActivity> FollowingActivities { get; } = new List<WorkflowActivity>();
    }
}
