using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// A uri with a content version
    /// </summary>
    public class VersionedUri
    {
        [JsonProperty("contentVersion")]
        public string? Version { get; set; } = "1.0.0.0";


        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">Uri to the target</param>
        public VersionedUri(string uri) => Uri = uri;

    }

}
