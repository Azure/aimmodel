using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that routes by promoting routing properties then
    /// publishing to a pub-sub topic.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ContentBasedRouter : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ContentBasedRouter"/> class.
        /// </summary>
        public ContentBasedRouter()
            : base(MessageRouterType.ContentBasedRouter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ContentBasedRouter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the content based router.</param>
        public ContentBasedRouter(string name)
            : base(name, MessageRouterType.ContentBasedRouter)
        {
        }
    }
}
