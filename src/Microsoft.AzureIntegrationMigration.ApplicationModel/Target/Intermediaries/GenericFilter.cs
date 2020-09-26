using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that processes a message.  A generic filter represents a custom
    /// component that will perform some sort of arbitrary processing on the message.  This may be
    /// part of a pipeline where each filter interacts with a channel via a programming interface
    /// or it could be a filter that receives messages from an external queuing service.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class GenericFilter : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="GenericFilter"/> class.
        /// </summary>
        public GenericFilter()
            : base(MessageProcessorType.GenericFilter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="GenericFilter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the generic filter.</param>
        public GenericFilter(string name)
            : base(name, MessageProcessorType.GenericFilter)
        {
        }

        /// <summary>
        /// Gets or sets the component that this generic filter intermediary executes to process
        /// a message.
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of properties used by the component when it executes.
        /// </summary>
        public IDictionary<string, object> ComponentProperties { get; } = new Dictionary<string, object>();
    }
}
