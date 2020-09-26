using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines a workflow variable used to hold a value that can be used by workflow
    /// activities.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class WorkflowVariable : WorkflowObject
    {
        /// <summary>
        /// Gets or sets the initial value of the workflow variable.
        /// </summary>
        public string InitialValue { get; set; }

        /// <summary>
        /// Gets or sets whether the default constructor is used to initialize the variable or not.
        /// </summary>
        public bool UseDefaultConstructor { get; set; }
    }
}
