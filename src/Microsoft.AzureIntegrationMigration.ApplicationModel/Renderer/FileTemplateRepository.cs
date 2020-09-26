using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Resources;
using Microsoft.Extensions.Logging;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer
{
    /// <summary>
    /// Defines a class that represents a file based repository for template files.
    /// </summary>
    public class FileTemplateRepository : ITemplateRepository
    {
        /// <summary>
        /// Defines a logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs a new instance of the <see cref="FileTemplateRepository"/> class with a logger.
        /// </summary>
        /// <param name="logger"></param>
        public FileTemplateRepository(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Loads a template from a file.
        /// </summary>
        /// <param name="filePath">The path where the template file is stored.</param>
        /// <returns>The template content.</returns>
        public async Task<string> LoadTemplateAsync(string filePath)
        {
            _ = filePath ?? throw new ArgumentNullException(nameof(filePath));

            _logger.LogTrace(TraceMessages.LoadingTemplateFile, filePath);

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                using (var reader = new StreamReader(fileInfo.FullName))
                {
                    var templateContent = await reader.ReadToEndAsync().ConfigureAwait(false);
                    return templateContent;
                }
            }
            else
            {
                // Doesn't exist, cannot load template
                throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, ErrorMessages.TemplateFileNotFound, filePath), fileInfo.FullName);
            }
        }

        /// <summary>
        /// Saves a template to a file.
        /// </summary>
        /// <param name="filePath">The path where the template file will be stored.</param>
        /// <param name="templateContent">The template to store.</param>
        public async Task SaveTemplateAsync(string filePath, string templateContent)
        {
            _ = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _ = templateContent ?? throw new ArgumentNullException(nameof(templateContent));

            _logger.LogTrace(TraceMessages.SavingTemplateFile, filePath);

            var fileInfo = new FileInfo(filePath);

            // Create output path if some directories don't exist            
            if (!Directory.Exists(fileInfo.FullName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }

            using (var writer = new StreamWriter(fileInfo.FullName, false))
            {
                await writer.WriteAsync(templateContent).ConfigureAwait(false);
            }
        }
    }
}
