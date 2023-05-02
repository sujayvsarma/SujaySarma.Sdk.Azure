
using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Properties of private endpoint connection
    /// </summary>
    public class PrivateEndpointConnectionProperties
    {

        /// <summary>
        /// Private endpoint
        /// </summary>
        [JsonProperty("privateEndpoint")]
        public SubResource PrivateEndpoint { get; set; } = new SubResource();

        /// <summary>
        /// Connection state
        /// </summary>
        [JsonProperty("privateLinkServiceConnectionState")]
        public PrivateLinkServiceConnectionState ConnectionState { get; set; } = new PrivateLinkServiceConnectionState();

        /// <summary>
        /// State of provisioning
        /// </summary>
        [JsonProperty("provisioningState")]
        public ProvisioningState ProvisioningState { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public PrivateEndpointConnectionProperties() { }
    }
}
