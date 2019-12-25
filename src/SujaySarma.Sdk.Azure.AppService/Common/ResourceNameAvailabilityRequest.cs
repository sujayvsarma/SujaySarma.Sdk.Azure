
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Common
{
    /// <summary>
    /// Request for checking for a resource name
    /// </summary>
    public class ResourceNameAvailabilityRequest
    {

        /// <summary>
        /// Name provided is a fully qualified domain name
        /// </summary>
        [JsonProperty("isFqdn")]
        public bool IsFullyQualifiedDomainName { get; set; } = false;

        /// <summary>
        /// The name to check for
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = "Site";


        public ResourceNameAvailabilityRequest() { }
    }
}
