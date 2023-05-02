using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Mapping of HTTP handlers for an Azure App Service app
    /// </summary>
    public class AppServiceWebAppHandlerMapping
    {
        /// <summary>
        /// Command-line arguments to pass on to the script processor
        /// </summary>
        [JsonProperty("arguments")]
        public string? CommandLine { get; set; } = null;

        /// <summary>
        /// Requests with this extension will be handled using the specified FastCgi application
        /// </summary>
        [JsonProperty("extension")]
        public string? Extension { get; set; } = null;

        /// <summary>
        /// Absolute path to the FastCGI script processor that would handle requests
        /// </summary>
        [JsonProperty("scriptProcessor")]
        public string FastCgiScriptProcessor { get; set; } = string.Empty;

    }
}
