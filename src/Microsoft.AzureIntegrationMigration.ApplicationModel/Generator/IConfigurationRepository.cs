using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Generator
{
    /// <summary>
    /// Defines an interface that represents a repository for YAML configuration files.
    /// </summary>
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Gets a collection of YAML objects representing the resource configuration.
        /// </summary>
        /// <param name="path">The path to the YAML files.</param>
        /// <returns>A collection of YAML objects.</returns>
        IList<YamlStream> GetConfiguration(string path);

        /// <summary>
        /// Renders configuration files in the source path using the Liquid template engine
        /// and stores the rendered files in the target path.
        /// </summary>
        /// <param name="model">The model used to provide properties to Liquid templates.</param>
        /// <param name="sourcePath">The path where the configuration files are stored.</param>
        /// <param name="targetPath">The path where rendered configuration files will be stored.</param>
        /// <returns>A task used to await the operation.</returns>
        Task RenderConfigurationAsync(AzureIntegrationServicesModel model, string sourcePath, string targetPath);
    }
}
