// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a group of filter expressions.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class FilterGroup
    {
        /// <summary>
        /// Constructs an instance of the <see cref="FilterGroup"/> class.
        /// </summary>
        protected FilterGroup()
        {
            Operation = FilterOperation.Or;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="FilterGroup"/> class with the logical operation to perform.
        /// </summary>
        /// <param name="operation">The filter operation.</param>
        protected FilterGroup(FilterOperation operation)
        {
            Operation = operation;
        }

        /// <summary>
        /// Gets or sets the type of operation to perform when combining filter predicates.
        /// </summary>
        public FilterOperation Operation { get; set; }

        /// <summary>
        /// Gets the group of filter predicates.
        /// </summary>
        public IList<Filter> Filters { get; } = new List<Filter>();

        /// <summary>
        /// Gets or sets nested filter groups.
        /// </summary>
        public IList<FilterGroup> Groups { get; } = new List<FilterGroup>();
    }
}
