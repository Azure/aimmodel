using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer
{
    /// <summary>
    /// Defines an interface that represents a repository for template files.
    /// </summary>
    public interface ITemplateRepository
    {
        /// <summary>
        /// Loads a template from a file.
        /// </summary>
        /// <param name="filePath">The path where the template file is stored.</param>
        /// <returns>The template content.</returns>
        Task<string> LoadTemplateAsync(string filePath);

        /// <summary>
        /// Saves a template to a file.
        /// </summary>
        /// <param name="filePath">The path where the template file will be stored.</param>
        /// <param name="templateContent">The template to store.</param>
        /// <returns>A task used to await the operation.</returns>
        Task SaveTemplateAsync(string filePath, string templateContent);
    }
}
