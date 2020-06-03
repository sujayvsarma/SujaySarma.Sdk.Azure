
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// State of connection of a Private Link service
    /// </summary>
    public class PrivateLinkServiceConnectionState
    {
        /// <summary>
        /// Message indicating what changes are required on the consumer end
        /// </summary>
        [JsonProperty("actionRequired")]
        public string? ActionRequired { get; set; }

        /// <summary>
        /// Reason for approval/rejection of the connection
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the connection has been Approved/Rejected/Removed by the owner of the service.
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public ConnectionState ConnectionState { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PrivateLinkServiceConnectionState() { }
    }
}
