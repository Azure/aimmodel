using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AzureIntegrationMigration.ApplicationModel.Target.Messages
{
    /// <summary>
    /// Defines an enumeration of allowable values for the content type of messages.
    /// </summary>
    public enum MessageContentType
    {
        /// <summary>
        /// The default message type is a JSON structured message.
        /// </summary>
        Json,

        /// <summary>
        /// Represents a XML structured message.
        /// </summary>
        Xml,

        /// <summary>
        /// Represents a text based message that uses delimiters to separate message fields.
        /// </summary>
        Delimited,

        /// <summary>
        /// Represents a text based message that uses specific positions in the message to
        /// separate fields.
        /// </summary>
        Positional,

        /// <summary>
        /// Represents a YAML structured message.
        /// </summary>
        Yaml,

        /// <summary>
        /// Represents a binary formatted message.
        /// </summary>
        Binary,

        /// <summary>
        /// Represents a message formatted using protocol buffers.
        /// </summary>
        Protobuf
    }
}
