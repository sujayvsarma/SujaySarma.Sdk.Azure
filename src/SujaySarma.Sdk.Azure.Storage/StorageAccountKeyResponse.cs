using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Response returned by the API
    /// </summary>
    public class StorageAccountKeyResponse
    {
        /// <summary>
        /// Keys
        /// </summary>
        [JsonProperty("keys")]
        public List<StorageAccountKey> Keys { get; set; } = new List<StorageAccountKey>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountKeyResponse() { }
    }
}
