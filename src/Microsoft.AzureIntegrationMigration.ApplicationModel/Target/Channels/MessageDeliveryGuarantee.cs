using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels
{
    /// <summary>
    /// Defines an enumeration of allowable values for the message delivery semantics.
    /// </summary>
    public enum MessageDeliveryGuarantee
    {
        /// <summary>
        /// No delivery semantics are enforced so messages could be delivered more than
        /// once or not at all.
        /// </summary>
        None,

        /// <summary>
        /// Delivery must be at least once but it's acceptable to send the same message more
        /// than once.  This would require an idempotent message receiver to correctly recognise
        /// and discard duplicate messages.
        /// </summary>
        AtLeastOnce,

        /// <summary>
        /// The message must only be delivered once, not more than once, but may also be undelivered.
        /// </summary>
        AtMostOnce
    }
}
