using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that enriches the header of a message with
    /// values from the body, where the properties are often routing properties.
    /// </summary>
    /// <remarks>
    /// Promoting values from the body of a message is often done prior to publishing
    /// a message to a pub-sub topic.  This intermediary in combination with the
    /// <see cref="MessagePublisher"/> intermediary allows a <see cref="ClaimCheck"/>
    /// intermediary to be placed between them to store large messages and send
    /// a message with a claim attached to a pub-sub topic.  If only small messages are
    /// used then a <see cref="ContentBasedRouter" would suffice.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ContentPromoter : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ContentPromoter"/> class.
        /// </summary>
        public ContentPromoter()
            : base(MessageProcessorType.ContentPromoter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ContentPromoter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the content promoter.</param>
        public ContentPromoter(string name)
            : base(name, MessageProcessorType.ContentPromoter)
        {
        }
    }
}
