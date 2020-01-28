using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Auto-heal trigger based on the outgoing status code in a response
    /// </summary>
    public class AppServiceWebAppAutoHealStatusCodeTrigger
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

        /// <summary>
        /// The Http status code to trigger on
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; } = 500;

        /// <summary>
        /// The Http sub-status code to trigger on. Set to Zero or NULL to only use the 
        /// primary status code
        /// </summary>
        [JsonProperty("subStatus")]
        public int? SubStatus { get; set; } = null;

        /// <summary>
        /// To trigger on a specific Win32 error code, set it here
        /// </summary>
        [JsonProperty("win32Status")]
        public int? Win32ErrorCode { get; set; } = null;

    }
}
