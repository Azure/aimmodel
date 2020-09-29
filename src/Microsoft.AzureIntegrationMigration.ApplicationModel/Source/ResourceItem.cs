// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Source
{
    /// <summary>
    /// Defines a resource item that has been parsed from a resource definition.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ResourceItem : ResourceNode
    {
        /// <summary>
        /// Gets the properties associated with the resource.
        /// </summary>
        public IDictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets a list of nested resources.
        /// </summary>
        public IList<ResourceItem> Resources { get; } = new List<ResourceItem>();

        /// <summary>
        /// Gets a list of relationships to other resources.
        /// </summary>
        public IList<ResourceRelationship> ResourceRelationships { get; } = new List<ResourceRelationship>();

        /// <summary>
        /// Adds a new relationship to the resource, testing for duplicates.
        /// </summary>
        /// <param name="relationship">The relationship to add.</param>
        public void AddRelationship(ResourceRelationship relationship)
        {
            if (!ResourceRelationships.Where(r => r.ResourceRelationshipType == relationship.ResourceRelationshipType && r.ResourceRefId == relationship.ResourceRefId).Any())
            {
                ResourceRelationships.Add(relationship);
            }
        }
    }
}
