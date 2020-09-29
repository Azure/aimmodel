// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that routes by splitting a single message into multiple
    /// messages.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class Splitter : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Splitter"/> class.
        /// </summary>
        public Splitter()
            : base(MessageRouterType.Splitter)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Splitter"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the splitter.</param>
        public Splitter(string name)
            : base(name, MessageRouterType.Splitter)
        {
        }
    }
}
