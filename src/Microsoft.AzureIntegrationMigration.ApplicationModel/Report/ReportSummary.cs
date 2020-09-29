// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Report
{
    /// <summary>
    /// Defines the root report summary node.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ReportSummary : ReportAnnotation
    {
        /// <summary>
        /// Gets or sets the timestamp when the tool started.
        /// </summary>
        public DateTimeOffset Started { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when it generated the report.
        /// </summary>
        public DateTimeOffset Completed { get; set; }

        /// <summary>
        /// Gets or sets the information about the tool.
        /// </summary>
        public ToolInfo ToolInfo { get; set; }

        /// <summary>
        /// Gets an arbitrary set of counts, such as count of errors and warning messages.
        /// </summary>
        public IDictionary<string, int> Counts { get; } = new Dictionary<string, int>();
    }
}
