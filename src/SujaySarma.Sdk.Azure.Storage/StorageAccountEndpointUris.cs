
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Uris to different Storage account endpoints
    /// </summary>
    public class StorageAccountEndpointUris
    {
        /// <summary>
        /// Blob service
        /// </summary>
        [JsonProperty("blob")]
        public string BlobUri { get; set; } = string.Empty;

        /// <summary>
        /// DFS service
        /// </summary>
        [JsonProperty("dfs")]
        public string DfsUri { get; set; } = string.Empty;

        /// <summary>
        /// File service
        /// </summary>
        [JsonProperty("file")]
        public string FileUri { get; set; } = string.Empty;

        /// <summary>
        /// Queue service
        /// </summary>
        [JsonProperty("queue")]
        public string QueueUri { get; set; } = string.Empty;

        /// <summary>
        /// Table service
        /// </summary>
        [JsonProperty("table")]
        public string TableUri { get; set; } = string.Empty;

        /// <summary>
        /// Website (configured on a Blob account)
        /// </summary>
        [JsonProperty("web")]
        public string WebUri { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountEndpointUris() { }
    }
}
