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
    /// Defines the test spec for the <see cref="CustomLiquidFunctions"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "xBehave syntax style.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This is handled by xBehave.net and the background attribute.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Not needed for tests.")]
    public class CustomLiquidFunctionsFeature
    {
        /// <summary>
        /// Defines a model for custom functions.
        /// </summary>
        private AzureIntegrationServicesModel _model;

        #region Before Each Scenario

        /// <summary>
        /// Sets up state before each scenario.
        /// </summary>
        [Background]
        public void Setup()
        {
            "Given a model"
                .x(() => _model = TestHelper.GetModelWithTargetResources());
        }

        #endregion

        #region FindResourceTemplate Scenarios

        /// <summary>
        /// Scenario tests that the find operation succeeds when the target resource exists.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="resource">The target resource.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindChannelResourceWithSuccess(AzureIntegrationServicesModel model, string templateKey, TargetResourceTemplate resource, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And a template key"
                .x(() => templateKey = "topicChannelAzureServiceBusStandard");

            "When finding the resource template"
                .x(() => e = Record.Exception(() => resource = CustomLiquidFunctions.FindResourceTemplate(model, templateKey)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resource.Should().NotBeNull();
                    resource.TemplateKey.Should().Be("topicChannelAzureServiceBusStandard");
               });
        }

        /// <summary>
        /// Scenario tests that the find operation returns null when the target resource doesn't exist.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="resource">The target resource.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindChannelMissingResourceWithSuccess(AzureIntegrationServicesModel model, string templateKey, TargetResourceTemplate resource, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And a template key for a resource that doesn't exist"
                .x(() => templateKey = "missingChannel");

            "When finding the resource template"
                .x(() => e = Record.Exception(() => resource = CustomLiquidFunctions.FindResourceTemplate(model, templateKey)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be null"
                .x(() => resource.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that the find operation returns null when the model is null.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="resource">The target resource.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindChannelResourceWithNullModel(AzureIntegrationServicesModel model, string templateKey, TargetResourceTemplate resource, Exception e)
        {
            "Given a null model"
                .x(() => model.Should().BeNull());

            "And a template key"
                .x(() => templateKey = "messageBox");

            "When finding the resource template"
                .x(() => e = Record.Exception(() => resource = CustomLiquidFunctions.FindResourceTemplate(model, templateKey)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be null"
                .x(() => resource.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that the find operation returns null when the template key is null.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="templateKey">The template key.</param>
        /// <param name="resource">The target resource.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindChannelResourceWithTemplateKeyNullError(AzureIntegrationServicesModel model, string templateKey, TargetResourceTemplate resource, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And a null template key"
                .x(() => templateKey.Should().BeNull());

            "When finding the resource template"
                .x(() => e = Record.Exception(() => resource = CustomLiquidFunctions.FindResourceTemplate(model, templateKey)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be null"
                .x(() => resource.Should().BeNull());
        }

        #endregion

        #region FindAllResourceTemplates Scenarios

        /// <summary>
        /// Scenario tests that the find operation returns null if the model is null
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="resources">The target resources.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindAllResourceTemplatesWithNullModel(AzureIntegrationServicesModel model, IEnumerable<TargetResourceTemplate> resources, Exception e)
        {
            "Given a null model"
                .x(() => model = null);

            "When finding all resource templates"
                .x(() => e = Record.Exception(() => resources = CustomLiquidFunctions.FindAllResourceTemplates(model)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resources.Should().BeNull();
                });
        }

        /// <summary>
        /// Scenario tests that the find operation returns resource templates with only channels having resources.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="resources">The target resources.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindAllResourceTemplatesWithChannelsOnly(AzureIntegrationServicesModel model, IEnumerable<TargetResourceTemplate> resources, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And the model only has resources for channels"
                .x(() =>
                {
                    _model.MigrationTarget.MessageBus.Resources.Clear();
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Messages).ToList().ForEach( a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Intermediaries).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Endpoints).ToList().ForEach(a => a.Resources.Clear());

                });

            "When finding all resource templates"
                .x(() => e = Record.Exception(() => resources = CustomLiquidFunctions.FindAllResourceTemplates(model)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resources.Should().NotBeNullOrEmpty();
                    resources.Should().HaveCount(1);
                });
        }

        /// <summary>
        /// Scenario tests that the find operation returns resource templates with only messages having resources.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="resources">The target resources.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindAllResourceTemplatesWithMessagesOnly(AzureIntegrationServicesModel model, IEnumerable<TargetResourceTemplate> resources, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And the model only has resources for messages"
                .x(() =>
                {
                    _model.MigrationTarget.MessageBus.Resources.Clear();
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Channels).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Intermediaries).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Endpoints).ToList().ForEach(a => a.Resources.Clear());

                });

            "When finding all resource templates"
                .x(() => e = Record.Exception(() => resources = CustomLiquidFunctions.FindAllResourceTemplates(model)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resources.Should().NotBeNullOrEmpty();
                    resources.Should().HaveCount(1);
                });
        }

        /// <summary>
        /// Scenario tests that the find operation returns resource templates with only intermediaries having resources.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="resources">The target resources.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindAllResourceTemplatesWithIntermediariesOnly(AzureIntegrationServicesModel model, IEnumerable<TargetResourceTemplate> resources, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And the model only has resources for intermediaries"
                .x(() =>
                {
                    _model.MigrationTarget.MessageBus.Resources.Clear();
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Channels).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Messages).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Endpoints).ToList().ForEach(a => a.Resources.Clear());

                });

            "When finding all resource templates"
                .x(() => e = Record.Exception(() => resources = CustomLiquidFunctions.FindAllResourceTemplates(model)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resources.Should().NotBeNullOrEmpty();
                    resources.Should().HaveCount(2);
                });
        }

        /// <summary>
        /// Scenario tests that the find operation returns resource templates with only endpoints having resources.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="resources">The target resources.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindAllResourceTemplatesWithEndpointsOnly(AzureIntegrationServicesModel model, IEnumerable<TargetResourceTemplate> resources, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And the model only has resources for endpoints"
                .x(() =>
                {
                    _model.MigrationTarget.MessageBus.Resources.Clear();
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Channels).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Messages).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Intermediaries).ToList().ForEach(a => a.Resources.Clear());
                });

            "When finding all resource templates"
                .x(() => e = Record.Exception(() => resources = CustomLiquidFunctions.FindAllResourceTemplates(model)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resources.Should().NotBeNullOrEmpty();
                    resources.Should().HaveCount(1);
                });
        }

        /// <summary>
        /// Scenario tests that the find operation returns resource templates with only message bus having resources.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="resources">The target resources.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindAllResourceTemplatesWithMessageBusOnly(AzureIntegrationServicesModel model, IEnumerable<TargetResourceTemplate> resources, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "And the model only has resources for the message bus"
                .x(() =>
                {
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Channels).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Endpoints).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Messages).ToList().ForEach(a => a.Resources.Clear());
                    _model.MigrationTarget.MessageBus.Applications.SelectMany(a => a.Intermediaries).ToList().ForEach(a => a.Resources.Clear());
                });

            "When finding all resource templates"
                .x(() => e = Record.Exception(() => resources = CustomLiquidFunctions.FindAllResourceTemplates(model)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resources.Should().NotBeNullOrEmpty();
                    resources.Should().HaveCount(1);
                });
        }


        /// <summary>
        /// Scenario tests that the find operation returns resource templates when there are multiple resources in the target model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="resources">The target resources.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindAllResourceTemplatesWitMultiplResources(AzureIntegrationServicesModel model, IEnumerable<TargetResourceTemplate> resources, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "When finding all resource templates"
                .x(() => e = Record.Exception(() => resources = CustomLiquidFunctions.FindAllResourceTemplates(model)));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the target resource should be the expected value from the model"
                .x(() =>
                {
                    resources.Should().NotBeNullOrEmpty();
                    resources.Should().HaveCount(6);
                });
        }

        #endregion

        #region GetEnvironmentVariable Scenarios

        /// <summary>
        /// Scenario tests that the get operation succeeds.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="env">The environment variable value.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetEnvironmentVariableWithSuccess(string name, string env, Exception e)
        {
            "Given a name"
                .x(() => name = "TESTENV");

            "And a test environment variable"
                .x(() => Environment.SetEnvironmentVariable(name, "This Is A Test"))
                .Teardown(() => Environment.SetEnvironmentVariable(name, null));

            "When getting the environment variable"
                .x(() => e = Record.Exception(() => env = CustomLiquidFunctions.GetEnvironmentVariable(name)));

            "Then the get should succeed"
                .x(() => e.Should().BeNull());

            "And the environment variable should have the expected value"
                .x(() => env.Should().Be("This Is A Test"));
        }

        /// <summary>
        /// Scenario tests that the get operation returns null when the name is null.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="env">The environment variable value.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetEnvironmentVariableWithEnvVarNameNullError(string name, string env, Exception e)
        {
            "Given a null name"
                .x(() => name.Should().BeNull());

            "And a test environment variable"
                .x(() => Environment.SetEnvironmentVariable("TESTENV", "This Is A Test"))
                .Teardown(() => Environment.SetEnvironmentVariable("TESTENV", null));

            "When getting the environment variable"
                .x(() => e = Record.Exception(() => env = CustomLiquidFunctions.GetEnvironmentVariable(name)));

            "Then the get should succeed"
                .x(() => e.Should().BeNull());

            "And the environment variable should be null"
                .x(() => env.Should().BeNull());
        }

        #endregion

        #region FormatRegion Scenarios

        /// <summary>
        /// Scenario tests that the format operation succeeds.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="formattedRegion">The formatted region.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FormatRegionWithSuccess(string region, string formattedRegion, Exception e)
        {
            "Given a region"
                .x(() => region = "UK South");

            "When formatting the region"
                .x(() => e = Record.Exception(() => formattedRegion = CustomLiquidFunctions.FormatRegion(region)));

            "Then the format should succeed"
                .x(() => e.Should().BeNull());

            "And the formatted region should have the expected value"
                .x(() => formattedRegion.Should().Be("uksouth"));
        }

        /// <summary>
        /// Scenario tests that the format operation succeeds.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="formattedRegion">The formatted region.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FormatMissingRegionWithSuccess(string region, string formattedRegion, Exception e)
        {
            "Given a region"
                .x(() => region = "Non-Existent Region");

            "When formatting the region"
                .x(() => e = Record.Exception(() => formattedRegion = CustomLiquidFunctions.FormatRegion(region)));

            "Then the format should succeed"
                .x(() => e.Should().BeNull());

            "And the formatted region should be null"
                .x(() => formattedRegion.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that the format operation returns null when the region is null.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="formattedRegion">The formatted region.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FormatRegionWithRegionNullError(string region, string formattedRegion, Exception e)
        {
            "Given a null region"
                .x(() => region.Should().BeNull());

            "When formatting the region"
                .x(() => e = Record.Exception(() => formattedRegion = CustomLiquidFunctions.FormatRegion(region)));

            "Then the format should succeed"
                .x(() => e.Should().BeNull());

            "And the formatted region should be null"
                .x(() => formattedRegion.Should().BeNull());
        }

        #endregion

        #region ToJsonString Scenarios

        /// <summary>
        /// Scenario tests that the conversion operation succeeds.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="jsonString">The converted string.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ToJsonStringWithSuccess(string value, string jsonString, Exception e)
        {
            "Given a string"
                .x(() => value = "Unquoted String");

            "When converting the string"
                .x(() => e = Record.Exception(() => jsonString = CustomLiquidFunctions.ToJsonString(value)));

            "Then the conversion should succeed"
                .x(() => e.Should().BeNull());

            "And the JSON string should have the expected value"
                .x(() => jsonString.Should().Be("Unquoted String"));
        }

        /// <summary>
        /// Scenario tests that the conversion operation succeeds.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="jsonString">The converted string.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ToEscapedJsonStringWithSuccess(string value, string jsonString, Exception e)
        {
            "Given a string"
                .x(() => value = "\"Quoted String with Backslash \\ Character\"");

            "When converting the string"
                .x(() => e = Record.Exception(() => jsonString = CustomLiquidFunctions.ToJsonString(value)));

            "Then the conversion should succeed"
                .x(() => e.Should().BeNull());

            "And the JSON string should have the expected value"
                .x(() => jsonString.Should().Be("\\\"Quoted String with Backslash \\\\ Character\\\""));
        }

        /// <summary>
        /// Scenario tests that the conversion operation succeeds.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="jsonString">The converted string.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ToNullJsonStringWithSuccess(string value, string jsonString, Exception e)
        {
            "Given a null string"
                .x(() => value = null);

            "When converting the string"
                .x(() => e = Record.Exception(() => jsonString = CustomLiquidFunctions.ToJsonString(value)));

            "Then the conversion should succeed"
                .x(() => e.Should().BeNull());

            "And the JSON string should be null"
                .x(() => jsonString.Should().BeNull());
        }

        #endregion

        #region ToSafeFilepath Scenarios

        /// <summary>
        /// Scenario tests that the conversion operation succeeds.
        /// </summary>
        /// <param name="filePath">The file path to make safe.</param>
        /// <param name="safeFilePath">The converted file path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ToNullSafeFilePathWithSuccess(string filePath, string safeFilePath, Exception e)
        {
            "Given a null file path"
                .x(() => filePath = null);

            "When making the file path safe"
                .x(() => e = Record.Exception(() => safeFilePath = CustomLiquidFunctions.ToSafeFilePath(filePath)));

            "Then the conversion should succeed"
                .x(() => e.Should().BeNull());

            "And the safe file path should be null"
                .x(() => safeFilePath.Should().BeNull());
        }

        /// <summary>
        /// Scenario tests that the conversion operation succeeds.
        /// </summary>
        /// <param name="filePath">The file path to make safe.</param>
        /// <param name="safeFilePath">The converted file path.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ToSafeFilePathWithSuccess(string filePath, string safeFilePath, Exception e)
        {
            "Given a file path"
                .x(() => filePath = "this//is\\\\a\\\\t?e>st/to/s e|e*/if/:th\"e/path/i<s/conver>ted");

            "When making the filepath safe"
                .x(() => e = Record.Exception(() => safeFilePath = CustomLiquidFunctions.ToSafeFilePath(filePath)));

            "Then the conversion should succeed"
                .x(() => e.Should().BeNull());

            "And the safe file path should be converted"
                .x(() => safeFilePath.Should().Be("this/is/a/test/to/see/if/the/path/is/converted"));
        }

        #endregion
    }
}
