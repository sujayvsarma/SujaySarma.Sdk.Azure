
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Private Endpoint connection
    /// </summary>
    public class PrivateEndpointConnection
    {
        /// <summary>
        /// Resource Id of the connection
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; } = string.Empty;

        /// <summary>
        /// Name of the endpoint
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of connection
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Properties of the connection
        /// </summary>
        [JsonProperty("properties")]
        public PrivateEndpointConnectionProperties Properties { get; set; } = new PrivateEndpointConnectionProperties();

        /// <summary>
        /// Default constructor
        /// </summary>
        public PrivateEndpointConnection() { }
    }
}
