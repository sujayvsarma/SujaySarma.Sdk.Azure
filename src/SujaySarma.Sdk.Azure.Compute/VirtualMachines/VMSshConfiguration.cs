using Newtonsoft.Json;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// SSH Configuration for a VM
    /// </summary>
    public class VMSshConfiguration
    {
        [JsonProperty("publicKeys")]
        public List<VMSshPublicKey> Keys { get; set; } = new List<VMSshPublicKey>();

        public VMSshConfiguration() { }
    }
}
