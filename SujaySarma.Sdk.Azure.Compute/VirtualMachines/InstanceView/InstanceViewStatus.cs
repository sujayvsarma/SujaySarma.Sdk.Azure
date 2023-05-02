using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Status of the instance view
    /// </summary>
    public class InstanceViewStatus
    {
        /// <summary>
        /// Status code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Display status
        /// </summary>
        [JsonProperty("displayStatus")]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Status level (Error, Info or Warning)
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// Status message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp of the status
        /// </summary>
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; } = DateTime.MinValue;

        public InstanceViewStatus() { }
    }
}
