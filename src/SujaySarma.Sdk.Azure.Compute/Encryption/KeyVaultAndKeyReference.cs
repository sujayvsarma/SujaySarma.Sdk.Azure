
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// Key Vault Secret Url and vault id of the disk encryption key
    /// </summary>
    public class KeyVaultAndKeyReference
    {
        /// <summary>
        /// Url pointing to a key or secret in KeyVault
        /// </summary>
        [JsonProperty("keyUrl")]
        public string KeyUrl { get; set; } = string.Empty;

        /// <summary>
        /// Resource id of the KeyVault containing the key or secret
        /// </summary>
        [JsonProperty("sourceVault")]
        public SourceVault Vault { get; set; } = new SourceVault();

        public KeyVaultAndKeyReference() { }
    }
}
