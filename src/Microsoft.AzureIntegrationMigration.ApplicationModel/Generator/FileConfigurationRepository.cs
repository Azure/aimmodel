// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Resources;
using Microsoft.Extensions.Logging;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Generator
{
    /// <summary>
    /// Defines a class that implements a file based repository for YAML configuration files.
    /// </summary>
    public class FileConfigurationRepository : IConfigurationRepository
    {
        /// <summary>
        /// Defines the allowable extensions for YAML configuration files.
        /// </summary>
        private readonly string[] _configurationFileExtensions = new string[] { ".yaml", ".yml" };

        /// <summary>
        /// Defines the allowable extensions for Liquid template versions of YAML configuration files.
        /// </summary>
        private readonly string[] _configurationLiquidFileExtensions = new string[] { ".liquid" };

        /// <summary>
        /// Defines a template renderer.
        /// </summary>
        private readonly ITemplateRenderer _renderer;

        /// <summary>
        /// Defines a logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs a new instance of the <see cref="FileConfigurationRepository"/> class with a renderer and a logger.
        /// </summary>
        /// <param name="logger"></param>
        public FileConfigurationRepository(ITemplateRenderer renderer, ILogger logger)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a collection of YAML objects representing the resource configuration.
        /// </summary>
        /// <param name="path">The path to the YAML files.</param>
        /// <returns>A collection of YAML objects.</returns>
        public IList<YamlStream> GetConfiguration(string path)
        {
            _ = path ?? throw new ArgumentNullException(nameof(path));

            _logger.LogDebug(TraceMessages.LoadingConfigurationFiles, path);

            var config = new List<YamlStream>();

            var dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists)
            {
                // Find all YAML configuration files
                var files = Directory.GetFiles(path, "*").Select(f => new FileInfo(f)).Where(f => _configurationFileExtensions.Contains(f.Extension));
                if (files != null && files.Any())
                {
                    foreach (var file in files)
                    {
                        using (var reader = new StreamReader(file.FullName))
                        {
                            var yaml = new YamlStream();
                            yaml.Load(reader);
                            config.Add(yaml);
                        }
                    }
                }

                return config;
            }
            else
            {
                // Doesn't exist, cannot load configuration files
                throw new DirectoryNotFoundException(string.Format(CultureInfo.CurrentCulture, ErrorMessages.ConfigurationDirectoryNotFound, path));
            }
        }

        /// <summary>
        /// Renders configuration files in the source path using the Liquid template engine
        /// and stores the rendered files in the target path.
        /// </summary>
        /// <param name="model">The model used to provide properties to Liquid templates.</param>
        /// <param name="sourcePath">The path where the configuration files are stored.</param>
        /// <param name="targetPath">The path where rendered configuration files will be stored.</param>
        /// <returns>A task used to await the operation.</returns>
        public async Task RenderConfigurationAsync(AzureIntegrationServicesModel model, string sourcePath, string targetPath)
        {
            _ = model ?? throw new ArgumentNullException(nameof(model));
            _ = sourcePath ?? throw new ArgumentNullException(nameof(sourcePath));
            _ = targetPath ?? throw new ArgumentNullException(nameof(targetPath));

            _logger.LogDebug(TraceMessages.RenderingConfigurationFiles, sourcePath, targetPath);

            var sourceDirInfo = new DirectoryInfo(sourcePath);
            if (sourceDirInfo.Exists)
            {
                // Find all Liquid YAML configuration files
                var files = Directory.GetFiles(sourcePath, "*").Select(f => new FileInfo(f)).Where(f => _configurationLiquidFileExtensions.Contains(f.Extension));
                if (files != null && files.Any())
                {
                    // Create output path if some directories don't exist
                    var targetDirInfo = new DirectoryInfo(targetPath);
                    if (!targetDirInfo.Exists)
                    {
                        Directory.CreateDirectory(targetDirInfo.FullName);
                    }

                    foreach (var file in files)
                    {
                        using (var reader = new StreamReader(file.FullName))
                        {
                            // Read file
                            var config = await reader.ReadToEndAsync().ConfigureAwait(false);

                            // Render file content using Liquid template engine
                            var renderedConfig = await _renderer.RenderTemplateAsync(config, model).ConfigureAwait(false);

                            // Write rendered content to target file (stripping .liquid from filename)
                            var targetFile = new FileInfo(Path.Combine(targetDirInfo.FullName, Path.GetFileNameWithoutExtension(file.FullName)));
                            using (var writer = new StreamWriter(targetFile.FullName, false))
                            {
                                await writer.WriteAsync(renderedConfig).ConfigureAwait(false);
                            }
                        }
                    }
                }
            }
            else
            {
                // Doesn't exist, cannot render configuration files
                throw new DirectoryNotFoundException(string.Format(CultureInfo.CurrentCulture, ErrorMessages.ConfigurationDirectoryNotFound, sourcePath));
            }
        }
    }
}
