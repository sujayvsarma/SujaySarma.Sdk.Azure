using Newtonsoft.Json;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Networking settings of a VM
    /// </summary>
    public class VMNetworkProfile
    {
        /// <summary>
        /// Network interfaces connected to this VM
        /// </summary>
        [JsonProperty("networkInterfaces")]
        public List<VMNetworkInterface>? Interfaces { get; set; }


        public VMNetworkProfile() { }
    }
}
