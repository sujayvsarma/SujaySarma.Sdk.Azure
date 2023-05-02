
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Properties of the Azure KeyVault
    /// </summary>
    public class KeyVaultProperties
    {
        /// <summary>
        /// The Id of the current key (in versioned KeyVault keys)
        /// </summary>
        [JsonProperty("currentVersionedKeyIdentifier")]
        public string? CurrentKeyVersionIdentifier { get; set; }

        /// <summary>
        /// Name of the key
        /// </summary>
        [JsonProperty("keyname")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Uri to the KeyVault
        /// </summary>
        [JsonProperty("keyvaulturi")]
        public string VaultUri { get; set; } = string.Empty;

        /// <summary>
        /// Version of the key
        /// </summary>
        [JsonProperty("keyversion")]
        public string? Version { get; set; }

        /// <summary>
        /// Timestamp of the last time the key was rotated
        /// </summary>
        [JsonProperty("lastKeyRotationTimestamp")]
        public string? LastKeyRotationTimestamp { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public KeyVaultProperties() { }

    }
}
