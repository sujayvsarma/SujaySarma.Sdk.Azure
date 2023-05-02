using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// A condition that describes when to execute auto-heal actions for an App Service
    /// </summary>
    public class AppServiceWebAppAutoHealTrigger
    {
        /// <summary>
        /// Trigger on an amount of private bytes consumed
        /// </summary>
        [JsonProperty("privateBytesInKB")]
        public int? OnPrivateBytesConsumed { get; set; }

        /// <summary>
        /// Trigger based on type/volume/nature of incoming requests
        /// </summary>
        [JsonProperty("requests")]
        public AppServiceWebAppAutoHealRequestBasedTrigger? OnRequest { get; set; }

        /// <summary>
        /// Trigger based on request execution time
        /// </summary>
        [JsonProperty("slowRequests")]
        public AppServiceWebAppAutoHealSlowRequestTrigger? OnSlowRequest { get; set; }

        /// <summary>
        /// Trigger based on the response status code
        /// </summary>
        [JsonProperty("slowRequests")]
        public List<AppServiceWebAppAutoHealStatusCodeTrigger>? OnResponseStatusCodes { get; set; } = new List<AppServiceWebAppAutoHealStatusCodeTrigger>();
    }
}
