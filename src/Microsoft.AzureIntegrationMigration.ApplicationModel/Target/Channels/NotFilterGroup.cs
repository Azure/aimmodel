using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a group of filters (predicates) that are logically tested using a NOT operation.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class NotFilterGroup : FilterGroup
    {
        /// <summary>
        /// Constructs an instance of the <see cref="NotFilterGroup"/> class.
        /// </summary>
        public NotFilterGroup()
            : base(FilterOperation.Not)
        {
        }
    }
}
