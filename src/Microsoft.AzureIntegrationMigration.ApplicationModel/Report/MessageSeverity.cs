using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Report
{
    /// <summary>
    /// Defines the severity of a report message.
    /// </summary>
    [Serializable]
    public enum MessageSeverity
    {
        /// <summary>
        /// Represents an information message.
        /// </summary>
        Information = 0,

        /// <summary>
        /// Represents a warning message.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Represents an error message.
        /// </summary>
        Error = 2
    }
}
