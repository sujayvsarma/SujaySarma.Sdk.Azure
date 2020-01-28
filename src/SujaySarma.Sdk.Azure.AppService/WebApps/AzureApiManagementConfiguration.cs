using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Azure API Management service settings linked to a web app
    /// </summary>
    public class AzureApiManagementConfiguration
    {
        /// <summary>
        /// Azure API Management API identifier
        /// </summary>
        [JsonProperty("id")]
        public string ApiManagementIdentifier { get; set; } = string.Empty;

    }
}
