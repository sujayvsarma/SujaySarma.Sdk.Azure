using System.Collections.Generic;
using Newtonsoft.Json;
using SujaySarma.Sdk.Azure.Compute.Encryption;

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
    }
}
