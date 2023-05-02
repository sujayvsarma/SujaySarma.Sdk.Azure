using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Running view of a VM extension
    /// </summary>
    public class VMExtensionInstanceView
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
        /// Statuses
        /// </summary>
        [JsonProperty("statuses")]
        public List<InstanceViewStatus>? Status { get; set; } = null;

        /// <summary>
        /// Sub statuses
        /// </summary>
        [JsonProperty("substatuses")]
        public List<InstanceViewStatus>? SubStatus { get; set; } = null;

        public VMExtensionInstanceView() { }
    }
}
