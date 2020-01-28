using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// A rule for auto-healing an Azure App Service Web App
    /// </summary>
    public class AppServiceWebAppAutoHealRule
    {

        /// <summary>
        /// Actions to be executed when a rule is triggered.
        /// </summary>
        [JsonProperty("actions")]
        public List<AppServiceWebAppAutoHealAction> Actions { get; set; } = new List<AppServiceWebAppAutoHealAction>();

        /// <summary>
        /// Conditions that describe when to execute auto-heal actions
        /// </summary>
        [JsonProperty("triggers")]
        public List<AppServiceWebAppAutoHealTrigger> Triggers { get; set; } = new List<AppServiceWebAppAutoHealTrigger>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceWebAppAutoHealRule() { }
    }
}
