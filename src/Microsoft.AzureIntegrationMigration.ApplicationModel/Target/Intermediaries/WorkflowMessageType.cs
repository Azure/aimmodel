using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Intermediaries
{
    /// <summary>
    /// Defines an enumeration of allowable values for the types of messages in a workflow.
    /// </summary>
    public enum WorkflowMessageType
    {
        /// <summary>
        /// The default message type is a request message.
        /// </summary>
        Request,

        /// <summary>
        /// Represents a message that is a response to a request.
        /// </summary>
        Response,

        /// <summary>
        /// Represents a fault message that is a response to a failed request.
        /// </summary>
        Fault
    }
}
