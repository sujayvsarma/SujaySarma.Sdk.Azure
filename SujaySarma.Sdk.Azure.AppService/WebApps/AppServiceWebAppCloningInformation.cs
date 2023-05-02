using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// During app creation if it was cloned from another app, this structure would contain information about the 
    /// source of the app clone. Otherwise, NULL.
    /// </summary>
    public class AppServiceWebAppCloningInformation
    {

        /// <summary>
        /// When specified, these settings override the App Settings in the source app. Otherwise, 
        /// the settings from the source app are retained in this app.
        /// </summary>
        [JsonProperty("appSettingsOverrides")]
        public object? AppSettingsOverrides { get; set; } = null;

        /// <summary>
        /// When true, custom hostnames from the source app are cloned to this app as well.
        /// </summary>
        [JsonProperty("cloneCustomHostNames")]
        public bool IsCustomHostnamesToBeCloned { get; set; } = false;

        /// <summary>
        /// When true, clones source control settings from the source app
        /// </summary>
        [JsonProperty("cloneSourceControl")]
        public bool IsSourceControlSettingsToBeCloned { get; set; } = false;

        /// <summary>
        /// When true, configures load balancing settings from the source app
        /// </summary>
        [JsonProperty("configureLoadBalancing")]
        public bool IsLoadBalancingSettingsToBeConfigured { get; set; } = false;

        /// <summary>
        /// A correlation Id to tie this cloning operation with other related operations
        /// </summary>
        [JsonProperty("correlationId")]
        public string? CorrelationId { get; set; } = null;

        /// <summary>
        /// Resource Uri to the App Service environment (???)
        /// </summary>
        [JsonProperty("hostingEnvironment")]
        public string? HostingEnvironmentUri { get; set; } = null;

        /// <summary>
        /// When true, and the target app exists, it is overwritten with the configuration in this structure. 
        /// Otherwise, operations would fail
        /// </summary>
        [JsonProperty("overwrite")]
        public bool IsTargetAppToBeOverwritten { get; set; } = false;

        /// <summary>
        /// Resource Uri to the source app. If the source app uses Slots and a specific slot is being cloned, the 
        /// Uri will point to that slot
        /// </summary>
        [JsonProperty("sourceWebAppId")]
        public string SourceAppResourceId { get; set; } = string.Empty;

        /// <summary>
        /// The region name where the source app is located (eg: "West US")
        /// </summary>
        [JsonProperty("sourceWebAppLocation")]
        public string SourceAppGeoLocationName { get; set; } = string.Empty;

        /// <summary>
        /// Resource Id of the traffic manager profile to use (if required/present)
        /// </summary>
        [JsonProperty("trafficManagerProfileId")]
        public string? TrafficManagerProfileResourceId { get; set; } = null;

        /// <summary>
        /// If specified, a new traffic manager profile with this name is created. Leave NULL and set 
        /// the TrafficManagerProfileResourceId property to use that profile
        /// </summary>
        [JsonProperty("trafficManagerProfileName")]
        public string? TrafficManagerTargetProfileName { get; set; } = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceWebAppCloningInformation() { }
    }
}
