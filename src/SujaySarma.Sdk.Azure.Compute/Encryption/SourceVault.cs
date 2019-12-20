
using Newtonsoft.Json;

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
    }
}
