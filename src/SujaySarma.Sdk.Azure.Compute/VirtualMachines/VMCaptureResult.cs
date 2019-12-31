using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Results of a VM capture request. 
    /// This structure is created internally and used.
    /// </summary>
    internal class VMCaptureResult
    {
        /// <summary>
        /// Schema URI
        /// </summary>
        [JsonProperty("$schema")]
        public string Schema { get; set; } = string.Empty;

        /// <summary>
        /// A version number of the contents
        /// </summary>
        [JsonProperty("contentVersion")]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Resource Id of the captured TEMPLATE
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; } = string.Empty;

        /// <summary>
        /// VM resource template parameters
        /// </summary>
        [JsonProperty("parameters")]
        public object Parameters { get; set; } = new object();

        /// <summary>
        /// Resources in the object
        /// </summary>
        [JsonProperty("resources")]
        public object[] Resources { get; set; } = Array.Empty<object>();


        public VMCaptureResult() { }
    }
}
