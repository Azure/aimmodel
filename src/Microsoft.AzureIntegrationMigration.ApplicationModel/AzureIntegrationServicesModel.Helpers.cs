using System;
using System.Linq;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Report;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Source;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Microsoft.AzureIntegrationMigration.Runner.Model;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel
{
    /// <summary>
    /// Defines the entry point for the application model used for migrating applications to Azure Integration Services.
    /// </summary>
    public partial class AzureIntegrationServicesModel : IApplicationModel
    {
        /// <summary>
        /// Gets the source model in the <see cref="MigrationSource"/> object as a typed object.
        /// </summary>
        /// <typeparam name="TSourceModel">The type of the source model.</typeparam>
        /// <returns>The source model cast to the specified type, or default value if the model doesn't exist or isn't of the specified type.</returns>
        public TSourceModel GetSourceModel<TSourceModel>()
        {
            if (MigrationSource.MigrationSourceModel != null)
            {
                if (MigrationSource.MigrationSourceModel is TSourceModel)
                {
                    return (TSourceModel)MigrationSource.MigrationSourceModel;
                }
            }

            return default;
        }

        /// <summary>
        /// Finds a messaging object based on its key and its parents, if applicable.
        /// </summary>
        /// <param name="key">The key for the messaging object.</param>
        /// <returns>
        /// Returns the hierarchy of the messaging object, if applicable, or null for some if the key
        /// represents a message bus or an application.
        /// </returns>
        public (MessageBus messageBus, Application application, MessagingObject messagingObject) FindMessagingObject(string key)
        {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            // Message Bus
            var messageBus = MigrationTarget?.MessageBus;
            if (messageBus != null)
            {
                if (messageBus.Key == key)
                {
                    return (messageBus, null, null);
                }
                else
                {
                    // Applications
                    if (messageBus.Applications.Any())
                    {
                        foreach (var application in messageBus.Applications)
                        {
                            if (application.Key == key)
                            {
                                return (messageBus, application, null);
                            }
                            else
                            {
                                // Messages
                                if (application.Messages.Any())
                                {
                                    foreach (var msg in application.Messages)
                                    {
                                        if (msg.Key == key)
                                        {
                                            return (messageBus, application, msg);
                                        }
                                    }
                                }

                                // Channels
                                if (application.Channels.Any())
                                {
                                    foreach (var channel in application.Channels)
                                    {
                                        if (channel.Key == key)
                                        {
                                            return (messageBus, application, channel);
                                        }
                                    }
                                }

                                // Endpoints
                                if (application.Endpoints.Any())
                                {
                                    foreach (var endpoint in application.Endpoints)
                                    {
                                        if (endpoint.Key == key)
                                        {
                                            return (messageBus, application, endpoint);
                                        }
                                    }
                                }

                                // Intermediaries
                                if (application.Intermediaries.Any())
                                {
                                    foreach (var intermediary in application.Intermediaries)
                                    {
                                        if (intermediary.Key == key)
                                        {
                                            return (messageBus, application, intermediary);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return (null, null, null);
        }
    }
}
