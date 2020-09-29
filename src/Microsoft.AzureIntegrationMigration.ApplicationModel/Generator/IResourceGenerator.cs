// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.Runner.Model;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Generator
{
    /// <summary>
    /// Defines an interface that represents a generator that builds target resources
    /// from configuration.
    /// </summary>
    public interface IResourceGenerator
    {
        /// <summary>
        /// Generates target resources from the messaging objects in the target model and
        /// adds the associated resources to the model.
        /// </summary>
        /// <param name="model">The application model.</param>
        /// <param name="config">A list of configuration objects.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task used to await the operation.</returns>
        Task GenerateResourcesAsync(AzureIntegrationServicesModel model, IList<YamlStream> config, CancellationToken token);
    }
}
