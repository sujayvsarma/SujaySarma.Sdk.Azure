using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Properties for notification settings for an App Service web app
    /// </summary>
    public class AppServiceWebAppPushNotificationsProperties
    {
        /// <summary>
        /// JSON string containing a list of dynamic tags that will be evaluated 
        /// from user claims in the push registration endpoint
        /// </summary>
        [JsonProperty("dynamicTagsJson")]
        public string? DynamicTagsJson { get; set; }

        /// <summary>
        /// If the push notifications endpoint is enabled
        /// </summary>
        [JsonProperty("isPushEnabled")]
        public bool IsPushEndpointEnabled { get; set; }

        /// <summary>
        /// JSON string containing a list of tags that are whitelisted for 
        /// use by the push registration endpoint
        /// </summary>
        [JsonProperty("tagWhitelistJson")]
        public string? JsonTagsWhitelisted { get; set; }

        /// <summary>
        /// JSON string containing a list of tags that require user authentication 
        /// to be used in the push registration endpoint
        /// </summary>
        [JsonProperty("tagsRequiringAuth")]
        public string? TagsRequiringAuth { get; set; }

    }

}
