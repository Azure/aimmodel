using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target
{
    /// <summary>
    /// Defines a class that represents the Azure Integration Services migration target.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class AzureIntegrationServicesMigrationTarget
    {
        /// <summary>
        /// Gets or sets the AIS environmment to target when converting the source to AIS.
        /// </summary>
        public AzureIntegrationServicesTargetEnvironment TargetEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the target deployment environment, such as dev, test, stage, prod, used
        /// as a key to find resources and resource templates in configuration.
        /// </summary>
        public string DeploymentEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the Azure subscription ID as the target for resources, where applicable,
        /// if a subscription ID must be chosen automatically for a logged in user.
        /// </summary>
        public string AzureSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the Azure primary region, where applicable, such as when deploying
        /// resource groups.
        /// </summary>
        public string AzurePrimaryRegion { get; set; }

        /// <summary>
        /// Gets or sets the Azure secondary region, where applicable, for BCDR.
        /// </summary>
        public string AzureSecondaryRegion { get; set; }

        /// <summary>
        /// Gets or sets a unique value that can be used to prefix or suffix a resource name in Azure
        /// as part of an Azure resource deployment.
        /// </summary>
        public string UniqueDeploymentId { get; set; }

        /// <summary>
        /// Gets or sets the message bus.
        /// </summary>
        public MessageBus MessageBus { get; set; }
    }
}
