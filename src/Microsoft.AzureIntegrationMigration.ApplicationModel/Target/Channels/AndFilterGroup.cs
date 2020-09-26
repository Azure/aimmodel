 using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a group of filters (predicates) that are logically tested using an AND operation.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class AndFilterGroup : FilterGroup
    {
        /// <summary>
        /// Constructs an instance of the <see cref="AndFilterGroup"/> class.
        /// </summary>
        public AndFilterGroup()
            : base(FilterOperation.And)
        {
        }
    }
}
