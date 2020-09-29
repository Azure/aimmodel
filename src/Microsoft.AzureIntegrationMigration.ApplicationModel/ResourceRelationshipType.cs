// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel
{
    /// <summary>
    /// Enum describing the type of association between two resources.
    /// </summary>
    [Serializable]
    public enum ResourceRelationshipType
    {
        /// <summary>
        /// The relationship is unknown or unassigned.
        /// </summary>
        Unknown,

        /// <summary>
        /// This is the parent of the associated resource.
        /// </summary>
        Parent,

        /// <summary>
        /// This is the child of the associated resource.
        /// </summary>
        Child,

        /// <summary>
        /// This resource is called by another resource.
        /// </summary>
        CalledBy,

        /// <summary>
        /// This resource makes calls to another resource.
        /// </summary>
        CallsTo,

        /// <summary>
        /// This resource is referenced by another resource.
        /// </summary>
        ReferencedBy,

        /// <summary>
        /// This resource makes references to another resource.
        /// </summary>
        ReferencesTo,

        /// <summary>
        /// This resource has a general association to another resource.
        /// </summary>
        Association,
    }
}
