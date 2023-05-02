
using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;

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

        /// <summary>
        /// Initialize a new reference
        /// </summary>
        /// <param name="vaultUri">ResourceUri to the KeyVault</param>
        /// <param name="keyUrl">Absolute URL to the key in the KeyVault</param>
        public KeyVaultAndKeyReference(ResourceUri vaultUri, string keyUrl)
        {
            if (string.IsNullOrWhiteSpace(keyUrl)) { throw new ArgumentNullException(nameof(keyUrl)); }

            Vault = new SourceVault(vaultUri);
            KeyUrl = keyUrl;
        }
    }
}
