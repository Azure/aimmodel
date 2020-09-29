// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Defines an enumeration of allowable values for the types of messages supported by
    /// the message bus.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The default message type is a document message, representing a normal business message.
        /// </summary>
        Document,

        /// <summary>
        /// Represents a message that is defined in an envelope format with one or more headers and
        /// one or more body messages as documents.
        /// </summary>
        Envelope,

        /// <summary>
        /// Represents a message that is a command, which is used to issue commands to applications,
        /// for example, issuing commands to applications running on devices.
        /// </summary>
        Command,

        /// <summary>
        /// Represents a message that is an event, which is used to inform an application that something
        /// happened that can be used to trigger further downstream processing, such as an alert.
        /// </summary>
        Event,

        /// <summary>
        /// Represents a message that is an acknowledgement of receipt or the completed processing of
        /// a message, typically used in a request-reply scenario, where no business message is available
        /// as a response, but the calling application still requires acknowledgement.
        /// </summary>
        Ack,

        /// <summary>
        /// Represents a message that is a negative acknowledgement, or an acknowledgement of a failure
        /// to receive and process a message in a request-reply scenario.
        /// </summary>
        Nack
    }
}
