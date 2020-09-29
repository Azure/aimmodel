// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines the categories of intermediaries that exist as discrete steps in the processing path of
    /// the message delivery between applications.
    /// </summary>
    public enum IntermediaryType
    {
        /// <summary>
        /// Represents a category of intermediaries that receives and sends messages from and to channels
        /// based on a number of different strategies, such as a simple enqueue or using a complex set of
        /// decisions and steps, such as a workflow.
        /// </summary>
        MessageRouter,

        /// <summary>
        /// Represents a category of intermediaries that process messages such as message translation,
        /// performing validation or security checks like digital signature checks, maybe in a pipe
        /// with set of sequential filters.
        /// </summary>
        MessageProcessor
    }
}
