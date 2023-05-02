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

        /// <summary>
        /// Create a new object
        /// </summary>
        public UriResource() { }

        /// <summary>
        /// Create a new object
        /// </summary>
        /// <param name="uri">Uri to the resource</param>
        public UriResource(string uri) => Uri = (string.IsNullOrWhiteSpace(uri) ? throw new System.ArgumentNullException(nameof(uri)) : uri);
    }
}
