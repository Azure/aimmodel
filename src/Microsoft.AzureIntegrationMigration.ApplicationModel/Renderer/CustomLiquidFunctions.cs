// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AzureIntegrationMigration.ApplicationModel.Target;
using Newtonsoft.Json.Linq;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Renderer
{
    /// <summary>
    /// Defines a class of custom functions used in a Liquid template.
    /// </summary>
    public static class CustomLiquidFunctions
    {
        /// <summary>
        /// Defines the name of the file containing the Azure regions.
        /// </summary>
        private const string RegionFile = "regions.json";

        /// <summary>
        /// Defines an object containing Azure regions.
        /// </summary>
        private static JArray s_regions;

        /// <summary>
        /// Lock object for regions variable.
        /// </summary>
        private static readonly object s_regionLock = new object();

        /// <summary>
        /// Finds a specific target resource template in the target model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="templateKey">The key for the target resource.</param>
        /// <returns>The target resource template or null if not found or arguments are null.</returns>
        public static TargetResourceTemplate FindResourceTemplate(AzureIntegrationServicesModel model, string templateKey)
        {
            if (model == null)
            {
                return null;
            }

            if (templateKey == null)
            {
                return null;
            }

            if (model?.MigrationTarget?.MessageBus != null)
            {
                foreach (var application in model.MigrationTarget.MessageBus.Applications)
                {
                    foreach (var message in application.Messages)
                    {
                        var targetResource = message.Resources.Where(r => r.TemplateKey == templateKey).FirstOrDefault();
                        if (targetResource != null)
                        {
                            return targetResource;
                        }
                    }

                    foreach (var channel in application.Channels)
                    {
                        var targetResource = channel.Resources.Where(r => r.TemplateKey == templateKey).FirstOrDefault();
                        if (targetResource != null)
                        {
                            return targetResource;
                        }
                    }

                    foreach (var intermediary in application.Intermediaries)
                    {
                        var targetResource = intermediary.Resources.Where(r => r.TemplateKey == templateKey).FirstOrDefault();
                        if (targetResource != null)
                        {
                            return targetResource;
                        }
                    }

                    foreach (var endpoint in application.Endpoints)
                    {
                        var targetResource = endpoint.Resources.Where(r => r.TemplateKey == templateKey).FirstOrDefault();
                        if (targetResource != null)
                        {
                            return targetResource;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds all target resource templates in the target model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The target resource templates or null if not found or arguments are null.</returns>
        public static IEnumerable<TargetResourceTemplate> FindAllResourceTemplates(AzureIntegrationServicesModel model)
        {
            if (model == null)
            {
                return null;
            }
            
            return model.MigrationTarget?.MessageBus?.Resources
                .Union(model.MigrationTarget?.MessageBus?.Applications.SelectMany(a => a.Resources))
                .Union(model.MigrationTarget?.MessageBus?.Applications.SelectMany(a => a.Messages).SelectMany(m => m.Resources))
                .Union(model.MigrationTarget?.MessageBus?.Applications.SelectMany(a => a.Channels).SelectMany(c => c.Resources))
                .Union(model.MigrationTarget?.MessageBus?.Applications.SelectMany(a => a.Intermediaries).SelectMany(i => i.Resources))
                .Union(model.MigrationTarget?.MessageBus?.Applications.SelectMany(a => a.Endpoints).SelectMany(e => e.Resources));
        }

        /// <summary>
        /// Gets the value of an environment variable, or null if it doesn't exist or has no value.
        /// </summary>
        /// <param name="name">The name of the environment variable.</param>
        /// <returns>The environment variable or null if not found or arguments are null.</returns>
        public static string GetEnvironmentVariable(string name)
        {
            if (name == null)
            {
                return null;
            }

            // Get environment variable
            var env = Environment.GetEnvironmentVariable(name);
            return env;
        }

        /// <summary>
        /// Formats the Azure region display name into the name.
        /// </summary>
        /// <param name="region">The Azure region.</param>
        /// <returns>A formatted region value or null if not found or arguments are null.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "The AIS resource names will be formatted as lowercase.")]
        public static string FormatRegion(string region)
        {
            if (region == null)
            {
                return null;
            }

            // Load config as JSON
            if (s_regions == null)
            {
                lock (s_regionLock)
                {
                    if (s_regions == null)
                    {
                        using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(RegionFile)))
                        {
                            var json = reader.ReadToEnd();
                            s_regions = JArray.Parse(json);
                        }
                    }
                }
            }

            // Returns the name version, from the display name, or returns null if region doesn't exist
            var jsonPath = $"$[?(@.DisplayName == '{region}')]";
            var formattedRegion = s_regions.SelectToken(jsonPath);
            if (formattedRegion != null)
            {
                return formattedRegion["Name"].Value<string>();
            }

            return null;
        }

        /// <summary>
        /// Converts a string to a JSON compatible string.
        /// </summary>
        /// <param name="value">A JSON string.</param>
        /// <returns>An escaped JSON string that can be used as a value of a JSON property.</returns>
        public static string ToJsonString(string value)
        {
            if (value == null)
            {
                return null;
            }

            var jsonString = HttpUtility.JavaScriptStringEncode(value);
            return jsonString;
        }

        /// <summary>
        /// Converts a filepath string by removing illegal characters.
        /// </summary>
        /// <param name="filePath">The file path to convert.</param>
        /// <returns>The converted file path.</returns>
        public static string ToSafeFilePath(string filePath)
        {
            if (filePath == null)
            {
                return null;
            }
            
            // Strip out any double forward or back slashes.
            var safeFilePath = filePath.Replace("\\\\", "\\").Replace("//", "/");

            // Normalise to a forward slash path separator.
            safeFilePath = safeFilePath.Replace("\\", "/");

            var pattern = new Regex("[\\/:*?\"<>| ]", RegexOptions.Compiled);

            // Loop through the folders in the path and sanitize each folder.
            var sanitizedFolders = new List<string>();            
            foreach (var folder in safeFilePath.Split('/'))
            {
                // Remove invalid characters.
                var sanitizedFolder = pattern.Replace(folder, string.Empty);

                if (!string.IsNullOrEmpty(sanitizedFolder))
                {
                    sanitizedFolders.Add(sanitizedFolder);
                }
            }

            // Return the sanitized path.
            return string.Join("/", sanitizedFolders);
        }
    }
}
