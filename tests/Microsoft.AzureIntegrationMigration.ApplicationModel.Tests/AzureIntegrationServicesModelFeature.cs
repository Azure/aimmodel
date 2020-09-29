// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Report;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Source;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.Runner.Model;
using Microsoft.VisualBasic;
using Moq;
using Newtonsoft.Json;
using Xbehave;
using Xunit;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Tests
{
    /// <summary>
    /// Defines the test spec for the <see cref="AzureIntegrationServicesModel"/> class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "xBehave syntax style.")]
    public class AzureIntegrationServicesModelFeature
    {
        /// <summary>
        /// Defines the test model.
        /// </summary>
        private AzureIntegrationServicesModel _model;

        #region Before Each Scenario

        /// <summary>
        /// Sets up state before each scenario.
        /// </summary>
        [Background]
        public void Setup()
        {
            "Given a new model"
                .x(() =>
                {
                    _model = new AzureIntegrationServicesModel();
                    _model.MigrationSource.MigrationSourceModel = "My Source Model Object";

                    var container1 = new ResourceContainer() { Name = "testcontainer1", Type = "test.resourcecontainer.msi", ContainerLocation = @"c:\container1\testcontainer1.msi" };
                    var container2 = new ResourceContainer() { Name = "testcontainer2", Type = "test.resourcecontainer.msi", ContainerLocation = @"c:\container1\testcontainer2.msi" };
                    var container3 = new ResourceContainer() { Name = "testassembly1", Type = "test.resourcecontainer.assembly", ContainerLocation = @"c:\container1\testassembly1.dll" };

                    var msiResource1 = new ResourceDefinition() { Name = "testfile1", Type = "test.resource.file", ResourceContent = "filecontent1" };
                    var msiResource2 = new ResourceDefinition() { Name = "testfile2", Type = "test.resource.file", ResourceContent = "filecontent2" };
                    var msiResource3 = new ResourceDefinition() { Name = "testfile3", Type = "test.resource.file", ResourceContent = "filecontent3" };

                    var asmResource1 = new ResourceDefinition() { Name = "class1", Type = "test.resource.class", ResourceContent = "classdef1" };
                    var asmResource2 = new ResourceDefinition() { Name = "class2", Type = "test.resource.class", ResourceContent = "classdef2" };

                    container1.ResourceDefinitions.Add(msiResource1);
                    container2.ResourceDefinitions.Add(msiResource2);
                    container2.ResourceDefinitions.Add(msiResource3);
                    container3.ResourceDefinitions.Add(asmResource1);
                    container3.ResourceDefinitions.Add(asmResource2);

                    _model.MigrationSource.ResourceContainers.Add(container1);
                    _model.MigrationSource.ResourceContainers.Add(container2);
                    _model.MigrationSource.ResourceContainers.Add(container3);
                });
        }

        #endregion

        #region GetSourceModel Scenarios

        /// <summary>
        /// Scenario tests that the source model is retrieved successfully.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="sourceModel">The source model.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetTypedModelWithSuccess(AzureIntegrationServicesModel model, string sourceModel, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "When getting the source model"
                .x(() => e = Record.Exception(() => sourceModel = model.GetSourceModel<string>()));

            "Then getting the source model should succeed"
                .x(() => e.Should().BeNull());

            "And the source model contains the expected value"
                .x(() =>
                {
                    sourceModel.Should().NotBeNullOrEmpty();
                    sourceModel.Should().Be("My Source Model Object");
                });
        }

        /// <summary>
        /// Scenario tests that the source model is retrieved successfully when it contains a value type.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="sourceModel">The source model.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetValueTypedModelWithSuccess(AzureIntegrationServicesModel model, int sourceModel, Exception e)
        {
            "Given a model"
                .x(() =>
                {
                    model = _model;
                    model.MigrationSource.MigrationSourceModel = 12;
                });

            "When getting the source model"
                .x(() => e = Record.Exception(() => sourceModel = model.GetSourceModel<int>()));

            "Then getting the source model should succeed"
                .x(() => e.Should().BeNull());

            "And the source model contains the expected integer value"
                .x(() =>
                {
                    sourceModel.Should().Be(12);
                });
        }

        /// <summary>
        /// Scenario tests that the source model returns null when it expects a different type.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="sourceModel">The source model.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void GetIncorrectlyTypedModelWithSuccess(AzureIntegrationServicesModel model, Collection sourceModel, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "When getting the source model"
                .x(() => e = Record.Exception(() => sourceModel = model.GetSourceModel<Collection>()));

            "Then getting the source model should succeed"
                .x(() => e.Should().BeNull());

            "And the source model contains a null value"
                .x(() => sourceModel.Should().BeNull());
        }

        #endregion

        #region FindMessagingObject Scenarios

        /// <summary>
        /// Scenario tests that the message bus can be found successfully.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messagingObjects">The messaging objects.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindMessagingObjectMessageBusWithSuccess(AzureIntegrationServicesModel model, (MessageBus messageBus, Application application, MessagingObject messagingObject) messagingObjects, Exception e)
        {
            "Given a model"
                .x(() => model = TestHelper.GetModel());

            "When finding the message bus"
                .x(() => e = Record.Exception(() => messagingObjects = model.FindMessagingObject("ContosoMessageBus")));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the result should contain a message bus"
                .x(() =>
                {
                    messagingObjects.Should().NotBeNull();
                    messagingObjects.messageBus.Should().NotBeNull();
                    messagingObjects.messageBus.Name.Should().Be("ContosoMessageBus");
                    messagingObjects.messageBus.Type.Should().Be(MessagingObjectType.MessageBus);
                    messagingObjects.application.Should().BeNull();
                    messagingObjects.messagingObject.Should().BeNull();
                });
        }

        /// <summary>
        /// Scenario tests that an application can be found successfully.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messagingObjects">The messaging objects.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindMessagingObjectApplicationWithSuccess(AzureIntegrationServicesModel model, (MessageBus messageBus, Application application, MessagingObject messagingObject) messagingObjects, Exception e)
        {
            "Given a model"
                .x(() => model = TestHelper.GetModel());

            "When finding an application"
                .x(() => e = Record.Exception(() => messagingObjects = model.FindMessagingObject("ContosoMessageBus:AppA")));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the result should contain a message bus and an application"
                .x(() =>
                {
                    messagingObjects.Should().NotBeNull();
                    messagingObjects.messageBus.Should().NotBeNull();
                    messagingObjects.application.Should().NotBeNull();
                    messagingObjects.application.Name.Should().Be("AppA");
                    messagingObjects.application.Type.Should().Be(MessagingObjectType.Application);
                    messagingObjects.messagingObject.Should().BeNull();
                });
        }

        /// <summary>
        /// Scenario tests that a message can be found successfully.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messagingObjects">The messaging objects.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindMessagingObjectMessageWithSuccess(AzureIntegrationServicesModel model, (MessageBus messageBus, Application application, MessagingObject messagingObject) messagingObjects, Exception e)
        {
            "Given a model"
                .x(() => model = TestHelper.GetModel());

            "When finding a message"
                .x(() => e = Record.Exception(() => messagingObjects = model.FindMessagingObject("ContosoMessageBus:AppA:PurchaseOrderFlatFile")));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the result should contain a message bus and an application and the message"
                .x(() =>
                {
                    messagingObjects.Should().NotBeNull();
                    messagingObjects.messageBus.Should().NotBeNull();
                    messagingObjects.application.Should().NotBeNull();
                    messagingObjects.application.Name.Should().Be("AppA");
                    messagingObjects.messagingObject.Should().NotBeNull();
                    messagingObjects.messagingObject.Name.Should().Be("PurchaseOrderFlatFile");
                    messagingObjects.messagingObject.Type.Should().Be(MessagingObjectType.Message);
                });
        }

        /// <summary>
        /// Scenario tests that a channel can be found successfully.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messagingObjects">The messaging objects.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindMessagingObjectChannelWithSuccess(AzureIntegrationServicesModel model, (MessageBus messageBus, Application application, MessagingObject messagingObject) messagingObjects, Exception e)
        {
            "Given a model"
                .x(() => model = TestHelper.GetModel());

            "When finding a channel"
                .x(() => e = Record.Exception(() => messagingObjects = model.FindMessagingObject("ContosoMessageBus:System:MessageBox")));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the result should contain a message bus and an application and the channel"
                .x(() =>
                {
                    messagingObjects.Should().NotBeNull();
                    messagingObjects.messageBus.Should().NotBeNull();
                    messagingObjects.application.Should().NotBeNull();
                    messagingObjects.application.Name.Should().Be("System");
                    messagingObjects.messagingObject.Should().NotBeNull();
                    messagingObjects.messagingObject.Name.Should().Be("MessageBox");
                    messagingObjects.messagingObject.Type.Should().Be(MessagingObjectType.Channel);
                });
        }

        /// <summary>
        /// Scenario tests that an endpoint can be found successfully.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messagingObjects">The messaging objects.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindMessagingObjectEndpointWithSuccess(AzureIntegrationServicesModel model, (MessageBus messageBus, Application application, MessagingObject messagingObject) messagingObjects, Exception e)
        {
            "Given a model"
                .x(() => model = TestHelper.GetModel());

            "When finding an endpoint"
                .x(() => e = Record.Exception(() => messagingObjects = model.FindMessagingObject("ContosoMessageBus:System:FtpReceive")));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the result should contain a message bus and an application and the endpoint"
                .x(() =>
                {
                    messagingObjects.Should().NotBeNull();
                    messagingObjects.messageBus.Should().NotBeNull();
                    messagingObjects.application.Should().NotBeNull();
                    messagingObjects.application.Name.Should().Be("System");
                    messagingObjects.messagingObject.Should().NotBeNull();
                    messagingObjects.messagingObject.Name.Should().Be("FtpReceive");
                    messagingObjects.messagingObject.Type.Should().Be(MessagingObjectType.Endpoint);
                });
        }

        /// <summary>
        /// Scenario tests that an intermediary can be found successfully.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="messagingObjects">The messaging objects.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void FindMessagingObjectIntermediaryWithSuccess(AzureIntegrationServicesModel model, (MessageBus messageBus, Application application, MessagingObject messagingObject) messagingObjects, Exception e)
        {
            "Given a model"
                .x(() => model = TestHelper.GetModel());

            "When finding an intermediary"
                .x(() => e = Record.Exception(() => messagingObjects = model.FindMessagingObject("ContosoMessageBus:System:MessageAgent")));

            "Then the find should succeed"
                .x(() => e.Should().BeNull());

            "And the result should contain a message bus and an application and the intemediary"
                .x(() =>
                {
                    messagingObjects.Should().NotBeNull();
                    messagingObjects.messageBus.Should().NotBeNull();
                    messagingObjects.application.Should().NotBeNull();
                    messagingObjects.application.Name.Should().Be("System");
                    messagingObjects.messagingObject.Should().NotBeNull();
                    messagingObjects.messagingObject.Name.Should().Be("MessageAgent");
                    messagingObjects.messagingObject.Type.Should().Be(MessagingObjectType.Intermediary);
                });
        }

        #endregion

        #region Serialization Scenarios

        /// <summary>
        /// Scenario tests that the object once constructed can be serialized to JSON.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="json">The serialized model as a JSON string.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void JsonSerializeWithSuccess(AzureIntegrationServicesModel model, string json, Exception e)
        {
            "Given a model"
                .x(() => model = _model);

            "When serializing the model to JSON"
                .x(() => e = Record.Exception(() => json = JsonConvert.SerializeObject(model, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All, ReferenceLoopHandling = ReferenceLoopHandling.Serialize, Formatting = Formatting.Indented })));

            "Then the serializer should succeed"
                .x(() => e.Should().BeNull());

            "And the model contains the content expected"
                .x(() =>
                {
                    json.Should().NotBeNullOrEmpty();
                    json.Should().ContainAll(
                        "testcontainer1", "test.resourcecontainer.msi", @"c:\\container1\\testcontainer1.msi",
                        "testcontainer2", "test.resourcecontainer.msi", @"c:\\container1\\testcontainer2.msi",
                        "testassembly1", "test.resourcecontainer.assembly", @"c:\\container1\\testassembly1.dll",
                        "testfile1", "test.resource.file", "filecontent1",
                        "testfile2", "test.resource.file", "filecontent2",
                        "testfile3", "test.resource.file", "filecontent3",
                        "class1", "test.resource.class", "classdef1",
                        "class2", "test.resource.class", "classdef2");
                });
        }

        #endregion
    }
}
