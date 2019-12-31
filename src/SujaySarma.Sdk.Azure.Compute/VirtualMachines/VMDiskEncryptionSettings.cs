using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Compute.Encryption;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// A collection of encryption settings, one for each disk volume. During 
    /// create/update, this is initialized from different class constructors
    /// </summary>
    public class VMDiskEncryptionSettings
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

        /// <summary>
        /// If the encryption is enabled/disabled.
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; } = false;


        public VMDiskEncryptionSettings() { }
    }
}
