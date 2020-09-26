using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines a base class for workflow objects in a workflow model for a process manager intermediary.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class WorkflowObject
    {
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        [JsonProperty(Order = -4)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unique key of the object in the model.
        /// </summary>
        [JsonProperty(Order = -3)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the represented type of the object, such as Receive or Send.
        /// </summary>
        [JsonProperty(Order = -2)]
        public string Type { get; set; }

        /// <summary>
        /// Gets a collection of arbitrary properties associated with the object.
        /// </summary>
        [JsonProperty(Order = -1)]
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
    }
}
