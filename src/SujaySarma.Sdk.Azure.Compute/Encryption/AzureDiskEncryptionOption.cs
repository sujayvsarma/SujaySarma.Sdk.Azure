
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// A collection of encryption settings, one for each disk volume.
    /// </summary>
    public class AzureDiskEncryptionOption
    {
        /// <summary>
        /// Key Vault Secret Url and vault id of the disk encryption key
        /// </summary>
        [JsonProperty("diskEncryptionKey")]
        public KeyVaultAndSecretReference? DiskEncryptionKey { get; set; } = null;

        /// <summary>
        /// Key Vault Key Url and vault id of the key encryption key. KeyEncryptionKey is 
        /// optional and when provided is used to unwrap the disk encryption key.
        /// </summary>
        [JsonProperty("keyEncryptionKey")]
        public KeyVaultAndKeyReference? KeyEncryptionKey { get; set; } = null;


        public AzureDiskEncryptionOption() { }
    }
}
