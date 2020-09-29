// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a filter for a subscription for a subscriber endpoint.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class SubscriptionFilter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="SubscriptionFilter"/> class.
        /// </summary>
        public SubscriptionFilter()
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="SubscriptionFilter"/> class with its filter.
        /// </summary>
        /// <param name="filterGroup">The filter for the subscription.</param>
        public SubscriptionFilter(FilterGroup filterGroup)
            : this()
        {
            FilterGroup = filterGroup ?? throw new ArgumentNullException(nameof(filterGroup));
        }

        /// <summary>
        /// Gets or sets the filter group for the subscription.
        /// </summary>
        public FilterGroup FilterGroup { get; set; }
    }
}
