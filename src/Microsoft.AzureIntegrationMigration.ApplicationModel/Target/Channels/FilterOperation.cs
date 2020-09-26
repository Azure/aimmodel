using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Defines an enumeration of allowable values for the types of filter group logical operations.
    /// </summary>
    public enum FilterOperation
    {
        /// <summary>
        /// Represents an OR operation.
        /// </summary>
        Or,

        /// <summary>
        /// Represents an AND operation.
        /// </summary>
        And,

        /// <summary>
        /// Represents a NOT operation.
        /// </summary>
        Not
    }
}
