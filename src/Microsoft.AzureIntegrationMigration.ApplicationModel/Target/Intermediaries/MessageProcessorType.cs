using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines the types of message processor.
    /// </summary>
    public enum MessageProcessorType
    {
        /// <summary>
        /// Represents a message processor that is an individual filter run as part of a general
        /// message processing pipeline.
        /// </summary>
        GenericFilter,

        /// <summary>
        /// Represents a message processor that translates a message from one format to another.
        /// </summary>
        Translator,

        /// <summary>
        /// Represents a specialised message translator that enriches a message with new content.
        /// </summary>
        ContentEnricher,

        /// <summary>
        /// Represents a specialised message translator that filters content from a message, such as
        /// sanitising a message.
        /// </summary>
        ContentFilter,

        /// <summary>
        /// Represents a message processor that promotes values from the body of the message to a
        /// header.
        /// </summary>
        ContentPromoter,

        /// <summary>
        /// Represents a message processor that demotes values from the header of the message to the
        /// body.
        /// </summary>
        ContentDemoter,

        /// <summary>
        /// Represents a specialised message translator that wraps a message in envelope format.
        /// </summary>
        EnvelopeWrapper,

        /// <summary>
        /// Represents a specialised message translator that stores part of a message in a store
        /// associated by a 'claim' and passes the reduced message with the claim to a receiver
        /// that can use the claim to retrieve the stored part of the message from the store.
        /// </summary>
        ClaimCheck
    }
}
