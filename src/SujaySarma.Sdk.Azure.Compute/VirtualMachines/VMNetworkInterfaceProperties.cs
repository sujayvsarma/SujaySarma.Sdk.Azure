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
    }
}
