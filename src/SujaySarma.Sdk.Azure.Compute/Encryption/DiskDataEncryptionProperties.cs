
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// Options to encrypt the contents/data in the disk, at rest, using user/platform-provided keys
    /// </summary>
    public class DiskDataEncryptionProperties
    {
        /// <summary>
        /// ResourceId of the disk encryption set to use for enabling encryption at rest
        /// </summary>
        [JsonProperty("diskEncryptionSetId")]
        public string EncryptionSetResourceId { get; set; } = string.Empty;

        /// <summary>
        /// The type of key used to encrypt the data of the disk
        /// </summary>
        [JsonProperty("type")]
        public DiskDataEncryptionTypeNamesEnum Type { get; set; }


        public DiskDataEncryptionProperties() { }
    }
}
