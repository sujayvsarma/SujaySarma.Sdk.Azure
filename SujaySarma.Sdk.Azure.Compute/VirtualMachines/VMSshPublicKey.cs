using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// A public key for use with SSH
    /// </summary>
    public class VMSshPublicKey
    {
        /// <summary>
        /// The (min)2048-bit public key certificate in SSH-RSA format
        /// </summary>
        [JsonProperty("keyData")]
        public string KeyData { get; set; } = string.Empty;

        /// <summary>
        /// The path on disk within the VM where the file has been uploaded to
        /// </summary>
        [JsonProperty("path")]
        public string UserProfileKeyFileName { get; set; } = "/home/dummy/.ssh/authorized_keys";

        public VMSshPublicKey() { }
    }
}
