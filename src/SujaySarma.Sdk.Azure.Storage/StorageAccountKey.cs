using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// A key for a storage account
    /// </summary>
    public class StorageAccountKey
    {
        /// <summary>
        /// Name of the key
        /// </summary>
        [JsonProperty("keyName")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The account key value
        /// </summary>
        [JsonProperty("value")]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Permissions grant this key. Typically these would be "Full", unless for some reason they are set to "readonly" (docs??)
        /// </summary>
        [JsonProperty("permissions")]
        public string Permissions { get; set; } = "Full";

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountKey() { }
    }
}
