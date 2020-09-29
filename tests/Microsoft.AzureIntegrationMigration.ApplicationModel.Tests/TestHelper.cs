// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Channels;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Tests
{
    /// <summary>
    /// Defines a class that provides helper functions to tests.
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Gets a populated model to use for the tests.
        /// </summary>
        /// <returns>A model instance.</returns>
        public static AzureIntegrationServicesModel GetModel()
        {
            var model = new AzureIntegrationServicesModel();
            model.MigrationTarget.TargetEnvironment = AzureIntegrationServicesTargetEnvironment.Consumption;
            model.MigrationTarget.DeploymentEnvironment = "dev";
            model.MigrationTarget.AzureSubscriptionId = "azure-subs-id";
            model.MigrationTarget.AzurePrimaryRegion = "UK South";
            model.MigrationTarget.AzureSecondaryRegion = "UK West";

            // Add a message bus
            model.MigrationTarget.MessageBus = new MessageBus()
            {
                Name = "ContosoMessageBus",
                Key = "ContosoMessageBus",
                ResourceMapKey = "messageBus"
            };

            // Add an application
            var systemApp = new Application()
            {
                Name = "System",
                Key = "ContosoMessageBus:System"
            };
            model.MigrationTarget.MessageBus.Applications.Add(systemApp);

            var app = new Application()
            {
                Name = "AppA",
                Key = "ContosoMessageBus:AppA",
                ResourceMapKey = "application"
            };
            model.MigrationTarget.MessageBus.Applications.Add(app);

            // Add an application message
            var appMessage = new DocumentMessage()
            {
                Name = "PurchaseOrderFlatFile",
                Key = "ContosoMessageBus:AppA:PurchaseOrderFlatFile",
                ContentType = MessageContentType.Xml,
                ResourceMapKey = "applicationMessage"
            };
            app.Messages.Add(appMessage);

            // Add a message box
            var messageBox = new TopicChannel()
            {
                Name = "MessageBox",
                Key = "ContosoMessageBus:System:MessageBox",
                ResourceMapKey = "messageBox"
            };
            systemApp.Channels.Add(messageBox);

            // Add a message agent
            var messageAgent = new ContentBasedRouter()
            {
                Name = "MessageAgent",
                Key = "ContosoMessageBus:System:MessageAgent",
                ResourceMapKey = "messageAgent"
            };
            systemApp.Intermediaries.Add(messageAgent);

            // Add a process manager
            var processManager = new ProcessManager()
            {
                Name = "FtpTransformWorkflow",
                Key = "ContosoMessageBus:AppA:FtpTransformWorkflow",
                ResourceMapKey = "processManager"
            };
            processManager.WorkflowModel = GetWorkflowModel();
            app.Intermediaries.Add(processManager);

            // Add workflow model to process manager
            // Add an FTP endpoint
            var ftpReceive = new AdapterEndpoint()
            {
                Name = "FtpReceive",
                Key = "ContosoMessageBus:System:FtpReceive",
                ResourceMapKey = "ftpReceive"
            };
            systemApp.Endpoints.Add(ftpReceive);

            return model;
        }

        /// <summary>
        /// Gets a populated workflow model.
        /// </summary>
        /// <returns>A workflow model.</returns>
        public static WorkflowDefinition GetWorkflowModel()
        {
            var workflowModel = new WorkflowDefinition()
            {
                Name = "BizTalkServerProject.SimpleOrch",
                Key = "BizTalkServerProject.SimpleOrch",
                Type = "Workflow"
            };

            var workflowChannel = new WorkflowChannel()
            {
                Name = "ReceiveSendPort",
                Key = "BizTalkServerProject.SimpleOrch.ReceiveSendPort",
                Type = "BizTalkServerProject.ReceiveSendPT",
                Activator = true,
                Direction = MessageExchangePattern.ReceiveResponse
            };
            workflowChannel.Properties.Add("OrderedDelivery", "False");
            workflowChannel.Properties.Add("Binding", "Logical");
            workflowModel.Channels.Add(workflowChannel);

            var workflowMessageIn = new WorkflowMessage()
            {
                Name = "Message_1",
                Key = "BizTalkServerProject.SimpleOrch.Message_1",
                Type = "System.String"
            };
            workflowModel.Messages.Add(workflowMessageIn);
            workflowChannel.MessagesIn.Add(workflowMessageIn);

            var workflowMessageOut = new WorkflowMessage()
            {
                Name = "Message_2",
                Key = "BizTalkServerProject.SimpleOrch.Message_2",
                Type = "System.String"
            };
            workflowModel.Messages.Add(workflowMessageOut);
            workflowChannel.MessagesOut.Add(workflowMessageOut);

            var receiveActivity = new WorkflowActivity()
            {
                Name = "Receive_1",
                Key = "BizTalkServerProject.SimpleOrch.Receive_1",
                Type = "Receive"
            };
            receiveActivity.Properties.Add("Activate", "True");
            receiveActivity.Properties.Add("PortName", "ReceiveSendPort");
            receiveActivity.Properties.Add("MessageName", "Message_1");
            receiveActivity.Properties.Add("OperationName", "Operation_1");
            receiveActivity.Properties.Add("OperationMessageName", "Request");
            receiveActivity.Properties.Add("WorkflowChannel", "BizTalkServerProject.SimpleOrch.ReceiveSendPort");
            workflowModel.Activities.Add(receiveActivity);

            var constructActivity = new WorkflowActivity()
            {
                Name = "ConstructMessage_1",
                Key = "BizTalkServerProject.SimpleOrch.ConstructMessage_11",
                Type = "MessageConstruction"
            };
            constructActivity.Properties.Add("Expression0", "Message_2 = null; //\"hello \" + Message_1;");
            constructActivity.Properties.Add("ConstructedMessage", "Message_2");
            workflowModel.Activities.Add(constructActivity);

            var sendActivity = new WorkflowActivity()
            {
                Name = "Send_1",
                Key = "BizTalkServerProject.SimpleOrch.Send_12",
                Type = "Send"
            };
            sendActivity.Properties.Add("PortName", "ReceiveSendPort");
            sendActivity.Properties.Add("MessageName", "Message_2");
            sendActivity.Properties.Add("OperationName", "Operation_1");
            sendActivity.Properties.Add("OperationMessageName", "Response");
            sendActivity.Properties.Add("WorkflowChannel", "BizTalkServerProject.SimpleOrch.ReceiveSendPort");
            workflowModel.Activities.Add(sendActivity);

            return workflowModel;
        }

        /// <summary>
        /// Gets a model with target resources added manually for testing liquid functions.
        /// </summary>
        /// <returns></returns>
        public static AzureIntegrationServicesModel GetModelWithTargetResources()
        {
            var model = GetModel();
            var messageBus = model.MigrationTarget.MessageBus;

            messageBus.Resources.Add(new TargetResourceTemplate()
            {
                TemplateKey = "messageBusResourceKey",
                TemplateType = "microsoft.template.arm",
                ResourceName = "messageBusResource",
                ResourceType = "microsoft.groups.azureresourcegroup"
            });

            var systemApp = messageBus.Applications.Where(a => a.Name == "System").Single();
                        
            var messageBox = systemApp.Channels.Where(c => c.Key == "ContosoMessageBus:System:MessageBox").Single();
            messageBox.Resources.Add(new TargetResourceTemplate()
            {
                TemplateType = "microsoft.template.arm",
                TemplateKey = "topicChannelAzureServiceBusStandard",
                ResourceName = "messageBox",
                ResourceType = "microsoft.messaging.azureservicebus"
            });

            var messageAgent = systemApp.Intermediaries.Where(i => i.Key == "ContosoMessageBus:System:MessageAgent").Single();
            messageAgent.Resources.Add(new TargetResourceTemplate()
            {
                TemplateType = "microsoft.template.arm",
                TemplateKey = "messageAgentResourceKey",
                ResourceName = "messageAgent",
                ResourceType = "microsoft.workflows.azurelogicapp"
            });

            var ftpReceive = systemApp.Endpoints.Where(e => e.Key == "ContosoMessageBus:System:FtpReceive").Single();
            ftpReceive.Resources.Add(new TargetResourceTemplate()
            {
                TemplateType = "microsoft.template.arm",
                TemplateKey = "ftpReceiveKey",
                ResourceName = "ftpReceive",
                ResourceType = "microsoft.workflows.azurelogicapp"
            });

            var app = messageBus.Applications.Where(a => a.Name == "AppA").Single();

            var appMessage = app.Messages.Where(i => i.Key == "ContosoMessageBus:AppA:PurchaseOrderFlatFile").Single();
            appMessage.Resources.Add(new TargetResourceTemplate()
            {
                TemplateType = "microsoft.template.arm",
                TemplateKey = "purchaseOrderFlatFileKey",
                ResourceName = "purchaseOrderFlatFile",
                ResourceType = "microsoft.workflows.azurelogicapp"
            });

            var processManager = app.Intermediaries.Where(i => i.Key == "ContosoMessageBus:AppA:FtpTransformWorkflow").Single();
            processManager.Resources.Add(new TargetResourceTemplate()
            {
                TemplateType = "microsoft.template.arm",
                TemplateKey = "processManagerResourceKey",
                ResourceName = "processManager",
                ResourceType = "microsoft.workflows.azurelogicapp"
            });
            processManager.Snippets.Add(new TargetResourceSnippet()
            {
                SnippetKey = "workflow",
                SnippetType = "microsoft.snippet.json",
                ResourceName = "workflow-dev",
                ResourceType = "microsoft.workflow.definition.azurelogicapp"
            });

            return model;
        }
    }
}
