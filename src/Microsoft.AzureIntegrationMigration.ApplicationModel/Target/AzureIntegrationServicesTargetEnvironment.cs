// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Defines an enumeration of allowable values for the AIS target environment.
    /// </summary>
    public enum AzureIntegrationServicesTargetEnvironment
    {
        /// <summary>
        /// The target is the multi-tenant Logic Apps Consumption environment.
        /// </summary>
        Consumption = 0,

        /// <summary>
        /// The target is the single-tenant Logic Apps Standard environment.
        /// </summary>
        Standard = 1,

        /// <summary>
        /// The target is the lite-version of the multi-tenant Logic Apps Consumption environment.
        /// </summary>
        ConsumptionLite = 2,

        /// <summary>
        /// The target is the lite-version of the single-tenant Logic Apps Standard environment.
        /// </summary>
        StandardLite = 3
    }
}
