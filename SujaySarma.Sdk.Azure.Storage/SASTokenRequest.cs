using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Request for an SAS account token
    /// </summary>
    public class SASTokenRequest
    {

        /// <summary>
        /// The key to use to sign the SAS token
        /// </summary>
        [JsonProperty("keyToSign")]
        public string SigningKeyName { get; set; } = "Key1";

        /// <summary>
        /// Interval or duration or time at which the token is to expire
        /// </summary>
        [JsonProperty("signedExpiry")]
        public string ValidUpto { get; set; } = string.Empty;

        /// <summary>
        /// Time from when the token will be valid
        /// </summary>
        [JsonProperty("signedStart")]
        public string? ValidFrom { get; set; } = string.Empty;

        /// <summary>
        /// IP address to accept requests from. Can be a single IPv4 address or a CIDR range
        /// </summary>
        [JsonProperty("signedIp")]
        public string? AcceptedIpAddress { get; set; }

        /// <summary>
        /// Accepted protocols. Comma-seperated. "http", "https" or "http,https"
        /// </summary>
        [JsonProperty("signedProtocol")]
        public string? AcceptedProtocols { get; set; } = "https";

        /// <summary>
        /// Permissions to grant by token: (r)ead, (w)rite, (d)elete, (l)ist, (a)dd, (c)reate, (u)pdate or (p)rocess. 
        ///  Comma-seperated.
        /// </summary>
        [JsonProperty("signedPermission")]
        public string? Permissions { get; set; } = "r";

        /// <summary>
        /// Types of resources to grant access to:
        /// S - service level, C - container level, O - object level
        /// </summary>
        [JsonProperty("signedResourceTypes")]
        public string? ResourceTypes { get; set; } = "o";

        /// <summary>
        /// Services to grant access to: (b)lob, (q)ueue, (t)able, (f)ile.  Comma-seperated.
        /// </summary>
        [JsonProperty("signedServices")]
        public string? Services { get; set; } = "b";

        /// <summary>
        /// Default constructor
        /// </summary>
        public SASTokenRequest() { }

    }
}
