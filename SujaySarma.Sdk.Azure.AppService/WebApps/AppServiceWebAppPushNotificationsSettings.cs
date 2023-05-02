using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Push notification settings for an Azure App Service
    /// </summary>
    public class AppServiceWebAppPushNotificationsSettings
    {

        /// <summary>
        /// The ResourceId URI for the object
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; } = string.Empty;

        /// <summary>
        /// Name of the object
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of object
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Kind of resource
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; } = string.Empty;


        /// <summary>
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public AppServiceWebAppPushNotificationsProperties Properties { get; set; } = new AppServiceWebAppPushNotificationsProperties();

    }

}
