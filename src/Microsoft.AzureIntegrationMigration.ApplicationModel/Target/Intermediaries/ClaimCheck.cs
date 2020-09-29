// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that extracts a fragment from the incoming message, stores
    /// the fragment in a claim store along with a claim ticket and sends the reduced message
    /// and claim to the receiver who can redeem it to retrieve the fragment.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ClaimCheck : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ClaimCheck"/> class.
        /// </summary>
        public ClaimCheck()
            : base(MessageProcessorType.ClaimCheck)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ClaimCheck"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the claim check.</param>
        public ClaimCheck(string name)
            : base(name, MessageProcessorType.ClaimCheck)
        {
        }

        /// <summary>
        /// Gets or sets a reference to the map that this processor uses to translate the
        /// incoming message into a reduced message with a claim.
        /// </summary>
        public string MapKeyRef { get; set; }

        /// <summary>
        /// Gets or sets a reference to the claim store used for storing message fragments
        /// </summary>
        public string ClaimStoreKeyRef { get; set; }
    }
}
