using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Describes a VM
    /// </summary>
    public class VirtualMachine : AzureObjectBase
    {

        /// <summary>
        /// Identity of the VM if configured
        /// </summary>
        [JsonProperty("identity")]
        public ResourceIdentity? Identity { get; set; } = null;

        /// <summary>
        /// The marketplace image used to create this VM. Known as "plan" in the API.
        /// </summary>
        [JsonProperty("plan")]
        public ResourcePlan MarketplaceImageSource { get; set; } = new ResourcePlan();

        /// <summary>
        /// Properties of the VM
        /// </summary>
        [JsonProperty("properties")]
        public VMProperties Properties { get; set; } = new VMProperties();

        /// <summary>
        /// Extensions installed on the VM
        /// </summary>
        [JsonProperty("resources")]
        public List<VMExtension>? ExtensionsInstalled { get; set; }

        /// <summary>
        /// Zones available to the VM
        /// </summary>
        [JsonProperty("zones")]
        public List<string>? Zones { get; set; }

        public VirtualMachine() { }
    }
}
