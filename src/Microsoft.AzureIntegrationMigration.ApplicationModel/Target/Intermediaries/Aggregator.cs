using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that routes by aggregating multiple messages
    /// into a single message.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class Aggregator : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Aggregator"/> class.
        /// </summary>
        public Aggregator()
            : base(MessageRouterType.Aggregator)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Aggregator"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the aggregator.</param>
        public Aggregator(string name)
            : base(name, MessageRouterType.Aggregator)
        {
        }
    }
}
