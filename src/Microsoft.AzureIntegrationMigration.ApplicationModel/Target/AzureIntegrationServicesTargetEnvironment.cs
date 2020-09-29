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
        /// The default target is the AIS consumption service.
        /// </summary>
        Consumption = 0,

        /// <summary>
        /// The target is the isolated, dedicated Azure Integration Service Environment (ISE).
        /// </summary>
        Ise = 1
    }
}
