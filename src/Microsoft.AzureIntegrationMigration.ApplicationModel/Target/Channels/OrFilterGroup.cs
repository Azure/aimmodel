using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Represents a group of filters (predicates) that are logically tested using an OR operation.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class OrFilterGroup : FilterGroup
    {
        /// <summary>
        /// Constructs an instance of the <see cref="OrFilterGroup"/> class.
        /// </summary>
        public OrFilterGroup()
            : base(FilterOperation.Or)
        {
        }
    }
}
