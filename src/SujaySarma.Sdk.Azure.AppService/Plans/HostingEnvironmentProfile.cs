using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Plans
{
    /// <summary>
    /// Profile of the hosting environment for an ASP
    /// </summary>
    public class HostingEnvironmentProfile
    {
        /// <summary>
        /// Resource Id of the app service environment
        /// </summary>
        [JsonProperty("id")]
        public string AppServiceEnvironmentId { get; set; } = string.Empty;

        /// <summary>
        /// Name of the app service environment
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Resource type of the app service environment
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
