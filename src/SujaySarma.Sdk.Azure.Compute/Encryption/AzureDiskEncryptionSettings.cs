
using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// Options to encrypt the disk itself using Azure Disk Encryption and can contain multiple 
    /// settings per disk/snapshot
    /// </summary>
    public class AzureDiskEncryptionSettings
    {
        /// <summary>
        /// Set this flag to true and provide DiskEncryptionKey and optional KeyEncryptionKey to enable encryption. 
        /// Set this flag to false and remove DiskEncryptionKey and KeyEncryptionKey to disable encryption. 
        /// If EncryptionSettings is null in the request object, the existing settings remain unchanged.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Version of encryption: "1.0" - Azure Disk Encryption with AAD App, "1.1" - Azure Disk Encryption. 
        /// Cannot be changed after setting it.
        /// </summary>
        [JsonProperty("encryptionSettingsVersion")]
        public string? Version { get; set; } = null;

        /// <summary>
        /// A collection of encryption settings, one for each disk volume.
        /// </summary>
        [JsonProperty("encryptionSettings")]
        public List<AzureDiskEncryptionOption>? Options { get; set; } = null;

        public AzureDiskEncryptionSettings() { }
    }
}
