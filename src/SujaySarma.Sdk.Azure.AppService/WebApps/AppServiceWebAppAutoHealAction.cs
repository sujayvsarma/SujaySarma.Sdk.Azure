using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Action to be executed when an Azure App Service auto-heal rule is triggered
    /// </summary>
    public class AppServiceWebAppAutoHealAction
    {
        /// <summary>
        /// Type of action to be taken
        /// </summary>
        [JsonProperty("actionType"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceWebAppAutoHealActionType ActionType { get; set; } = AppServiceWebAppAutoHealActionType.Default;

        /// <summary>
        /// The minimum amount of time the process should execute before the auto-heal can be triggered.
        /// </summary>
        [JsonProperty("minProcessExecutionTime")]
        public string MinimumProcessExecutionTimeBeforeRun { get; set; } = string.Empty;

        /// <summary>
        /// If we are running a custom action, then its definition goes here
        /// </summary>
        [JsonProperty("customAction")]
        public AppServiceWebAppAutoHealCustomAction? CustomAction { get; set; }
    }
}
