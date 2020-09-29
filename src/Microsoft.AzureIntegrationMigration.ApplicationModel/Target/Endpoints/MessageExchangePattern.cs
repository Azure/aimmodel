// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Endpoints
{
    /// <summary>
    /// Defines an enumeration of allowable values for the supported message exchange patterns.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These message exchange patterns are viewed from the perspective of the messaging system.
    /// </para>
    /// <para>
    /// All of the 'receive' patterns are used to represent the shape of the endpoint where a message
    /// has been received from an application and is sending to a channel.
    /// </para>
    /// <para>
    /// All of the 'send patterns are used to represent the shape of the endpoint where the endpoint
    /// is sending a message to the application that has been received from a channel.
    /// </para>
    /// </remarks>
    public enum MessageExchangePattern
    {
        /// <summary>
        /// Used when receiving a message but where an acknowledgement of the message having
        /// been processed and saved to durable storage is required.  Equivalent to an HTTP 200.
        /// </summary>
        Receive,

        /// <summary>
        /// Used when receiving a message but where an acknowledgement is only required when
        /// the message has been received but not processed or saved.  Equivalent to an HTTP 202.
        /// </summary>
        Accept,

        /// <summary>
        /// Used when receiving a message and where a response is expected by the endpoint.
        /// Equivalent to an HTTP 200 with a response in the body.
        /// </summary>
        ReceiveResponse,

        /// <summary>
        /// Used when sending a message to an endpoint where the recipient should acknowledge
        /// to the messaging system when the message has been processed and saved to durable storage.
        /// Equivalent to an HTTP 200.
        /// </summary>
        Send,

        /// <summary>
        /// Used when sending a message to an endpoint where the recipient only needs to acknowledge
        /// to the messaging system when it has received the message.  Equivalent to an HTTP 202.
        /// </summary>
        FireForget,

        /// <summary>
        /// Used when sending a message to an endpoint and where the recipient is expected to send
        /// a reply.  Equivalent to an HTTP 200 with a response in the body.
        /// </summary>
        RequestReply
    }
}
