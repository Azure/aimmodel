using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Report
{
    /// <summary>
    /// Defines a set of conversion ratings for report nodes.
    /// </summary>
    [Serializable]
    public enum ConversionRating
    {
        /// <summary>
        /// No rating applied to the node.
        /// </summary>
        NoRating = 0,

        /// <summary>
        /// No conversion possible, functionality is not supported by the migration target.
        /// </summary>
        NotSupported = 1,

        /// <summary>
        /// No automatic conversion by the tool is possible, but a custom solution could be implemented.
        /// </summary>
        NoAutomaticConversion = 2,

        /// <summary>
        /// Partial conversion possible, but output template requires more work to complete.
        /// </summary>
        PartialConversion = 3,

        /// <summary>
        /// Full conversion possible but with some loss of functionality where migration target doesn't support all options.
        /// </summary>
        FullConversionWithFidelityLoss = 4,

        /// <summary>
        /// Full conversion possible with no loss of functionality.
        /// </summary>
        FullConversion = 5,
    }
}
