using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Report
{
    /// <summary>
    /// Defines a message with a severity.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ReportMessage
    {
        /// <summary>
        /// Gets or sets the severity of the message.
        /// </summary>
        public MessageSeverity Severity { get; set; }

        /// <summary>
        /// Gets or sets the textual message.
        /// </summary>
        public string Message { get; set; }
    }
}
