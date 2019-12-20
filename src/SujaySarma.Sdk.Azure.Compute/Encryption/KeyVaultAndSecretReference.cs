
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// Key Vault Key Url and vault id of the key encryption key. KeyEncryptionKey is 
    /// optional and when provided is used to unwrap the disk encryption key.
    /// </summary>
    public class KeyVaultAndSecretReference
    {
        /// <summary>
        /// Url pointing to a key or secret in KeyVault
        /// </summary>
        [JsonProperty("secretUrl")]
        public string SecretUrl { get; set; } = string.Empty;

        /// <summary>
        /// Resource id of the KeyVault containing the key or secret
        /// </summary>
        [JsonProperty("sourceVault")]
        public SourceVault Vault { get; set; } = new SourceVault();

        public KeyVaultAndSecretReference() { }
    }
}
