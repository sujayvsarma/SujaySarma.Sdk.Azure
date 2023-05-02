using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// A ramp up rule for an App Service experiment
    /// </summary>
    public class AppServiceWebAppRampUpRule
    {
        /// <summary>
        /// Name of the host to redirect to if the rule decides so (eg: "contoso-staging.azurewebsites.net")
        /// </summary>
        [JsonProperty("actionHostName")]
        public string RedirectHostName { get; set; } = string.Empty;

        /// <summary>
        /// Custom decision algorithm can be provided in TiPCallback site extension 
        /// which URL can be specified. See TiPCallback site extension for the scaffold and contracts
        /// </summary>
        [JsonProperty("changeDecisionCallbackUrl")]
        public string? TiPCallbackDecisionUrl { get; set; }

        /// <summary>
        /// Interval in minutes to evaluate the ReroutePercentage value
        /// </summary>
        [JsonProperty("changeIntervalInMinutes")]
        public int ChangeIntervalMinutes { get; set; } = 30;

        /// <summary>
        /// In auto ramp up scenario this is the step to add/remove from ReroutePercentage until 
        /// it reaches MinReroutePercentage or MaxReroutePercentage. Site metrics are checked every 
        /// N minutes specified in ChangeIntervalInMinutes. Custom decision algorithm can be provided 
        /// in TiPCallback site extension which URL can be specified in ChangeDecisionCallbackUrl
        /// </summary>
        [JsonProperty("changeStep")]
        public double ChangeStep { get; set; } = 0;

        /// <summary>
        /// Upper boundary below which ReroutePercentage will stay
        /// </summary>
        [JsonProperty("maxReroutePercentage")]
        public double MaxReroutePercentage { get; set; } = 0;

        /// <summary>
        /// Lower boundary above which ReroutePercentage will stay
        /// </summary>
        [JsonProperty("minReroutePercentage")]
        public double MinReroutePercentage { get; set; } = 0;

        /// <summary>
        /// Name of the rule
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Percentage of the traffic which will be redirected to RedirectHostName
        /// </summary>
        [JsonProperty("reroutePercentage")]
        public double ReroutePercentage { get; set; } = 0;

    }
}
