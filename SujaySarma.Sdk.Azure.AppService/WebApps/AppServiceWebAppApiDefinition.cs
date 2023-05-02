using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// If a web app is configured as an API app, the formal API definition for the app
    /// </summary>
    public class AppServiceWebAppApiDefinition
    {
        /// <summary>
        /// Url to the API definition
        /// </summary>
        [JsonProperty("url")]
        public string ApiDefinitionUrl { get; set; } = string.Empty;

    }
}
