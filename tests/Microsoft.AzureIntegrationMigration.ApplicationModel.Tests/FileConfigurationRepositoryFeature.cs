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
    /// Defines the test spec for the <see cref="FileConfigurationRepository"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "xBehave syntax style.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is handled by xBehave.net and the background attribute.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Not needed for tests.")]
    public class FileConfigurationRepositoryFeature
    {
        /// <summary>
        /// Defines a config path.
        /// </summary>
        private const string OkConfigPath = @"..\..\..\..\scenarios\ok-config";

        /// <summary>
        /// Defines a config path.
        /// </summary>
        private const string OkAltNameConfigPath = @"..\..\..\..\scenarios\ok-altname-config";

        /// <summary>
        /// Defines a config path.
        /// </summary>
        private const string OkMultiConfigPath = @"..\..\..\..\scenarios\ok-multi-config";

        /// <summary>
        /// Defines a config path.
        /// </summary>
        private const string BadConfigPath = @"..\..\..\..\scenarios\bad-config";

        /// <summary>
        /// Defines a temporary file path for renderred template files.
        /// </summary>
        private string _tempOutputTemplatePath;

        /// <summary>
        /// Defines a mocked template renderer object.
        /// </summary>
        private Mock<ITemplateRenderer> _mockRenderer;

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

        #region Before Each Scenario

        /// <summary>
        /// Sets up state before each scenario.
        /// </summary>
        [Background]
        public void Setup()
        {
            "Given a new mock template renderer"
                .x(() =>
                {
                    _mockRenderer = new Mock<ITemplateRenderer>();
                    _mockRenderer.Setup(r => r.RenderTemplateAsync(It.IsAny<string>(), It.IsAny<AzureIntegrationServicesModel>(), It.IsAny<MessagingObject>(), It.IsAny<TargetResourceTemplate>())).ReturnsAsync(_templateContent);
                });

            "Given a new mock logger"
                .x(() =>
                {
                    _mockLogger = new Mock<ILogger>();
                    _mockLogger.Setup(l => l.IsEnabled(It.IsAny<LogLevel>())).Returns(true);
                });

            "Given scenario config paths"
                .x(() =>
                {
                    var dirInfo = new DirectoryInfo(OkConfigPath);
                    Directory.Exists(dirInfo.FullName).Should().BeTrue(OkConfigPath + " should exist");
                    dirInfo = new DirectoryInfo(OkMultiConfigPath);
                    Directory.Exists(dirInfo.FullName).Should().BeTrue(OkMultiConfigPath + " should exist");
                    dirInfo = new DirectoryInfo(BadConfigPath);
                    Directory.Exists(dirInfo.FullName).Should().BeTrue(BadConfigPath + " should exist");
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

            "Given a model"
                .x(() => _model = TestHelper.GetModel());
        }

        #endregion

        #region Constructor Scenarios

        /// <summary>
        /// Scenario tests that the object construction throws an exception when null renderer is passed.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithNullRenderer(IConfigurationRepository repository, ITemplateRenderer renderer, ILogger logger, Exception e)
        {
            "Given a null repository"
                .x(() => repository.Should().BeNull());

            "And a null template renderer"
                .x(() => renderer.Should().BeNull());

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When constructing the repository with null renderer"
                .x(() => e = Record.Exception(() => new FileConfigurationRepository(renderer, logger)));

            "Then the constructor should throw an exception"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("renderer"));
        }

        /// <summary>
        /// Scenario tests that the object construction throws an exception when null logger is passed.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithNullLogger(IConfigurationRepository repository, ITemplateRenderer renderer, ILogger logger, Exception e)
        {
            "Given a null repository"
                .x(() => repository.Should().BeNull());

            "And a template renderer"
                .x(() => renderer = _mockRenderer.Object);

            "And a null logger"
                .x(() => logger.Should().BeNull());

            "When constructing the repository with null logger"
                .x(() => e = Record.Exception(() => new FileConfigurationRepository(renderer, logger)));

            "Then the constructor should throw an exception"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("logger"));
        }

        /// <summary>
        /// Scenario tests that the object can be constructed successfully.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithSuccess(ITemplateRepository repository, ITemplateRenderer renderer, ILogger logger, Exception e)
        {
            "Given a null repository"
                .x(() => repository.Should().BeNull());

            "And a template renderer"
                .x(() => renderer = _mockRenderer.Object);

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When constructing the repository"
                .x(() => e = Record.Exception(() => new FileConfigurationRepository(renderer, logger)));

            "Then the constructor should succeed"
                .x(() => e.Should().BeNull());
        }

        #endregion

        #region GetConfiguration Scenarios

        /// <summary>
        /// Scenario tests that a single configuration file can be loaded successfully.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetSingleConfigurationWithSuccess(IConfigurationRepository repository, IList<YamlStream> config, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "When getting the configuration"
                .x(() => e = Record.Exception(() => config = repository.GetConfiguration(OkConfigPath)));

            "Then the get should succeed"
                .x(() => e.Should().BeNull());

            "And the config should contain a single object"
                .x(() => config.Should().NotBeNull().And.Subject.Should().HaveCount(1));
        }

        /// <summary>
        /// Scenario tests that no configuration files can be loaded because they don't exist.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetNoConfigurationWithSuccess(IConfigurationRepository repository, IList<YamlStream> config, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "When getting the configuration with a path with no YAML files"
                .x(() => e = Record.Exception(() => config = repository.GetConfiguration(".")));

            "Then the get should succeed"
                .x(() => e.Should().BeNull());

            "And the config should contain no objects"
                .x(() => config.Should().NotBeNull().And.Subject.Should().HaveCount(0));
        }

        /// <summary>
        /// Scenario tests that a single configuration file with alternative YAML extension can be loaded successfully.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetSingleAltNameConfigurationWithSuccess(IConfigurationRepository repository, IList<YamlStream> config, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "When getting the configuration with altenative extension"
                .x(() => e = Record.Exception(() => config = repository.GetConfiguration(OkAltNameConfigPath)));

            "Then the get should succeed"
                .x(() => e.Should().BeNull());

            "And the config should contain a single object"
                .x(() => config.Should().NotBeNull().And.Subject.Should().HaveCount(1));
        }

        /// <summary>
        /// Scenario tests that multiple configuration files can be loaded successfully.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetMultipleConfigurationWithSuccess(IConfigurationRepository repository, IList<YamlStream> config, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "When getting the configuration"
                .x(() => e = Record.Exception(() => config = repository.GetConfiguration(OkMultiConfigPath)));

            "Then the get should succeed"
                .x(() => e.Should().BeNull());

            "And the config should contain 5 objects"
                .x(() => config.Should().NotBeNull().And.Subject.Should().HaveCount(5));
        }

        /// <summary>
        /// Scenario tests that badly formatted configuration file fails to load.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetBadConfigurationWithError(IConfigurationRepository repository, IList<YamlStream> config, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "When getting the configuration"
                .x(() => e = Record.Exception(() => config = repository.GetConfiguration(BadConfigPath)));

            "Then the get should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<SemanticErrorException>());

            "And the config should be null"
                .x(() => config.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that configuration file fails to load when using a bad path.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetConfigurationWithPathNotFoundError(IConfigurationRepository repository, IList<YamlStream> config, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "When getting the configuration with a bad path"
                .x(() => e = Record.Exception(() => config = repository.GetConfiguration(@"A:\missing.yaml")));

            "Then the get should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<DirectoryNotFoundException>());

            "And the config should be null"
                .x(() => config.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that an error is raised when a path is null.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="config">The config.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetConfigurationWithPathNullError(IConfigurationRepository repository, IList<YamlStream> config, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "When getting the configuration with null path"
                .x(() => e = Record.Exception(() => config = repository.GetConfiguration(null)));

            "Then the get should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("path"));

            "And the config should be null"
                .x(() => config.Should().BeNull());
        }

        #endregion

        #region RenderConfigurationAsync Scenarios

        /// <summary>
        /// Scenario tests that the render succeeds.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderConfigurationAsyncWithSuccess(IConfigurationRepository repository, ITemplateRenderer renderer, AzureIntegrationServicesModel model, string sourcePath, string targetPath, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a repository"
                .x(() => repository = new FileConfigurationRepository(renderer, _mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a source path"
                .x(() => sourcePath = OkConfigPath);

            "And a target path"
                .x(() => targetPath = _tempOutputTemplatePath);

            "When getting the configuration"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.RenderConfigurationAsync(model, sourcePath, targetPath)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the output path should contain the configuration file with rendered content"
                .x(() =>
                {
                    var files = Directory.GetFiles(targetPath, "*");
                    files.Should().NotBeNull().And.HaveCount(1).And.ContainMatch("*aim-sample.yaml");

                    using var reader = new StreamReader(files.Single());
                    var content = reader.ReadToEnd();
                    content.Should().NotBeNull().And.ContainAny("Env: dev").And.NotContainAny("{{").And.NotContainAny("}}");
                });
        }

        /// <summary>
        /// Scenario tests that the render succeeds with multiple configuration files.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="model">The model.</param>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderMultipleConfigurationAsyncWithSuccess(IConfigurationRepository repository, ITemplateRenderer renderer, AzureIntegrationServicesModel model, string sourcePath, string targetPath, Exception e)
        {
            "Given a template renderer"
                .x(() => renderer = new LiquidTemplateRenderer(_mockLogger.Object));

            "And a repository"
                .x(() => repository = new FileConfigurationRepository(renderer, _mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a source path"
                .x(() => sourcePath = OkMultiConfigPath);

            "And a target path"
                .x(() => targetPath = _tempOutputTemplatePath);

            "When getting the configuration"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.RenderConfigurationAsync(model, sourcePath, targetPath)));

            "Then the render should succeed"
                .x(() => e.Should().BeNull());

            "And the output path should contain the configuration files with rendered content"
                .x(() =>
                {
                    var files = Directory.GetFiles(targetPath, "*");
                    files.Should().NotBeNull().And.HaveCount(5);

                    foreach (var file in files)
                    {
                        using var reader = new StreamReader(file);
                        var content = reader.ReadToEnd();
                        content.Should().NotBeNull().And.NotContainAny("{{").And.NotContainAny("}}");
                    }
                });
        }

        /// <summary>
        /// Scenario tests that an error is raised when the model is null.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="model">The model.</param>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderConfigurationAsyncWithModelNullError(IConfigurationRepository repository, AzureIntegrationServicesModel model, string sourcePath, string targetPath, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "And a null model"
                .x(() => model.Should().BeNull());

            "And a source path"
                .x(() => sourcePath = OkConfigPath);

            "And a target path"
                .x(() => targetPath = _tempOutputTemplatePath);

            "When getting the configuration with null model"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.RenderConfigurationAsync(model, sourcePath, targetPath)));

            "Then the render should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("model"));

            "And the output path should be empty"
                .x(() => Directory.GetFiles(targetPath, "*").Should().HaveCount(0));
        }

        /// <summary>
        /// Scenario tests that an error is raised when the source path is null.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="model">The model.</param>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderConfigurationAsyncWithSourcePathNullError(IConfigurationRepository repository, AzureIntegrationServicesModel model, string sourcePath, string targetPath, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a null source path"
                .x(() => sourcePath.Should().BeNull());

            "And a target path"
                .x(() => targetPath = _tempOutputTemplatePath);

            "When getting the configuration with null source path"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.RenderConfigurationAsync(model, sourcePath, targetPath)));

            "Then the render should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("sourcePath"));

            "And the output path should be empty"
                .x(() => Directory.GetFiles(targetPath, "*").Should().HaveCount(0));
        }

        /// <summary>
        /// Scenario tests that an error is raised when the target path is null.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="model">The model.</param>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void RenderConfigurationAsyncWithTargetPathNullError(IConfigurationRepository repository, AzureIntegrationServicesModel model, string sourcePath, string targetPath, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileConfigurationRepository(_mockRenderer.Object, _mockLogger.Object));

            "And a model"
                .x(() => model = _model);

            "And a source path"
                .x(() => sourcePath = OkConfigPath);

            "And a null target path"
                .x(() => targetPath.Should().BeNull());

            "When getting the configuration with null target path"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.RenderConfigurationAsync(model, sourcePath, targetPath)));

            "Then the render should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("targetPath"));
        }

        #endregion
    }
}
