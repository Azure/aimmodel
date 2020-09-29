// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents an individual filter predicate for a subscription filter.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class Filter
    {
        /// <summary>
        /// Gets or sets the filter expression based on syntax <expression> { = | <> | != | > | >= | < | <= } <expression>.
        /// </summary>
        public string FilterExpression { get; set; }
    }
}
