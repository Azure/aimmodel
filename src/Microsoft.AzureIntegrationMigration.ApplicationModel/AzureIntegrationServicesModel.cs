using System;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Report;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Source;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.Runner.Model;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel
{
    /// <summary>
    /// Defines the entry point for the application model used for migrating applications to Azure Integration Services.
    /// </summary>
    [Serializable]
    public partial class AzureIntegrationServicesModel : IApplicationModel
    {
        /// <summary>
        /// Gets the model object representing the migration source.
        /// </summary>
        public MigrationSource MigrationSource { get; } = new MigrationSource();

        /// <summary>
        /// Gets the model object representing the Azure Integration Services solution that will be created after migration.
        /// </summary>
        public AzureIntegrationServicesMigrationTarget MigrationTarget { get; } = new AzureIntegrationServicesMigrationTarget();

        /// <summary>
        /// Gets the report created as part of an assessment.
        /// </summary>
        public ReportModel Report { get; } = new ReportModel();
    }
}
