// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer
{
    /// <summary>
    /// Defines an interface that represents a renderer for template files.
    /// </summary>
    public interface ITemplateRenderer
    {
        /// <summary>
        /// Renders a template using a Liquid template engine.
        /// </summary>
        /// <param name="templateContent">The template content to render.</param>
        /// <param name="model">The model used to provide properties to Liquid templates.</param>
        /// <param name="messagingObject">An optional messaging object to add to the variable context, accessible to templates.</param>
        /// <param name="resourceTemplate">An optional resource template object to add to the variable context, accessible to templates.</param>
        /// <returns>The rendered template content.</returns>
        Task<string> RenderTemplateAsync(string templateContent, AzureIntegrationServicesModel model, MessagingObject messagingObject = null, TargetResourceTemplate resourceTemplate = null);
    }
}
