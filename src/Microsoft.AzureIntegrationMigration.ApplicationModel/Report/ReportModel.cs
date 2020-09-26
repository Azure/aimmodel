using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Report
{
    /// <summary>
    /// Defines the root report node.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ReportModel
    {
        /// <summary>
        /// Defines the summary node.
        /// </summary>
        public ReportSummary Summary { get; set; }
    }
}
