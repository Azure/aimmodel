// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Represents an intermediary that routes messages in sequence by resequencing
    /// input messages.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    public class Resequencer : MessageRouter
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Resequencer"/> class.
        /// </summary>
        public Resequencer()
            : base(MessageRouterType.Resequencer)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Resequencer"/> class with a name.
        /// </summary>
        /// <param name="name">The name of the resequencer.</param>
        public Resequencer(string name)
            : base(name, MessageRouterType.Resequencer)
        {
        }
    }
}
