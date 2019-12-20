using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Compute.DiskEncryptionSets
{
    /// <summary>
    /// Defines a disk encryption set
    /// </summary>
    public class DiskEncryptionSet : AzureObjectBase
    {
        /// <summary>
        /// The encryption set's identity
        /// </summary>
        [JsonProperty("identity")]
        public DiskEncryptionSetIdentity? Identity { get; set; } = null;

        /// <summary>
        /// Properties of the set
        /// </summary>
        [JsonProperty("properties")]
        public DiskEncryptionSetProperties? Properties { get; set; } = null;


        public DiskEncryptionSet() : base() { }

    }
}
