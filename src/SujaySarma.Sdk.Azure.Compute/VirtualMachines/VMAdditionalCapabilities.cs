using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Additional capabilities enabled/disabled on the VM
    /// </summary>
    public class VMAdditionalCapabilities
    {
        /// <summary>
        /// The flag that enables or disables a capability to have one or more managed data disks 
        /// with UltraSSD_LRS storage account type on the VM or VMSS. Managed disks with storage 
        /// account type UltraSSD_LRS can be added to a virtual machine or virtual machine scale 
        /// set only if this property is enabled
        /// </summary>
        [JsonProperty("ultraSSDEnabled")]
        public bool IsUltraSSDEnabled { get; set; } = false;

        public VMAdditionalCapabilities() { }

        /// <summary>
        /// Enable/disable Ultra SSD
        /// </summary>
        /// <param name="enableUltraSSD">Set Ultra SSD enabled/disabled</param>
        public VMAdditionalCapabilities(bool enableUltraSSD)
        {
            IsUltraSSDEnabled = enableUltraSSD;
        }
    }
}
