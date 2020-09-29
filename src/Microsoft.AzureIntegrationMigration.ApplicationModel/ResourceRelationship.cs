// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel
{
    /// <summary>
    /// Defines the relationship between the current resource and another resource.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ResourceRelationship
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRelationship"/> class.
        /// </summary>
        public ResourceRelationship()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRelationship"/> class.
        /// </summary>
        /// <param name="refId">The refId of the other resource.</param>
        /// <param name="resourceRelationshipType">The type of relationship as seen from this resource.</param>
        public ResourceRelationship(string refId, ResourceRelationshipType resourceRelationshipType)
        {
            ResourceRefId = refId;
            ResourceRelationshipType = resourceRelationshipType;
        }

        /// <summary>
        /// Gets or sets the RefId of the associated source resource.
        /// </summary>
        public string ResourceRefId { get; set; }

        /// <summary>
        /// Gets or sets the type of relationship between the resources.
        /// </summary>
        public ResourceRelationshipType ResourceRelationshipType { get; set; }

        /// <summary>
        // Gets a dictionary of arbitrary relationship properties and their values.
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();
    }
}
