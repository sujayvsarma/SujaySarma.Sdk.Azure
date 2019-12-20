using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// A resource with a URI
    /// </summary>
    public class UriResource
    {
        /// <summary>
        /// URI of the resource
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;

        public UriResource() { }

    }
}
