using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Custom auto-heal action for an App Service
    /// </summary>
    public class AppServiceWebAppAutoHealCustomAction
    {
        /// <summary>
        /// Executable (name or partial/absolute path) to be executed
        /// </summary>
        [JsonProperty("exe")]
        public string Executable { get; set; } = string.Empty;

        /// <summary>
        /// Parameters for the executable
        /// </summary>
        [JsonProperty("parameters")]
        public string? CommandLine { get; set; } = string.Empty;
    }
}
