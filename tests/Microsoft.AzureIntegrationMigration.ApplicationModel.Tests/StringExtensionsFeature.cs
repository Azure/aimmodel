using System;
using FluentAssertions;
using Xbehave;
using Xunit;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Tests
{
#pragma warning disable CA1303 // Do not pass literals as localized parameters
    /// <summary>
    /// Tests for the <see cref="StringExtensions"/> class.
    /// </summary>
    public class StringExtensionsFeature
    {
        /// <summary>
        /// Scenario tests when a null string is supplied.
        /// </summary>
        /// <param name="snakeCase">The snake case string to convert.</param>
        /// <param name="camelCase">The converted camel case string.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConvertWithNullString(string snakeCase, string camelCase, Exception e)
        {
            "Given a null snake case string"
               .x(() => snakeCase.Should().BeNull());

            "When converting to camel case"
                .x(() => e = Record.Exception(() => camelCase = snakeCase.ConvertSnakeCaseToCamelCase())); ;

            "Then the resource should throw an exception"
                .x(() => e.Should().NotBeNull());
        }

        /// <summary>
        /// Scenario tests when a a string is supplied with one word.
        /// </summary>
        /// <param name="snakeCase">The snake case string to convert.</param>
        /// <param name="camelCase">The converted camel case string.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConvertWithOneWord(string snakeCase, string camelCase, Exception e)
        {
            "Given a snake case string"
               .x(() => snakeCase = "test");

            "When converting to camel case"
                .x(() => e = Record.Exception(() => camelCase = snakeCase.ConvertSnakeCaseToCamelCase())); ;

            "Then the resource should NOT throw an exception"
                .x(() => e.Should().BeNull());

            "And the config will be formatted"
                .x(() =>
                {
                    camelCase.Should().NotBeNullOrEmpty();
                    camelCase.Should().Be("test");
                });
        }

        /// <summary>
        /// Scenario tests when a a string is supplied with multiple words.
        /// </summary>
        /// <param name="snakeCase">The snake case string to convert.</param>
        /// <param name="camelCase">The converted camel case string.</param>
        /// <param name="e">The exception, if any.</param>
        [Scenario]
        [Trait(TestConstants.TraitCategory, TestConstants.CategoryUnitTest)]
        public void ConvertWithMultipleWords(string snakeCase, string camelCase, Exception e)
        {
            "Given a snake case string"
               .x(() => snakeCase = "this is a test case sentence");

            "When converting to camel case"
                .x(() => e = Record.Exception(() => camelCase = snakeCase.ConvertSnakeCaseToCamelCase())); ;

            "Then the resource should NOT throw an exception"
                .x(() => e.Should().BeNull());

            "And the config will be formatted"
                .x(() =>
                {
                    camelCase.Should().NotBeNullOrEmpty();
                    camelCase.Should().Be("thisIsATestCaseSentence");
                });
        }
    }
#pragma warning restore CA1303 // Do not pass literals as localized parameters
}
