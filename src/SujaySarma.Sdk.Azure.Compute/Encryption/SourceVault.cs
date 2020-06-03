
using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// Key vault for an encryption
    /// </summary>
    public class SourceVault
    {
        /// <summary>
        /// Absolute resource Id of the key vault
        /// </summary>
        [JsonProperty("id")]
        public string KeyVaultResourceId { get; set; } = string.Empty;


        public SourceVault() { }

        /// <summary>
        /// Create reference to a source KeyVault
        /// </summary>
        /// <param name="vaultUri">ResourceUri to an Azure KeyVault</param>
        public SourceVault(ResourceUri vaultUri)
        {
            if ((vaultUri == null) || (!vaultUri.IsValid) || (!vaultUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.KeyVault")) ||
                (!vaultUri.Is(ResourceUriCompareLevel.Type, "vaults")))
            {
                throw new ArgumentException(nameof(vaultUri));
            }

            KeyVaultResourceId = vaultUri.ToString();
        }
    }
}
