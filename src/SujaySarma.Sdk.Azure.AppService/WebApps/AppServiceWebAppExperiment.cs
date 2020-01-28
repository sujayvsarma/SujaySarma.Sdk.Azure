using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Settings for polymorphic App Service apps (workaround settings -- as per Azure API docs)
    /// </summary>
    public class AppServiceWebAppExperiment
    {
        /// <summary>
        /// The ramp up rules for this experiment
        /// </summary>
        [JsonProperty("rampUpRules")]
        public List<AppServiceWebAppRampUpRule> Rules { get; set; } = new List<AppServiceWebAppRampUpRule>();
    }
}
