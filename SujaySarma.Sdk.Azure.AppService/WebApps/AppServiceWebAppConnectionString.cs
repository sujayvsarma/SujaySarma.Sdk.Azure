using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// A connection string configured for an Azure App Service Web App
    /// </summary>
    public class AppServiceWebAppConnectionString
    {
        /// <summary>
        /// Type of connection string
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceWebAppConnectionStringType Type { get; set; }

        /// <summary>
        /// Name of the connection string
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The connection string
        /// </summary>
        [JsonProperty("connectionString")]
        public string Value { get; set; } = string.Empty;
    }
}
