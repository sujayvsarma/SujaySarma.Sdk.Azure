using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Properties of a network interface
    /// </summary>
    public class VMNetworkInterfaceProperties
    {
        /// <summary>
        /// If true, this is the primary interface
        /// </summary>
        [JsonProperty("primary")]
        public bool IsPrimary { get; set; } = false;


        public VMNetworkInterfaceProperties() { }

        /// <summary>
        /// Specify that the interface is primary or otherwise
        /// </summary>
        /// <param name="isPrimary">If set, this is the primary network interface for the VM</param>
        public VMNetworkInterfaceProperties(bool isPrimary) => IsPrimary = isPrimary;
    }
}
