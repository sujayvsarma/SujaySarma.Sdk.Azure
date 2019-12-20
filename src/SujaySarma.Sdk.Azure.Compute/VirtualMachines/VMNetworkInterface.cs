using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// A single network interface attached to a VM
    /// </summary>
    public class VMNetworkInterface
    {
        /// <summary>
        /// Id of the interface
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public VMNetworkInterfaceProperties Properties { get; set; } = new VMNetworkInterfaceProperties();

        public VMNetworkInterface() { }
    }
}
