// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer
{
    /// <summary>
    /// Defines an interface that represents a renderer for snippet files.
    /// </summary>
    public interface ISnippetRenderer
    {
        /// <summary>
        /// Renders a snippet using a Liquid template engine.
        /// </summary>
        /// <param name="snippetContent">The snippet content to render.</param>
        /// <param name="model">The model used to provide properties to snippet Liquid templates.</param>
        /// <param name="messagingObject">An optional messaging object to add to the variable context, accessible to snippets.</param>
        /// <param name="resourceTemplate">An optional resource template object to add to the variable context, accessible to templates.</param>
        /// <param name="resourceSnippet">An optional resource template object to add to the variable context, accessible to snippets.</param>
        /// <param name="workflowObject">A workflow object to add to the variable context, accessible to snippets.</param>
        /// <returns>The rendered snippet content.</returns>
        Task<string> RenderSnippetAsync(string snippetContent, AzureIntegrationServicesModel model, MessagingObject messagingObject = null, TargetResourceTemplate resourceTemplate = null, TargetResourceSnippet resourceSnippet = null, WorkflowObject workflowObject = null);
    }
}
