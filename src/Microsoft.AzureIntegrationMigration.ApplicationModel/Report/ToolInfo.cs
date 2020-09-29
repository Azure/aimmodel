// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Report
{
    /// <summary>
    /// Defines the information about the migration tool.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ToolInfo : ReportAnnotation
    {
        /// <summary>
        /// Gets or sets the version of the tool.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets the command line arguments for the tool.
        /// </summary>
        public IDictionary<string, object> CommandLineArgs { get; } = new Dictionary<string, object>();
    }
}
