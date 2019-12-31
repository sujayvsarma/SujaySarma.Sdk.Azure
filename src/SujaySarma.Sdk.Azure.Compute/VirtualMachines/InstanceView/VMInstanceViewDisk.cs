using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Disks currently attached to the VM
    /// </summary>
    public class VMInstanceViewDisk
    {
        /// <summary>
        /// Name of the disk
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Status information as the disk spun up
        /// </summary>
        [JsonProperty("status")]
        public List<InstanceViewStatus>? Status { get; set; }

        /// <summary>
        /// Set only for the OS/primary disk, provides the encryption settings
        /// </summary>
        [JsonProperty("encryptionSettings")]
        public List<VMDiskEncryptionSettings>? OSDiskEncryptionSettings { get; set; }


        public VMInstanceViewDisk() { }
    }
}
