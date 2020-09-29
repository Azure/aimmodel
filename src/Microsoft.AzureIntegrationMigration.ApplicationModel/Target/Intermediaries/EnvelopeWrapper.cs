// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that wraps a message it receives in an envelope format.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class EnvelopeWrapper : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="EnvelopeWrapper"/> class.
        /// </summary>
        public EnvelopeWrapper()
            : base(MessageProcessorType.EnvelopeWrapper)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="EnvelopeWrapper"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the envelope wrapper.</param>
        public EnvelopeWrapper(string name)
            : base(name, MessageProcessorType.EnvelopeWrapper)
        {
        }

        /// <summary>
        /// Gets or sets a reference to the map that this wrapper uses to wrap an existing
        /// message with an envelope.
        /// </summary>
        public string MapKeyRef { get; set; }
    }
}
