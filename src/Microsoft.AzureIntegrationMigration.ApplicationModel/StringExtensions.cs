// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Globalization;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel
{
    /// <summary>
    /// Defines extension methods for the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a snake case sentence to a camel case string, removing the spaces between the words.
        /// </summary>
        /// <param name="snakeCase">The snake case sentence to convert.</param>
        /// <returns>The camel case representation.</returns>
        public static string ConvertSnakeCaseToCamelCase(this string snakeCase)
        {
            _ = snakeCase ?? throw new ArgumentNullException(nameof(snakeCase));

            // Uppercase the first letter of each word.
            var info = CultureInfo.InvariantCulture.TextInfo;
            var convertedText = info.ToTitleCase(snakeCase).Replace(" ", string.Empty);

            // Lower case the first character.
            return string.Concat(char.ToLower(convertedText[0], CultureInfo.InvariantCulture),convertedText.Substring(1));
        }
    }
}
