
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Properties of an encryption service
    /// </summary>
    public class EncryptionService
    {
        /// <summary>
        /// If this service is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Type of key used
        /// </summary>
        [JsonProperty("keyType"), JsonConverter(typeof(StringEnumConverter))]
        public EncryptionServiceKeyType KeyType { get; set; }

        /// <summary>
        /// Timestamp of when the service was last in enabled state
        /// </summary>
        [JsonProperty("lastEnabledTime")]
        public string? LastEnabledTime { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public EncryptionService() { }
    }
}
