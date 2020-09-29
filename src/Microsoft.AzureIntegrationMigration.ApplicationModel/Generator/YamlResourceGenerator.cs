// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Report;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Resources;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.Runner.Model;
using Microsoft.Extensions.Logging;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Generator
{
    /// <summary>
    /// Defines a class that implements a generator that builds target resources
    /// from YAML configuration streams.
    /// </summary>
    public class YamlResourceGenerator : IResourceGenerator
    {
        /// <summary>
        /// Defines a logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs a new instance of the <see cref="YamlResourceGenerator"/> class with a logger.
        /// </summary>
        public YamlResourceGenerator(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Generates target resources from the messaging objects in the target model and
        /// adds the associated resources to the model.
        /// </summary>
        /// <param name="model">The application model.</param>
        /// <param name="config">A list of configuration objects.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task used to await the operation.</returns>
        public Task GenerateResourcesAsync(AzureIntegrationServicesModel model, IList<YamlStream> config, CancellationToken token)
        {
            _ = model ?? throw new ArgumentNullException(nameof(model));
            _ = config ?? throw new ArgumentNullException(nameof(config));

            // Parse YAML streams and build up target resources in the target model
            if (model.MigrationTarget.MessageBus != null)
            {
                // Message Bus
                PopulateTargetResources(model, config, model.MigrationTarget.MessageBus);

                foreach (var application in model.MigrationTarget.MessageBus.Applications)
                {
                    // Cancel if requested
                    token.ThrowIfCancellationRequested();

                    // Application
                    PopulateTargetResources(model, config, application);

                    foreach (var message in application.Messages)
                    {
                        // Messages
                        PopulateTargetResources(model, config, message);
                    }

                    foreach (var channel in application.Channels)
                    {
                        // Channels
                        PopulateTargetResources(model, config, channel);
                    }

                    foreach (var intermediary in application.Intermediaries)
                    {
                        // Intermediaries
                        PopulateTargetResources(model, config, intermediary);
                    }

                    foreach (var endpoint in application.Endpoints)
                    {
                        // Endpoints
                        PopulateTargetResources(model, config, endpoint);
                    }
                }
            }
            else
            {
                _logger.LogWarning(WarningMessages.MessageBusMissingInModel);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Populates the target resources from configuration for the specified messaging object.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="config">The YAML configuration set.</param>
        /// <param name="messagingObject">The messaging object.</param>
        private void PopulateTargetResources(AzureIntegrationServicesModel model, IEnumerable<YamlStream> config, MessagingObject messagingObject)
        {
            if (messagingObject.ResourceMapKey != null)
            {
                _logger.LogDebug(TraceMessages.LoadingTargetResources, messagingObject.Key);

                // Find resource map
                var resourceKeyList = FindResourcesForMap(config, messagingObject.ResourceMapKey);
                if (resourceKeyList != null)
                {
                    foreach (var resourceKeyNode in resourceKeyList)
                    {
                        var resourceKey = (YamlScalarNode)resourceKeyNode;

                        _logger.LogTrace(TraceMessages.LookingForTemplatesForResource, resourceKey.Value);

                        // Find targets for resource
                        var targetsList = FindTargetsForResource(config, resourceKey.Value);
                        if (targetsList != null)
                        {
                            _logger.LogDebug(TraceMessages.FilteringResourcesByTarget, model.MigrationTarget.TargetEnvironment);

                            foreach (var targetNode in targetsList)
                            {
                                var target = (YamlMappingNode)targetNode;

                                // Get target name
                                var targetNameNode = (YamlSequenceNode)target.Children["target"];
                                var targetNames = targetNameNode.Select(t => ((YamlScalarNode)t).Value.ToUpperInvariant());
                                if (targetNames.Contains(model.MigrationTarget.TargetEnvironment.ToString("G").ToUpperInvariant()))
                                {
                                    if (target.Children.ContainsKey("templates"))
                                    {
                                        _logger.LogTrace(TraceMessages.FoundTemplatesForTarget, model.MigrationTarget.TargetEnvironment, resourceKey.Value);

                                        // Get templates
                                        var templateKeyList = (YamlSequenceNode)target.Children["templates"];

                                        foreach (var templateKeyNode in templateKeyList)
                                        {
                                            var templateKey = (YamlScalarNode)templateKeyNode;

                                            var templateNode = FindTemplate(config, templateKey.Value);
                                            if (templateNode != null)
                                            {
                                                // Create target resource
                                                var targetResourceTemplate = CreateTargetResourceTemplate(model, templateKey, templateNode);
                                                messagingObject.Resources.Add(targetResourceTemplate);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _logger.LogTrace(TraceMessages.NoTemplatesForTarget, model.MigrationTarget.TargetEnvironment, resourceKey.Value);
                                    }

                                    if (target.Children.ContainsKey("snippets"))
                                    {
                                        _logger.LogTrace(TraceMessages.FoundSnippetsForTarget, model.MigrationTarget.TargetEnvironment, resourceKey.Value);

                                        // Get snippets
                                        var snippetKeyList = (YamlSequenceNode)target.Children["snippets"];

                                        foreach (var snippetKeyNode in snippetKeyList)
                                        {
                                            var snippetKey = (YamlScalarNode)snippetKeyNode;

                                            var snippetNode = FindSnippet(config, snippetKey.Value);
                                            if (snippetNode != null)
                                            {
                                                // Create target resource
                                                var targetResourceSnippet = CreateTargetResourceSnippet(model, snippetKey, snippetNode);
                                                messagingObject.Snippets.Add(targetResourceSnippet);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _logger.LogTrace(TraceMessages.NoSnippetsForTarget, model.MigrationTarget.TargetEnvironment, resourceKey.Value);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    _logger.LogWarning(WarningMessages.ResourceMapMissingInConfiguration, messagingObject.ResourceMapKey, messagingObject.Name);
                }
            }
            else
            {
                _logger.LogDebug(TraceMessages.ResourceMapKeyNotSpecified, messagingObject.Name, messagingObject.Type);
            }
        }

        /// <summary>
        /// Creates a target resource for the template.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="templateKey">The key of the template.</param>
        /// <param name="templateNode">The template from configuration.</param>
        /// <returns></returns>
        private TargetResourceTemplate CreateTargetResourceTemplate(AzureIntegrationServicesModel model, YamlScalarNode templateKey, YamlMappingNode templateNode)
        {
            // Mandatory fields
            var targetResource = new TargetResourceTemplate()
            {
                TemplateKey = ((YamlScalarNode)templateNode.Children["template"]).Value,
                TemplateType = ((YamlScalarNode)templateNode.Children["templateType"]).Value,
                ResourceName = ((YamlScalarNode)templateNode.Children["resourceName"]).Value,
                ResourceType = ((YamlScalarNode)templateNode.Children["resourceType"]).Value
            };

            // Optional fields
            if (templateNode.Children.ContainsKey("outputPath"))
            {
                targetResource.OutputPath = ((YamlScalarNode)templateNode.Children["outputPath"]).Value;
            }

            // Tags
            if (templateNode.Children.ContainsKey("tags"))
            {
                var templateTags = (YamlSequenceNode)templateNode.Children["tags"];
                if (templateTags != null)
                {
                    foreach (var tag in templateTags)
                    {
                        var tagNode = ((YamlMappingNode)tag).Children.SingleOrDefault();
                        targetResource.Tags.Add(((YamlScalarNode)tagNode.Key).Value, ((YamlScalarNode)tagNode.Value).Value);
                    }
                }
            }

            // Parameters
            if (templateNode.Children.ContainsKey("parameters"))
            {
                var templateParams = (YamlSequenceNode)templateNode.Children["parameters"];
                if (templateParams != null)
                {
                    foreach (var param in templateParams)
                    {
                        var paramNode = ((YamlMappingNode)param).Children.SingleOrDefault();
                        targetResource.Parameters.Add(((YamlScalarNode)paramNode.Key).Value, ((YamlScalarNode)paramNode.Value).Value);
                    }
                }
            }

            // Files
            if (templateNode.Children.ContainsKey("files"))
            {
                var templateFiles = (YamlSequenceNode)templateNode.Children["files"];
                if (templateFiles != null)
                {
                    _logger.LogTrace(TraceMessages.FilteringTemplatesByEnvironment, model.MigrationTarget.DeploymentEnvironment, templateKey.Value);

                    foreach (var templateFileNode in templateFiles)
                    {
                        var templateFile = (YamlMappingNode)templateFileNode;

                        // Filter by deployment environment
                        var envNameNode = (YamlSequenceNode)templateFile.Children["env"];
                        var envNames = envNameNode.Select(t => ((YamlScalarNode)t).Value.ToUpperInvariant());
                        if (envNames.Contains(model.MigrationTarget.DeploymentEnvironment.ToUpperInvariant()))
                        {
                            _logger.LogTrace(TraceMessages.FoundFilesForEnvironment, model.MigrationTarget.DeploymentEnvironment, templateKey.Value);

                            // Get paths
                            var pathsList = (YamlSequenceNode)templateFile.Children["paths"];

                            foreach (var path in pathsList)
                            {
                                targetResource.ResourceTemplateFiles.Add(((YamlScalarNode)path).Value);
                            }
                        }
                    }
                }
            }

            return targetResource;
        }

        /// <summary>
        /// Creates a target resource for the snippet.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="snippetKey">The key of the snippet.</param>
        /// <param name="snippetNode">The snippet from configuration.</param>
        /// <returns></returns>
        private TargetResourceSnippet CreateTargetResourceSnippet(AzureIntegrationServicesModel model, YamlScalarNode snippetKey, YamlMappingNode snippetNode)
        {
            // Mandatory fields
            var targetResource = new TargetResourceSnippet()
            {
                SnippetKey = ((YamlScalarNode)snippetNode.Children["snippet"]).Value,
                SnippetType = ((YamlScalarNode)snippetNode.Children["snippetType"]).Value,
                ResourceName = ((YamlScalarNode)snippetNode.Children["resourceName"]).Value,
                ResourceType = ((YamlScalarNode)snippetNode.Children["resourceType"]).Value
            };

            // Optional fields
            if (snippetNode.Children.ContainsKey("outputPath"))
            {
                targetResource.OutputPath = ((YamlScalarNode)snippetNode.Children["outputPath"]).Value;
            }

            // Parameters
            if (snippetNode.Children.ContainsKey("parameters"))
            {
                var templateParams = (YamlSequenceNode)snippetNode.Children["parameters"];
                if (templateParams != null)
                {
                    foreach (var param in templateParams)
                    {
                        var paramNode = ((YamlMappingNode)param).Children.SingleOrDefault();
                        targetResource.Parameters.Add(((YamlScalarNode)paramNode.Key).Value, ((YamlScalarNode)paramNode.Value).Value);
                    }
                }
            }

            // Files
            if (snippetNode.Children.ContainsKey("files"))
            {
                var snippetFiles = (YamlSequenceNode)snippetNode.Children["files"];
                if (snippetFiles != null)
                {
                    _logger.LogTrace(TraceMessages.FilteringSnippetsByEnvironment, model.MigrationTarget.DeploymentEnvironment, snippetKey.Value);

                    foreach (var snippetFileNode in snippetFiles)
                    {
                        var snippetFile = (YamlMappingNode)snippetFileNode;

                        // Filter by deployment environment
                        var envNameNode = (YamlSequenceNode)snippetFile.Children["env"];
                        var envNames = envNameNode.Select(t => ((YamlScalarNode)t).Value.ToUpperInvariant());
                        if (envNames.Contains(model.MigrationTarget.DeploymentEnvironment.ToUpperInvariant()))
                        {
                            _logger.LogTrace(TraceMessages.FoundSnippetFileForEnvironment, model.MigrationTarget.DeploymentEnvironment, snippetKey.Value);

                            // Get path
                            targetResource.ResourceSnippetFile = ((YamlScalarNode)snippetFile.Children["path"]).Value;
                        }
                    }
                }
            }

            return targetResource;
        }

        /// <summary>
        /// Finds the resource map for a named map in the YAML configuration set.
        /// </summary>
        /// <param name="config">The YAML configuration set.</param>
        /// <param name="resourceMapKey">The key of the resource map to find.</param>
        /// <returns></returns>
        private YamlSequenceNode FindResourcesForMap(IEnumerable<YamlStream> config, string resourceMapKey)
        {
            _logger.LogTrace(TraceMessages.LookingForResourceMap, resourceMapKey);

            foreach (var stream in config)
            {
                foreach (var doc in stream.Documents)
                {
                    var root = doc.RootNode as YamlMappingNode;
                    if (root != null)
                    {
                        YamlNode resourceMapsNode;
                        if (root.Children.TryGetValue(new YamlScalarNode("resourceMaps"), out resourceMapsNode))
                        {
                            var resourceMapList = (YamlSequenceNode)resourceMapsNode;
                            var resourceMapNode = resourceMapList.Where(t => ((YamlScalarNode)((YamlMappingNode)t).Children["map"]).Value == resourceMapKey).Select(t => (YamlMappingNode)t).FirstOrDefault();
                            if (resourceMapNode != null)
                            {
                                _logger.LogTrace(TraceMessages.FoundResourceMap, resourceMapKey);

                                var resources = (YamlSequenceNode)resourceMapNode.Children[new YamlScalarNode("resources")];
                                return resources;
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the targets for a named resource in the YAML configuration set.
        /// </summary>
        /// <param name="config">The YAML configuration set.</param>
        /// <param name="resourceKey">The key of the resource to find.</param>
        /// <returns></returns>
        private YamlSequenceNode FindTargetsForResource(IEnumerable<YamlStream> config, string resourceKey)
        {
            _logger.LogTrace(TraceMessages.LookingForTargetsForResource, resourceKey);

            foreach (var stream in config)
            {
                foreach (var doc in stream.Documents)
                {
                    var root = doc.RootNode as YamlMappingNode;
                    if (root != null)
                    {
                        YamlNode resourcesNode;
                        if (root.Children.TryGetValue(new YamlScalarNode("resources"), out resourcesNode))
                        {
                            var resourceList = (YamlSequenceNode)resourcesNode;
                            var resourceNode = resourceList.Where(t => ((YamlScalarNode)((YamlMappingNode)t).Children["resource"]).Value == resourceKey).Select(t => (YamlMappingNode)t).FirstOrDefault();
                            if (resourceNode != null)
                            {
                                _logger.LogTrace(TraceMessages.FoundTargetsForResource, resourceKey);

                                var targets = (YamlSequenceNode)resourceNode.Children[new YamlScalarNode("targets")];
                                return targets;
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a named template in the YAML configuration set.
        /// </summary>
        /// <param name="config">The YAML configuration set.</param>
        /// <param name="templateKey">The key of the template to find.</param>
        /// <returns>A template node.</returns>
        private YamlMappingNode FindTemplate(IEnumerable<YamlStream> config, string templateKey)
        {
            _logger.LogTrace(TraceMessages.LookingForTemplate, templateKey);

            foreach (var stream in config)
            {
                foreach (var doc in stream.Documents)
                {
                    var root = doc.RootNode as YamlMappingNode;
                    if (root != null)
                    {
                        YamlNode resourceTemplatesNode;
                        if (root.Children.TryGetValue(new YamlScalarNode("resourceTemplates"), out resourceTemplatesNode))
                        {
                            var resourceTemplateList = (YamlSequenceNode)resourceTemplatesNode;
                            var templateNode = resourceTemplateList.Where(t => ((YamlScalarNode)((YamlMappingNode)t).Children["template"]).Value == templateKey).Select(t => (YamlMappingNode)t).FirstOrDefault();
                            if (templateNode != null)
                            {
                                _logger.LogTrace(TraceMessages.FoundTemplate, templateKey);

                                return (YamlMappingNode)templateNode;
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a named snippet in the YAML configuration set.
        /// </summary>
        /// <param name="config">The YAML configuration set.</param>
        /// <param name="snippetKey">The key of the snippet to find.</param>
        /// <returns>A snippet node.</returns>
        private YamlMappingNode FindSnippet(IEnumerable<YamlStream> config, string snippetKey)
        {
            _logger.LogTrace(TraceMessages.LookingForSnippet, snippetKey);

            foreach (var stream in config)
            {
                foreach (var doc in stream.Documents)
                {
                    var root = doc.RootNode as YamlMappingNode;
                    if (root != null)
                    {
                        YamlNode resourceSnippetsNode;
                        if (root.Children.TryGetValue(new YamlScalarNode("resourceSnippets"), out resourceSnippetsNode))
                        {
                            var resourceSnippetList = (YamlSequenceNode)resourceSnippetsNode;
                            var snippetNode = resourceSnippetList.Where(t => ((YamlScalarNode)((YamlMappingNode)t).Children["snippet"]).Value == snippetKey).Select(t => (YamlMappingNode)t).FirstOrDefault();
                            if (snippetNode != null)
                            {
                                _logger.LogTrace(TraceMessages.FoundSnippet, snippetKey);

                                return (YamlMappingNode)snippetNode;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
