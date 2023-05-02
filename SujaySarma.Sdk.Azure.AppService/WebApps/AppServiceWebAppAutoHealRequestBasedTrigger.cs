using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Auto-heal trigger based on type/volume/nature of incoming requests
    /// </summary>
    public class AppServiceWebAppAutoHealRequestBasedTrigger
    {
        /// <summary>
        /// Auto-heal will be triggered when the total number of requests in a time period 
        /// crosses this threshold of incoming requests (For example, to guard against potential 
        /// DDoS attacks)
        /// </summary>
        [JsonProperty("count")]
        public int TotalRequestCount { get; set; } = 0;

        /// <summary>
        /// Time interval (eg: "1H" for 1 hour and so on) to accumalate request counts
        /// </summary>
        [JsonProperty("timeInterval")]
        public string TimeInterval { get; set; } = string.Empty;
    }
}
