// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Report
{
    /// <summary>
    /// Defines a base report annotation node, used to annotate objects in the source and target model.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class ReportAnnotation
    {
        /// <summary>
        /// Gets or sets the rating for this report node, based on its convertability, if required.
        /// </summary>
        public ConversionRating Rating { get; set; }

        /// <summary>
        /// Gets the report messages.
        /// </summary>
        public IList<ReportMessage> ReportMessages { get; } = new List<ReportMessage>();

        /// <summary>
        /// Gets the list of links associated with this report node for more information.
        /// </summary>
        public IList<string> ReportLinks { get; } = new List<string>();
    }
}
