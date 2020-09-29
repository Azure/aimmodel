// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using FluentAssertions;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer;
using Microsoft.Extensions.Logging;
using Moq;
using Xbehave;
using Xunit;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Tests
{
    /// <summary>
    /// Defines the test spec for the <see cref="FileTemplateRepository"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "xBehave syntax style.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is handled by xBehave.net and the background attribute.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Not needed for tests.")]
    public class FileTemplateRepositoryFeature
    {
        /// <summary>
        /// Defines a mocked logger object.
        /// </summary>
        private Mock<ILogger> _mockLogger;

        /// <summary>
        /// Defines a temporary file path for a template file.
        /// </summary>
        private string _tempTemplateFilePath;

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
            "Given a new mock logger"
                .x(() =>
                {
                    _mockLogger = new Mock<ILogger>();
                    _mockLogger.Setup(l => l.IsEnabled(It.IsAny<LogLevel>())).Returns(true);
                });

            "Given a template file"
                .x(() =>
                {
                    _tempTemplateFilePath = Path.GetTempFileName();
                    using var writer = new StreamWriter(_tempTemplateFilePath);
                    writer.Write(_templateContent);
                })
                .Teardown(() =>
                {
                    if (File.Exists(_tempTemplateFilePath))
                    {
                        File.Delete(_tempTemplateFilePath);
                    }
                });
        }

        #endregion

        #region Constructor Scenarios

        /// <summary>
        /// Scenario tests that the object construction throws an exception when null logger is passed.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithNullLogger(ITemplateRepository repository, ILogger logger, Exception e)
        {
            "Given a null repository"
                .x(() => repository.Should().BeNull());

            "And a null logger"
                .x(() => logger.Should().BeNull());

            "When constructing the repository with null logger"
                .x(() => e = Record.Exception(() => new FileTemplateRepository(logger)));

            "Then the constructor should throw an exception"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>().Which.ParamName.Should().Be("logger"));
        }

        /// <summary>
        /// Scenario tests that the object can be constructed successfully.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConstructWithSuccess(ITemplateRepository repository, ILogger logger, Exception e)
        {
            "Given a null repository"
                .x(() => repository.Should().BeNull());

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When constructing the repository"
                .x(() => e = Record.Exception(() => new FileTemplateRepository(logger)));

            "Then the constructor should succeed"
                .x(() => e.Should().BeNull());
        }

        #endregion

        #region LoadTemplateAsync Scenarios

        /// <summary>
        /// Scenario tests that a template can be loaded successfully.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void LoadTemplateWithSuccess(ITemplateRepository repository, ILogger logger, string templateContent, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileTemplateRepository(_mockLogger.Object));

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "And a template file that exists"
                .x(() => File.Exists(_tempTemplateFilePath).Should().BeTrue());

            "When loading the template"
                .x(async () => e = await Record.ExceptionAsync(async () => templateContent = await repository.LoadTemplateAsync(_tempTemplateFilePath)));

            "Then the load should succeed"
                .x(() => e.Should().BeNull());

            "And the template should contain the correct content"
                .x(() => templateContent.Should().StartWith("test template"));
        }

        /// <summary>
        /// Scenario tests that a load of a template fails when the path is null.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void LoadTemplateWithPathNullError(ITemplateRepository repository, ILogger logger, string templateContent, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileTemplateRepository(_mockLogger.Object));

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When loading the template for a file path that is null"
                .x(async () => e = await Record.ExceptionAsync(async () => templateContent = await repository.LoadTemplateAsync(null)));

            "Then the load should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>());

            "And the template content should be null"
                .x(() => templateContent.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that a load of a template fails when the template doesn't exist.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void LoadTemplateWithFileNotFoundError(ITemplateRepository repository, ILogger logger, string templateContent, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileTemplateRepository(_mockLogger.Object));

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "When loading the template for a file that doesn't exist"
                .x(async () => e = await Record.ExceptionAsync(async () => templateContent = await repository.LoadTemplateAsync("missingfile.json")));

            "Then the load should error"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<FileNotFoundException>());

            "And the template content should be null"
                .x(() => templateContent.Should().BeNull());
        }

        #endregion

        #region SaveTemplateAsync Scenarios

        /// <summary>
        /// Scenario tests that a template can be saved successfully.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="templateFilePath">The template file path.</param>
        /// <param name="templateContent">The template content.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void SaveTemplateWithSuccess(ITemplateRepository repository, ILogger logger, string templateFilePath, string templateContent, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileTemplateRepository(_mockLogger.Object));

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "And a template file path"
                .x(() => templateFilePath = Path.GetTempFileName());

            "When saving the template"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.SaveTemplateAsync(templateFilePath, _templateContent)))
                .Teardown(() =>
                {
                    if (File.Exists(templateFilePath))
                    {
                        File.Delete(templateFilePath);
                    }
                });

            "Then the save should succeed"
                .x(() => e.Should().BeNull());

            "And the saved template should contain the correct content"
                .x(() =>
                {
                    using (var reader = new StreamReader(templateFilePath))
                    {
                        templateContent = reader.ReadToEnd();
                    }

                    templateContent.Should().Be(_templateContent);
                });
        }

        /// <summary>
        /// Scenario tests that a save fails when a path is null.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="templateFilePath">The template file path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void SaveTemplateWithPathNullError(ITemplateRepository repository, ILogger logger, string templateFilePath, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileTemplateRepository(_mockLogger.Object));

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "And a template file path that is null"
                .x(() => templateFilePath.Should().BeNull());

            "When saving the template"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.SaveTemplateAsync(templateFilePath, _templateContent)));

            "Then the save should fail"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>());
        }

        /// <summary>
        /// Scenario tests that a save fails when the content is null.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="templateFilePath">The template file path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void SaveTemplateWithContentNullError(ITemplateRepository repository, ILogger logger, string templateFilePath, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileTemplateRepository(_mockLogger.Object));

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "And a template file path"
                .x(() => templateFilePath = Path.GetTempFileName());

            "When saving the template with null content"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.SaveTemplateAsync(templateFilePath, null)));

            "Then the save should fail"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<ArgumentNullException>());
        }

        /// <summary>
        /// Scenario tests that a save fails when a path is incorrect.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="templateFilePath">The template file path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void SaveTemplateWithDirectoryNotFoundError(ITemplateRepository repository, ILogger logger, string templateFilePath, Exception e)
        {
            "Given a repository"
                .x(() => repository = new FileTemplateRepository(_mockLogger.Object));

            "And a logger"
                .x(() => logger = _mockLogger.Object);

            "And a template file path"
                .x(() => templateFilePath = @"A:\badfile.json");

            "When saving the template"
                .x(async () => e = await Record.ExceptionAsync(async () => await repository.SaveTemplateAsync(templateFilePath, _templateContent)));

            "Then the save should fail"
                .x(() => e.Should().NotBeNull().And.Subject.Should().BeOfType<DirectoryNotFoundException>());
        }

        #endregion
    }
}
