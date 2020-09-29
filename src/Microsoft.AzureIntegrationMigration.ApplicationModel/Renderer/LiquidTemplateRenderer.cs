// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Resources;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer
{
    /// <summary>
    /// Defines a class that uses a Liquid template engine to render the template.
    /// </summary>
    public class LiquidTemplateRenderer : ITemplateRenderer
    {
        /// <summary>
        /// Defines a template context for rendering a template using Liquid syntax.
        /// </summary>
        private readonly LiquidTemplateContext _context;

        /// <summary>
        /// Defines a logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructs a new instance of the <see cref="LiquidTemplateRenderer"/> class with a logger.
        /// </summary>
        public LiquidTemplateRenderer(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Load functions to support Liquid rendering
            var funcScriptObject = new ScriptObject();
            funcScriptObject.Import(typeof(CustomLiquidFunctions));

            // Create template context (disable loop limit)
            _context = new LiquidTemplateContext() { LoopLimit = int.MaxValue };
            _context.PushGlobal(funcScriptObject);
        }

        /// <summary>
        /// Renders a template using a Liquid template engine.
        /// </summary>
        /// <param name="templateContent">The template content to render.</param>
        /// <param name="model">The model used to provide properties to Liquid templates.</param>
        /// <param name="messagingObject">An optional messaging object to add to the variable context, accessible to templates.</param>
        /// <param name="resourceTemplate">An optional resource template object to add to the variable context, accessible to templates.</param>
        /// <returns>The rendered template content.</returns>
        public async Task<string> RenderTemplateAsync(string templateContent, AzureIntegrationServicesModel model, MessagingObject messagingObject = null, TargetResourceTemplate resourceTemplate = null)
        {
            _ = model ?? throw new ArgumentNullException(nameof(model));
            _ = templateContent ?? throw new ArgumentNullException(nameof(templateContent));

            // Create variables on script object, to be accessible to Liquid templates
            var scriptObject = new ScriptObject
            {
                ["model"] = model
            };

            // Is there a messaging object?
            if (messagingObject != null)
            {
                var messagingObjects = model.FindMessagingObject(messagingObject.Key);
                if (messagingObjects.messageBus != null)
                {
                    scriptObject["message_bus"] = messagingObjects.messageBus;

                    if (messagingObjects.application != null)
                    {
                        scriptObject["application"] = messagingObjects.application;
                    }

                    if (messagingObjects.messagingObject != null)
                    {
                        scriptObject["messaging_object"] = messagingObjects.messagingObject;

                        switch (messagingObjects.messagingObject.Type)
                        {
                            case MessagingObjectType.Message:
                                scriptObject["message"] = (Message)messagingObjects.messagingObject;
                                break;

                            case MessagingObjectType.Channel:
                                scriptObject["channel"] = (Channel)messagingObjects.messagingObject;
                                break;

                            case MessagingObjectType.Intermediary:
                                scriptObject["intermediary"] = (Intermediary)messagingObjects.messagingObject;
                                break;

                            case MessagingObjectType.Endpoint:
                                scriptObject["endpoint"] = (Endpoint)messagingObjects.messagingObject;
                                break;
                        }
                    }
                }
                else
                {
                    // Should never happen, unless the messaging object is not attached to the target model
                    _logger.LogWarning(WarningMessages.MessagingObjectMissingInModel, messagingObject.Key);
                }
            }

            // Is there a resource template?
            if (resourceTemplate != null)
            {
                scriptObject["resource_template"] = resourceTemplate;
            }

            // Push variables onto the context
            _context.PushGlobal(scriptObject);

            try
            {
                // Render template
                var template = Template.ParseLiquid(templateContent);
                var renderedContent = await template.RenderAsync(_context).ConfigureAwait(false);
                return renderedContent;
            }
            finally
            {
                // Pop model from context stack
                _context.PopGlobal();
            }
        }
    }
}
