
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Routing preference for the storage endpoints
    /// </summary>
    public class RoutingPreference
    {
        /// <summary>
        /// Specify if internet routing storage endpoints are to be published
        /// </summary>
        [JsonProperty("publishInternetEndpoints")]
        public bool AreInternetEndpointsPublished { get; set; }

        /// <summary>
        /// Specify if Microsoft routing storage endpoints to be published
        /// </summary>
        [JsonProperty("publishMicrosoftEndpoints")]
        public bool AreMicrosoftEndpointsPublished { get; set; }

        /// <summary>
        /// Choice of routing opted
        /// </summary>
        [JsonProperty("routingChoice"), JsonConverter(typeof(StringEnumConverter))]
        public RoutingChoice RoutingChoice { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RoutingPreference() { }
    }
}
