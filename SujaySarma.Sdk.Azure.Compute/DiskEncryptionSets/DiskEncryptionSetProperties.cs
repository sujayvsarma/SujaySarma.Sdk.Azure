using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Encryption;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.DiskEncryptionSets
{
    /// <summary>
    /// Properties of a disk encryption set
    /// </summary>
    public class DiskEncryptionSetProperties
    {
        /// <summary>
        /// State of the provisioning of the set
        /// </summary>
        [JsonProperty("provisioningState")]
        public string ProvisioningState { get; set; } = "Succeeded";

        /// <summary>
        /// The key vault key which is currently used by this disk encryption set
        /// </summary>
        [JsonProperty("activeKey")]
        public KeyVaultAndKeyReference? ActiveKey { get; set; } = null;

        /// <summary>
        /// A readonly collection of key vault keys previously used by this disk 
        /// encryption set while a key rotation is in progress. It will be empty if there is no ongoing key rotation.
        /// </summary>
        [JsonProperty("previousKeys")]
        public List<KeyVaultAndKeyReference>? PreviousKeys { get; set; } = null;


        public DiskEncryptionSetProperties() { }

        /// <summary>
        /// Create a property pointing to an active key
        /// </summary>
        /// <param name="keyVaultUri">The Resource Uri to the KeyVault where the key is interned</param>
        /// <param name="keyVaultKeyUri">The absolute Uri to the key in the KeyVault</param>
        public DiskEncryptionSetProperties(ResourceUri keyVaultUri, string keyVaultKeyUri)
        {
            if ((!keyVaultUri.IsValid) || (!keyVaultUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.KeyVault")) || (!keyVaultUri.Is(ResourceUriCompareLevel.Type, "vaults")))
            {
                throw new ArgumentException(nameof(keyVaultUri));
            }

            ActiveKey = new KeyVaultAndKeyReference()
            {
                Vault = new SourceVault()
                {
                    KeyVaultResourceId = keyVaultUri.ToString()
                },
                KeyUrl = keyVaultKeyUri
            };
        }
    }
}
