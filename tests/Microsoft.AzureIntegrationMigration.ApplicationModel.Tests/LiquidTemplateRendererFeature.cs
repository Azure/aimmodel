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
    /// Defines the test spec for the <see cref="LiquidTemplateRenderer"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "xBehave syntax style.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is handled by xBehave.net and the background attribute.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Not needed for tests.")]
    public class LiquidTemplateRendererFeature
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
        /// Defines template content.
        /// </summary>
        private readonly string _templateContent = "test template for environment: {{ model.migration_target.deployment_environment }}";

        /// <summary>
        /// Defines template content with a custom function.
        /// </summary>
        private readonly string _templateFunctionContent = "{%- assign resource_template = find_resource_template model 'topicChannelAzureServiceBusStandard' -%}{{ resource_template.resource_name }}";

        /// <summary>
        /// Defines template content with a message variable.
        /// </summary>
        private readonly string _templateMessageContent = "{{ message.name }}";

        /// <summary>
        /// Defines template content with a channel variable.
        /// </summary>
        private readonly string _templateChannelContent = "{{ channel.name }}";

        /// <summary>
        /// Defines template content with an intermediary variable.
        /// </summary>
        private readonly string _templateIntermediaryContent = "{{ intermediary.name }}";

        /// <summary>
        /// Defines template content with an endpoint variable.
        /// </summary>
        private readonly string _templateEndpointContent = "{{ endpoint.name }}";

        /// <summary>
        /// Defines template content with a resource template variable.
        /// </summary>
        private readonly string _templateResourceTemplateContent = "{{ resource_template.resource_name }}";

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
        public void ConstructWithNullLogger(ITemplateRenderer renderer, ILogger logger, Exception e)
        {
            "Given a null renderer"
                .x(() => renderer.Should().BeNull());

            "And a null logger"
                .x(() => logger.Should().BeNull());

            "When constructing the renderer with null logger"
                .x(() => e = Record.Exception(() => new LiquidTemplateRenderer(logger)));

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
        public void ConstructWithSuccess(ITemplateRenderer renderer, ILogger logger, Exception e)
        {
            "Given a null renderer"
                .x(() => renderer.Should().BeNull());

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When constructing the renderer"
                .x(() => e = Record.Exception(() => new LiquidTemplateRenderer(logger)));

            "Then the constructor should succeed"
                .x(() => e.Should().BeNull());
        }

        #endregion

        #region RenderConfigurationAsync Scenarios

        /// <summary>
        /// Scenario tests that the render succeeds.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithSuccess(ITemplateRenderer renderer, AzureIntegrationServicesModel model, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a template content"
                .x(() => templateContent = _templateContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.ContainAny("test template for environment: dev").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render succeeds when a message messaging object is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="messagingObject">The messaging object.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithMessageWithSuccess(ITemplateRenderer renderer, AzureIntegrationServicesModel model, MessagingObject messagingObject, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a messaging object"
                .x(() => messagingObject = model.FindMessagingObject("ContosoMessageBus:AppA:PurchaseOrderFlatFile").messagingObject);

            "And a template content"
                .x(() => templateContent = _templateMessageContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model, messagingObject)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.ContainAny("PurchaseOrderFlatFile").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render succeeds when a channel messaging object is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="messagingObject">The messaging object.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithChannelWithSuccess(ITemplateRenderer renderer, AzureIntegrationServicesModel model, MessagingObject messagingObject, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a messaging object"
                .x(() => messagingObject = model.FindMessagingObject("ContosoMessageBus:System:MessageBox").messagingObject);

            "And a template content"
                .x(() => templateContent = _templateChannelContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model, messagingObject)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.ContainAny("MessageBox").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render succeeds when an intermediary messaging object is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="messagingObject">The messaging object.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithIntermediaryWithSuccess(ITemplateRenderer renderer, AzureIntegrationServicesModel model, MessagingObject messagingObject, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a messaging object"
                .x(() => messagingObject = model.FindMessagingObject("ContosoMessageBus:System:MessageAgent").messagingObject);

            "And a template content"
                .x(() => templateContent = _templateIntermediaryContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model, messagingObject)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.ContainAny("MessageAgent").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render succeeds when an endpoint messaging object is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="messagingObject">The messaging object.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithEndpointWithSuccess(ITemplateRenderer renderer, AzureIntegrationServicesModel model, MessagingObject messagingObject, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a messaging object"
                .x(() => messagingObject = model.FindMessagingObject("ContosoMessageBus:System:FtpReceive").messagingObject);

            "And a template content"
                .x(() => templateContent = _templateEndpointContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model, messagingObject)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.ContainAny("FtpReceive").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render succeeds when a resource template object is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="messagingObject">The messaging object.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithResourceTemplateWithSuccess(ITemplateRenderer renderer, AzureIntegrationServicesModel model, MessagingObject messagingObject, TargetResourceTemplate resourceTemplate, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a messaging object"
                .x(() => messagingObject = model.FindMessagingObject("ContosoMessageBus:System:FtpReceive").messagingObject);

            "And a resource template object"
                .x(() => resourceTemplate = new TargetResourceTemplate() { ResourceName = "endpointFtpReceiveLogicAppConsumption" });

            "And a template content"
                .x(() => templateContent = _templateResourceTemplateContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model, null, resourceTemplate)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.ContainAny("endpointFtpReceiveLogicAppConsumption").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render warns when a missing messaging object is passed.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="messagingObject">The messaging object.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithMissingMessagingObjectWithWarning(ITemplateRenderer renderer, AzureIntegrationServicesModel model, MessagingObject messagingObject, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a missing messaging object"
                .x(() => messagingObject = new TopicChannel("MissingChannel") { Key = "MessageBus:MissingApp:MissingChannel" });

            "And a template content"
                .x(() => templateContent = _templateChannelContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model, messagingObject)));

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
        /// Scenario tests that the render succeeds when using a template with a custom function.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithFunctionWithSuccess(ITemplateRenderer renderer, AzureIntegrationServicesModel model, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a template content"
                .x(() => templateContent = _templateFunctionContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the rendered content should have expected values from the model"
                .x(() =>
                {
                    renderedContent.Should().NotBeNull().And.Be("messageBox").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that an error is raised when the model is null.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithModelNullError(ITemplateRenderer renderer, AzureIntegrationServicesModel model, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a null model"
                .x(() => model.Should().BeNull());

            "And a template content"
                .x(() => templateContent = _templateContent);

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model)));

            "Then the render should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("model"));

            "And the rendered content should not have a value"
                .x(() => renderedContent.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that an error is raised when the template content is null.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="renderedContent">The rendered content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderTemplateAsyncWithTemplateContentNullError(ITemplateRenderer renderer, AzureIntegrationServicesModel model, string templateContent, string renderedContent, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a null template content"
                .x(() => templateContent.Should().BeNull());

            "When rendering the template"
                .x(async () => e = await Record.ExceptionAsync(async () => renderedContent = await renderer.RenderTemplateAsync(templateContent, model)));

            "Then the render should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("templateContent"));

            "And the rendered content should not have a value"
                .x(() => renderedContent.Should().BeNull());
        }

        #endregion
    }
}
