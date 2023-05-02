using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Extensions on a running VM
    /// </summary>
    public class VMInstanceViewExtension
    {
        /// <summary>
        /// Name of extension
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

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
        [JsonProperty("statuses")]
        public List<InstanceViewStatus>? Status { get; set; }

        /// <summary>
        /// Sub-status information as the instance spun up
        /// </summary>
        [JsonProperty("substatuses")]
        public List<InstanceViewStatus>? Substatus { get; set; }

        public VMInstanceViewExtension() { }
    }
}
