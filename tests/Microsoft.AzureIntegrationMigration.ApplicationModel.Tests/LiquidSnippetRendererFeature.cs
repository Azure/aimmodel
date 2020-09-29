// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using FluentAssertions;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Generator;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages;
using Microsoft.Extensions.Logging;
using Moq;
using Xbehave;
using Xunit;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Tests
{
    /// <summary>
    /// Defines the test spec for the <see cref="LiquidSnippetRenderer"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "xBehave syntax style.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is handled by xBehave.net and the background attribute.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Not needed for tests.")]
    public class LiquidSnippetRendererFeature
    {
        /// <summary>
        /// Defines a mocked logger object.
        /// </summary>
        private Mock<ILogger> _mockLogger;

        /// <summary>
        /// Defines a model for rendering.
        /// </summary>
        private AzureIntegrationServicesModel _model;

        /// <summary>
        /// Defines snippet content.
        /// </summary>
        private readonly string _snippetContent = @"{
            ""StepName:_{{ workflow_object.name | replace: "" "", ""_"" }}"": {
                ""type"": ""JavaScriptCode"",
                ""inputs"": {
                    ""code"": ""var step = \""StepName: {{ workflow_object.name }}\"";\r\n\r\nreturn step;""
                    },
                    ""runAfter"": {}
            }
        }";

        /// <summary>
        /// Defines a template containing application name.
        /// </summary>
        private readonly string _missingProcessManagerSnippet = "{{ application.name }}";

        #region Before Each Scenario

        /// <summary>
        /// Sets up state before each scenario.
        /// </summary>
        [Background]
        public void Setup()
        {
            "Given a new mock logger"
                .x(() =>
                {
                    _mockLogger = new Mock<ILogger>();
                    _mockLogger.Setup(l => l.IsEnabled(It.IsAny<LogLevel>())).Returns(true);
                });

            "Given a model"
                .x(() => _model = TestHelper.GetModelWithTargetResources());
        }

        #endregion

        #region Constructor Scenarios

        /// <summary>
        /// Scenario tests that the object construction throws an exception when null logger is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithNullLogger(ISnippetRenderer renderer, ILogger logger, Exception e)
        {
            "Given a null renderer"
                .x(() => renderer.Should().BeNull());

            "And a null logger"
                .x(() => logger.Should().BeNull());

            "When constructing the renderer with null logger"
                .x(() => e = Record.Exception(() => new LiquidSnippetRenderer(logger)));

            "Then the constructor should throw an exception"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("logger"));
        }

        /// <summary>
        /// Scenario tests that the object can be constructed successfully.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithSuccess(ISnippetRenderer renderer, ILogger logger, Exception e)
        {
            "Given a null renderer"
                .x(() => renderer.Should().BeNull());

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When constructing the renderer"
                .x(() => e = Record.Exception(() => new LiquidSnippetRenderer(logger)));

            "Then the constructor should succeed"
                .x(() => e.Should().BeNull());
        }

        #endregion

        #region RenderSnippetAsync Scenarios

        /// <summary>
        /// Scenario tests that the render succeeds.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="processManager">The process manager.</param>
        /// <param name="resourceTemplate">The target resource template.</param>
        /// <param name="resourceSnippet">The target resource snippet.</param>
        /// <param name="workflowObject">The workflow object.</param>
        /// <param name="snippetContent">The snippet content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderSnippetAsyncWithSuccess(ISnippetRenderer renderer, AzureIntegrationServicesModel model, ProcessManager processManager, TargetResourceTemplate resourceTemplate, TargetResourceSnippet resourceSnippet, WorkflowObject workflowObject, string snippetContent, string renderedContent, Exception e)
        {
            "Given a snippet renderer"
                .x(() => renderer = new LiquidSnippetRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a process manager"
                .x(() =>
                {
                    var messagingObject = model.FindMessagingObject("ContosoMessageBus:AppA:FtpTransformWorkflow");
                    messagingObject.messagingObject.Should().NotBeNull().And.BeOfType(typeof(ProcessManager));
                    processManager = (ProcessManager)messagingObject.messagingObject;
                    resourceTemplate = processManager.Resources.First();
                    resourceSnippet = processManager.Snippets.First();
                });

            "And a workflow object"
                .x(() =>
                {
                    workflowObject = processManager.WorkflowModel.Activities.First();
                });

            "And a snippet content"
                .x(() => snippetContent = _snippetContent);

            "When rendering the snippet"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderSnippetAsync(snippetContent, model, processManager, resourceTemplate, resourceSnippet, workflowObject)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.ContainAny($"StepName:_{workflowObject.Name.Replace(" ", "_", StringComparison.InvariantCulture)}").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render warns when a missing messaging object is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="processManager">The process manager.</param>
        /// <param name="missingProcessManager">A process manager that doesn't exist in the model.</param>
        /// <param name="resourceTemplate">The target resource template.</param>
        /// <param name="resourceSnippet">The target resource snippet.</param>
        /// <param name="workflowObject">The workflow object.</param>
        /// <param name="snippetContent">The snippet content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderSnippetAsyncWithMissingMessagingObjectWithWarning(ISnippetRenderer renderer, AzureIntegrationServicesModel model, ProcessManager processManager, ProcessManager missingProcessManager, TargetResourceTemplate resourceTemplate, TargetResourceSnippet resourceSnippet, WorkflowObject workflowObject, string snippetContent, string renderedContent, Exception e)
        {
            "Given a snippet renderer"
                .x(() => renderer = new LiquidSnippetRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a missing process manager"
                .x(() =>
                {
                    missingProcessManager = new ProcessManager("MissingProcessManager") { Key = "MissingProcessManager" };
                });

            "And a workflow object"
                .x(() =>
                {
                    var messagingObject = model.FindMessagingObject("ContosoMessageBus:AppA:FtpTransformWorkflow");
                    messagingObject.messagingObject.Should().NotBeNull().And.BeOfType(typeof(ProcessManager));
                    processManager = (ProcessManager)messagingObject.messagingObject;
                    resourceTemplate = processManager.Resources.First();
                    resourceSnippet = processManager.Snippets.First();
                    workflowObject = processManager.WorkflowModel.Activities.First();
                });

            "And a snippet content"
                .x(() => snippetContent = _missingProcessManagerSnippet);

            "When rendering the snippet"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderSnippetAsync(snippetContent, model, missingProcessManager, resourceTemplate, resourceSnippet, workflowObject)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should not have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.Be("").And.NotContainAny("{{").And.NotContainAny("}}");

                    // Verify warning was raised
                    _mockLogger.Verify(l => l.Log(
                        It.Is<LogLevel>(l => l == LogLevel.Warning),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("does not appear in the target model", StringComparison.CurrentCultureIgnoreCase)),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
                });
        }

        /// <summary>
        /// Scenario tests that an error is raised when the model is null.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="processManager">The process manager.</param>
        /// <param name="resourceTemplate">The target resource template.</param>
        /// <param name="resourceSnippet">The target resource snippet.</param>
        /// <param name="workflowObject">The workflow object.</param>
        /// <param name="snippetContent">The snippet content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithModelNullError(ISnippetRenderer renderer, AzureIntegrationServicesModel model, ProcessManager processManager, TargetResourceTemplate resourceTemplate, TargetResourceSnippet resourceSnippet, WorkflowObject workflowObject, string snippetContent, string renderedContent, Exception e)
        {
            "Given a snippet renderer"
                .x(() => renderer = new LiquidSnippetRenderer(_mockLogger.Object));

            "And a null model"
                .x(() => model.Should().BeNull());

            "And a process manager"
                .x(() =>
                {
                    var messagingObject = _model.FindMessagingObject("ContosoMessageBus:AppA:FtpTransformWorkflow");
                    messagingObject.messagingObject.Should().NotBeNull().And.BeOfType(typeof(ProcessManager));
                    processManager = (ProcessManager)messagingObject.messagingObject;
                    resourceTemplate = processManager.Resources.First();
                    resourceSnippet = processManager.Snippets.First();
                });

            "And a workflow object"
                .x(() =>
                {
                    workflowObject = processManager.WorkflowModel.Activities.First();
                });

            "And a snippet content"
                .x(() => snippetContent = _snippetContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderSnippetAsync(snippetContent, model, processManager, resourceTemplate, resourceSnippet, workflowObject)));

            "Then the render should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("model"));

            "And the rendered content should not have a value"
                .x(() => renderedContent.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that an error is raised when the snippet content is null.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="processManager">The process manager.</param>
        /// <param name="resourceTemplate">The target resource template.</param>
        /// <param name="resourceSnippet">The target resource snippet.</param>
        /// <param name="workflowObject">The workflow object.</param>
        /// <param name="snippetContent">The snippet content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithSnippetContentNullError(ISnippetRenderer renderer, AzureIntegrationServicesModel model, ProcessManager processManager, TargetResourceTemplate resourceTemplate, TargetResourceSnippet resourceSnippet, WorkflowObject workflowObject, string snippetContent, string renderedContent, Exception e)
        {
            "Given a snippet renderer"
                .x(() => renderer = new LiquidSnippetRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a process manager"
                .x(() =>
                {
                    var messagingObject = model.FindMessagingObject("ContosoMessageBus:AppA:FtpTransformWorkflow");
                    messagingObject.messagingObject.Should().NotBeNull().And.BeOfType(typeof(ProcessManager));
                    processManager = (ProcessManager)messagingObject.messagingObject;
                    resourceTemplate = processManager.Resources.First();
                    resourceSnippet = processManager.Snippets.First();
                });

            "And a workflow object"
                .x(() =>
                {
                    workflowObject = processManager.WorkflowModel.Activities.First();
                });

            "And null snippet content"
                .x(() => snippetContent = null);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderSnippetAsync(snippetContent, model, processManager, resourceTemplate, resourceSnippet, workflowObject)));

            "Then the render should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("snippetContent"));

            "And the rendered content should not have a value"
                .x(() => renderedContent.Should().BeNull());
        }

        #endregion
    }
}
