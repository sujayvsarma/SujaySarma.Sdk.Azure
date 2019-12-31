using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Instance of the VM Agent running on a VM
    /// </summary>
    public class VMAgentInstanceView
    {

        /// <summary>
        /// Full version number of the VM Agent
        /// </summary>
        [JsonProperty("vmAgentVersion")]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Status of various things
        /// </summary>
        [JsonProperty("statuses")]
        public List<InstanceViewStatus>? Status { get; set; }

        /// <summary>
        /// Instance views of all registered handlers for the VM Agent extension
        /// </summary>
        [JsonProperty("extensionHandlers")]
        public List<VMExtensionHandlerInstanceView>? ExtensionHandlers { get; set; }


        public VMAgentInstanceView() { }
    }
}
