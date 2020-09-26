using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Castle.Core.Resource;
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
    /// Defines the test spec for the <see cref="YamlResourceGenerator"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "xBehave syntax style.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is handled by xBehave.net and the background attribute.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Not needed for tests.")]
    public class YamlResourceGeneratorFeature
    {
        /// <summary>
        /// Defines a config path.
        /// </summary>
        private const string OkConfigPath = @"..\..\..\..\scenarios\ok-config";

        /// <summary>
        /// Defines a config path.
        /// </summary>
        private const string OkMultiConfigPath = @"..\..\..\..\scenarios\ok-multi-config";

        /// <summary>
        /// Defines a mocked logger object.
        /// </summary>
        private Mock<ILogger> _mockLogger;

        /// <summary>
        /// Defines a model for rendering.
        /// </summary>
        private AzureIntegrationServicesModel _model;

        /// <summary>
        /// Defines a cancellation token source that is used to create cancellation tokens for async method calls
        /// that support cancellation.
        /// </summary>
        private CancellationTokenSource _source;

        /// <summary>
        /// Defines a temporary file path for renderred template files.
        /// </summary>
        private string _tempOutputTemplatePath;

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
                .x(() => _model = TestHelper.GetModel());

            "Given a new cancellation token source"
                .x(() => _source = new CancellationTokenSource());

            "Given scenario config paths"
                .x(() =>
                {
                    var dirInfo = new DirectoryInfo(OkMultiConfigPath);
                    Directory.Exists(dirInfo.FullName).Should().BeTrue(OkMultiConfigPath + " should exist");
                });

            "Given temp output path for rendered templates"
                .x(() =>
                {
                    _tempOutputTemplatePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    Directory.CreateDirectory(_tempOutputTemplatePath);
                })
                .Teardown(() =>
                {
                    if (Directory.Exists(_tempOutputTemplatePath))
                    {
                        Directory.Delete(_tempOutputTemplatePath, true);
                    }
                });
        }

        #endregion

        #region Constructor Scenarios

        /// <summary>
        /// Scenario tests that the object construction throws an exception when null logger is passed.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithNullLogger(IResourceGenerator generator, ILogger logger, Exception e)
        {
            "Given a null generator"
                .x(() => generator.Should().BeNull());

            "And a null logger"
                .x(() => logger.Should().BeNull());

            "When constructing the generator with null logger"
                .x(() => e = Record.Exception(() => new YamlResourceGenerator(logger)));

            "Then the constructor should throw an exception"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("logger"));
        }

        /// <summary>
        /// Scenario tests that the object can be constructed successfully.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithSuccess(IResourceGenerator generator, ILogger logger, Exception e)
        {
            "Given a null generator"
                .x(() => generator.Should().BeNull());

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When constructing the generator"
                .x(() => e = Record.Exception(() => new YamlResourceGenerator(logger)));

            "Then the constructor should succeed"
                .x(() => e.Should().BeNull());
        }

        #endregion

        #region GenerateResourcesAsync Scenarios

        /// <summary>
        /// Scenario tests that the generator succeeds.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="model">The model.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GenerateResourcesAsyncWithSuccess(IResourceGenerator generator, AzureIntegrationServicesModel model, IList<YamlStream> config, Exception e)
        {
            "Given a resource generator"
                .x(() => generator = new YamlResourceGenerator(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And configuration"
                .x(async () =>
                {
                    var renderer = new LiquidTemplateRenderer(_mockLogger.Object);
                    var repository = new FileConfigurationRepository(renderer, _mockLogger.Object);
                    await repository.RenderConfigurationAsync(model, OkMultiConfigPath, _tempOutputTemplatePath);
                    config = repository.GetConfiguration(_tempOutputTemplatePath);
                });

            "When generating resources"
                .x(async () => e = await Record.ExceptionAsync(async () => await generator.GenerateResourcesAsync(model, config, _source.Token)));

            "Then the generation should succeed"
                .x(() => e.Should().BeNull());

            "And the model should have been populated with the template resources from the configuration"
                .x(() =>
                {
                    model.MigrationTarget.MessageBus.Resources.Should().HaveCount(10);
                    var app = model.MigrationTarget.MessageBus.Applications.Where(a => a.Name == "AppA").SingleOrDefault();
                    app.Should().NotBeNull();
                    app.Resources.Should().HaveCount(2);
                    var msg = app.Messages.SingleOrDefault();
                    msg.Should().NotBeNull();
                    msg.Resources.Should().HaveCount(1);
                });

            "And the model should have been populated with the snippet resources from the configuration"
                .x(() =>
                {
                    var app = model.MigrationTarget.MessageBus.Applications.Where(a => a.Name == "AppA").SingleOrDefault();
                    app.Should().NotBeNull();
                    var processManagers = app.Intermediaries.Where(i => i is ProcessManager);
                    processManagers.Should().NotBeNull().And.HaveCount(1);
                    var processManager = processManagers.Single();
                    processManager.Snippets.Should().HaveCount(11);
                });
        }

        /// <summary>
        /// Scenario tests that the generator succeeds when there are multiple targets across
        /// more than one line.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="model">The model.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GenerateResourcesWithMultipleTargetsAsyncWithSuccess(IResourceGenerator generator, AzureIntegrationServicesModel model, IList<YamlStream> config, Exception e)
        {
            "Given a resource generator"
                .x(() => generator = new YamlResourceGenerator(_mockLogger.Object));

            "And a model"
                .x(() =>
                {
                    model = _model;
                    model.MigrationTarget.TargetEnvironment = AzureIntegrationServicesTargetEnvironment.Ise;
                });

            "And configuration"
                .x(async () =>
                {
                    var renderer = new LiquidTemplateRenderer(_mockLogger.Object);
                    var repository = new FileConfigurationRepository(renderer, _mockLogger.Object);
                    await repository.RenderConfigurationAsync(model, OkConfigPath, _tempOutputTemplatePath);
                    config = repository.GetConfiguration(_tempOutputTemplatePath);
                });

            "When generating resources"
                .x(async () => e = await Record.ExceptionAsync(async () => await generator.GenerateResourcesAsync(model, config, _source.Token)));

            "Then the generation should succeed"
                .x(() => e.Should().BeNull());

            "And the model should have been populated with the template resources from the configuration"
                .x(() =>
                {
                    model.MigrationTarget.MessageBus.Resources.Should().HaveCount(10);
                    var app = model.MigrationTarget.MessageBus.Applications.Where(a => a.Name == "AppA").SingleOrDefault();
                    app.Should().NotBeNull();
                    app.Resources.Should().HaveCount(2);
                    var msg = app.Messages.SingleOrDefault();
                    msg.Should().NotBeNull();
                    msg.Resources.Should().HaveCount(1);
                });

            "And the model should have been populated with the snippet resources from the configuration"
                .x(() =>
                {
                    var app = model.MigrationTarget.MessageBus.Applications.Where(a => a.Name == "AppA").SingleOrDefault();
                    app.Should().NotBeNull();
                    var processManagers = app.Intermediaries.Where(i => i is ProcessManager);
                    processManagers.Should().NotBeNull().And.HaveCount(1);
                    var processManager = processManagers.Single();
                    processManager.Snippets.Should().HaveCount(11);
                });
        }

        /// <summary>
        /// Scenario tests that the generator warns when the message bus is missing.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="model">The model.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GenerateResourcesAsyncWithMessageBusMissingWithWarning(IResourceGenerator generator, AzureIntegrationServicesModel model, IList<YamlStream> config, Exception e)
        {
            "Given a resource generator"
                .x(() => generator = new YamlResourceGenerator(_mockLogger.Object));

            "And a model with a missing message bus"
                .x(() =>
                {
                    model = _model;
                    model.MigrationTarget.MessageBus = null;
                });

            "And configuration"
                .x(async () =>
                {
                    var renderer = new LiquidTemplateRenderer(_mockLogger.Object);
                    var repository = new FileConfigurationRepository(renderer, _mockLogger.Object);
                    await repository.RenderConfigurationAsync(model, OkMultiConfigPath, _tempOutputTemplatePath);
                    config = repository.GetConfiguration(_tempOutputTemplatePath);
                });

            "When generating resources"
                .x(async () => e = await Record.ExceptionAsync(async () => await generator.GenerateResourcesAsync(model, config, _source.Token)));

            "Then the generation should succeed"
                .x(() => e.Should().BeNull());

            "And it should have issued a warning"
                .x(() =>
                {
                    // Verify warning log message output once
                    _mockLogger.Verify(l => l.Log(
                        It.Is<LogLevel>(l => l == LogLevel.Warning),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("no Message Bus in the target model", StringComparison.CurrentCultureIgnoreCase)),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
                });
        }

        /// <summary>
        /// Scenario tests that the generator traces when the application doesn't have a resource map key.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="model">The model.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GenerateResourcesAsyncWithMissingResourceMapKeyWithDebug(IResourceGenerator generator, AzureIntegrationServicesModel model, IList<YamlStream> config, Exception e)
        {
            "Given a resource generator"
                .x(() => generator = new YamlResourceGenerator(_mockLogger.Object));

            "And a model with a missing application resource map key"
                .x(() =>
                {
                    model = _model;
                    model.MigrationTarget.MessageBus.Applications.Where(a => a.Name == "System").Single().ResourceMapKey = null;
                });

            "And configuration"
                .x(async () =>
                {
                    var renderer = new LiquidTemplateRenderer(_mockLogger.Object);
                    var repository = new FileConfigurationRepository(renderer, _mockLogger.Object);
                    await repository.RenderConfigurationAsync(model, OkMultiConfigPath, _tempOutputTemplatePath);
                    config = repository.GetConfiguration(_tempOutputTemplatePath);
                });

            "When generating resources"
                .x(async () => e = await Record.ExceptionAsync(async () => await generator.GenerateResourcesAsync(model, config, _source.Token)));

            "Then the generation should succeed"
                .x(() => e.Should().BeNull());

            "And it should have issued a debug message"
                .x(() =>
                {
                    // Verify debug log message output once
                    _mockLogger.Verify(l => l.Log(
                        It.Is<LogLevel>(l => l == LogLevel.Debug),
                        It.IsAny<EventId>(),
                        It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("is not associated with a resource map key", StringComparison.CurrentCultureIgnoreCase)),
                        It.IsAny<Exception>(),
                        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
                });
        }

        /// <summary>
        /// Scenario tests that an error is raised when the token is cancelled.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="model">The model.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GenerateResourcesAsyncWithOperationCancelledError(IResourceGenerator generator, AzureIntegrationServicesModel model, IList<YamlStream> config, Exception e)
        {
            "Given a resource generator"
                .x(() => generator = new YamlResourceGenerator(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And configuration"
                .x(async () =>
                {
                    var renderer = new LiquidTemplateRenderer(_mockLogger.Object);
                    var repository = new FileConfigurationRepository(renderer, _mockLogger.Object);
                    await repository.RenderConfigurationAsync(model, OkMultiConfigPath, _tempOutputTemplatePath);
                    config = repository.GetConfiguration(_tempOutputTemplatePath);
                });

            "And a cancelled token"
                .x(() => _source.Cancel());

            "When generating resources"
                .x(async () => e = await Record.ExceptionAsync(async () => await generator.GenerateResourcesAsync(model, config, _source.Token)));

            "Then the generation should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<OperationCanceledException>());
        }

        /// <summary>
        /// Scenario tests that an error is raised when the model is null.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="model">The model.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GenerateResourcesAsyncWithModelNullError(IResourceGenerator generator, AzureIntegrationServicesModel model, IList<YamlStream> config, Exception e)
        {
            "Given a resource generator"
                .x(() => generator = new YamlResourceGenerator(_mockLogger.Object));

            "And a null model"
                .x(() => model.Should().BeNull());

            "And configuration"
                .x(async () =>
                {
                    var renderer = new LiquidTemplateRenderer(_mockLogger.Object);
                    var repository = new FileConfigurationRepository(renderer, _mockLogger.Object);
                    await repository.RenderConfigurationAsync(_model, OkMultiConfigPath, _tempOutputTemplatePath);
                    config = repository.GetConfiguration(_tempOutputTemplatePath);
                });

            "When generating resources"
                .x(async () => e = await Record.ExceptionAsync(async () => await generator.GenerateResourcesAsync(model, config, _source.Token)));

            "Then the generation should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("model"));
        }

        /// <summary>
        /// Scenario tests that an error is raised when the config is null.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="model">The model.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GenerateResourcesAsyncWithConfigNullError(IResourceGenerator generator, AzureIntegrationServicesModel model, IList<YamlStream> config, Exception e)
        {
            "Given a resource generator"
                .x(() => generator = new YamlResourceGenerator(_mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And null configuration"
                .x(() => config.Should().BeNull());

            "When generating resources"
                .x(async () => e = await Record.ExceptionAsync(async () => await generator.GenerateResourcesAsync(model, config, _source.Token)));

            "Then the generation should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("config"));
        }

        #endregion
    }
}
