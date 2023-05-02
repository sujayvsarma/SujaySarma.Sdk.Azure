using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Auto-heal trigger based on duration of execution of a request
    /// </summary>
    public class AppServiceWebAppAutoHealSlowRequestTrigger
    {
        /// <summary>
        /// Auto-heal will be triggered when the total number of requests in a time period 
        /// crosses this threshold of incoming requests (For example, to guard against potential 
        /// DDoS attacks)
        /// </summary>
        [JsonProperty("count")]
        public int TotalRequestCount { get; set; } = 0;

        /// <summary>
        /// Time interval (eg: "01:00:00" for 1 hour and so on) to accumalate request counts
        /// </summary>
        [JsonProperty("timeInterval")]
        public string TimeInterval { get; set; } = string.Empty;

        /// <summary>
        /// Delay in response (eg: "00:00:45" for 45 seconds) that the response took
        /// </summary>
        [JsonProperty("timeTaken")]
        public string ResponseDelay { get; set; } = string.Empty;
    }
}
