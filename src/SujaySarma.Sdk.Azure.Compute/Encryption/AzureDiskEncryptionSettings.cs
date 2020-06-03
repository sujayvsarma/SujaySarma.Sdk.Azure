
using Newtonsoft.Json;

using System;
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
        /// If Options is null in the request object, the existing settings remain unchanged.
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

        /// <summary>
        /// Enable encryption
        /// </summary>
        /// <param name="encryptionWithAzureApp">Set to true to use legacy disk encryption using an Azure AD App</param>
        /// <param name="diskEncryptionKey">The Disk Encryption Key (mandatory)</param>
        /// <param name="keyEncryptionKey">(Optional) Encryption key for the disk encryption key</param>
        public AzureDiskEncryptionSettings(bool encryptionWithAzureApp, KeyVaultAndSecretReference diskEncryptionKey, KeyVaultAndKeyReference? keyEncryptionKey = null)
        {
            if (diskEncryptionKey == null) { throw new ArgumentNullException(nameof(diskEncryptionKey)); }

            Enabled = true;

            Version = encryptionWithAzureApp switch
            {
                true => "1.0",
                false => "1.1"
            };

            Options = new List<AzureDiskEncryptionOption>()
            {
                new AzureDiskEncryptionOption()
                {
                    DiskEncryptionKey = diskEncryptionKey,
                    KeyEncryptionKey = keyEncryptionKey
                }
            };
        }

        /// <summary>
        /// Add encryption keys to the Options
        /// </summary>
        /// <param name="enableIfDisabled">Enables the encryption if currently disabled. Will throw an exception if this is FALSE and encryption is not enabled.</param>
        /// <param name="diskEncryptionKey">The Disk Encryption Key (mandatory)</param>
        /// <param name="keyEncryptionKey">(Optional) Encryption key for the disk encryption key</param>
        public void AddKeys(bool enableIfDisabled, KeyVaultAndSecretReference diskEncryptionKey, KeyVaultAndKeyReference? keyEncryptionKey = null)
        {
            if (diskEncryptionKey == null) { throw new ArgumentNullException(nameof(diskEncryptionKey)); }

            if (!Enabled)
            {
                if (!enableIfDisabled)
                {
                    throw new InvalidOperationException();
                }

                Enabled = true;
                Version = "1.1";
            }

            if (Options == null)
            {
                Options = new List<AzureDiskEncryptionOption>();
            }

            Options.Add(new AzureDiskEncryptionOption() { DiskEncryptionKey = diskEncryptionKey, KeyEncryptionKey = keyEncryptionKey });
        }

        /// <summary>
        /// Disables encryption
        /// </summary>
        public void DisableEncryption()
        {
            Enabled = false;
            Options?.Clear();
        }
    }
}
