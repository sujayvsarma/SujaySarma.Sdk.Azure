using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Hardware profile of the VM. This is done simply by refering to the size-code of the VM.
    /// </summary>
    public class VMHardwareProfile
    {
        /// <summary>
        /// Size-code of a VM (eg: "Basic_A0")
        /// </summary>
        [JsonProperty("vmSize")]
        public string VMSize { get; set; } = "Basic_A0";

        public VMHardwareProfile() { }

        public VMHardwareProfile(string sizeName)
        {
            if (string.IsNullOrWhiteSpace(sizeName)) { throw new ArgumentNullException(nameof(sizeName)); }
            VMSize = sizeName;
        }
    }
}
