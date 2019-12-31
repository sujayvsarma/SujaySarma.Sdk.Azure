using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Instance views of all registered handlers for the VM Agent extension
    /// </summary>
    public class VMExtensionHandlerInstanceView
    {
        /// <summary>
        /// Type of extension
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Version of the type handler
        /// </summary>
        [JsonProperty("typeHandlerVersion")]
        public string TypeHandlerVersion { get; set; } = string.Empty;

        /// <summary>
        /// Status information as the instance spun up
        /// </summary>
        [JsonProperty("status")]
        public List<InstanceViewStatus>? Status { get; set; }


        public VMExtensionHandlerInstanceView() { }
    }
}
