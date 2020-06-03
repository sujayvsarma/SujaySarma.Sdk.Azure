
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Encryption options for the storage account
    /// </summary>
    public class StorageAccountEncryption
    {
        /// <summary>
        /// Key source: Microsoft.KeyVault or Microsoft.Storage
        /// </summary>
        [JsonProperty("keySource")]
        public string KeySource { get; set; } = "Microsoft.KeyVault";

        /// <summary>
        /// Properties of key vault
        /// </summary>
        [JsonProperty("keyvaultproperties")]
        public KeyVaultProperties KeyVaultProperties { get; set; } = new KeyVaultProperties();

        /// <summary>
        /// Collection of Encryption service
        /// </summary>
        [JsonProperty("services")]
        public EncryptableServices? EncryptionService { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountEncryption() { }
    }
}
