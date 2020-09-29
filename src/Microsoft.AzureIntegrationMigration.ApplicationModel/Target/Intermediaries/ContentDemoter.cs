// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that demotes content in the header of a message
    /// back into the body of the message before the body is sent to the application
    /// by an endpoint.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class ContentDemoter : MessageProcessor
    {
        /// <summary>
        /// Constructs an instance of the <see cref="ContentDemoter"/> class.
        /// </summary>
        public ContentDemoter()
            : base(MessageProcessorType.ContentDemoter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="ContentPromoter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the content demoter.</param>
        public ContentDemoter(string name)
            : base(name, MessageProcessorType.ContentDemoter)
        {
        }
    }
}
