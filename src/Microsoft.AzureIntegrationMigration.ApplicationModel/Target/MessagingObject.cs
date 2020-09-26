using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Report;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Defines a base messaging object.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class MessagingObject : ReportAnnotation
    {
        /// <summary>
        /// Constructs an instance of the <see cref="MessagingObject"/> class.
        /// </summary>
        protected MessagingObject()
            : this(MessagingObjectType.Message)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessagingObject"/> class with its type.
        /// </summary>
        /// <param name="type">The type of the messaging object.</param>
        protected MessagingObject(MessagingObjectType type)
            : base()
        {
            Type = type;
        }

        /// <summary>
        /// Constructs an instance of the <see cref="MessagingObject"/> class with its name and type.
        /// </summary>
        /// <param name="name">The name of the messaging object.</param>
        /// <param name="type">The type of the messaging object.</param>
        protected MessagingObject(string name, MessagingObjectType type)
            : this(type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Gets or sets a name representing this messaging object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a unique key representing this object.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the type of the building block in the messaging system.
        /// </summary>
        public MessagingObjectType Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the messaging object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the key to the resource map in configuration that is used for resolving
        /// resources.
        /// </summary>
        public string ResourceMapKey { get; set; }

        /// <summary>
        /// Gets a list of resources that comprise the implementation of this messaging object in AIS.
        /// </summary>
        public IList<TargetResourceTemplate> Resources { get; } = new List<TargetResourceTemplate>();

        /// <summary>
        /// Gets a list of resources that represent snippets of code or DSL used to generate a complete
        /// template resource in AIS.
        /// </summary>
        public IList<TargetResourceSnippet> Snippets { get; } = new List<TargetResourceSnippet>();

        /// <summary>
        // Gets a dictionary of arbitrary messaging properties and their values used for template generation.
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
    }
}
