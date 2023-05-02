using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Status of a swap operation between two slots of an App Service app
    /// </summary>
    public class AppServiceWebAppSlotSwapStatus
    {
        /// <summary>
        /// Name of the slot switched to in the last slot swap operation
        /// </summary>
        [JsonProperty("destinationSlotName")]
        public string TargetSlotName { get; set; } = string.Empty;

        /// <summary>
        /// Name of the slot switched from in the last slot swap operation
        /// </summary>
        [JsonProperty("sourceSlotName")]
        public string SourceSlotName { get; set; } = string.Empty;

        /// <summary>
        /// Date/time of swap, in UTC
        /// </summary>
        [JsonProperty("timestampUtc")]
        public DateTime Timestamp { get; set; }
    }
}
