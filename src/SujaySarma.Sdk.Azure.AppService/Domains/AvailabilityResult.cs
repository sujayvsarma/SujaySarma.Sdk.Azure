using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Result of an availability check
    /// </summary>
    public class AvailabilityResult
    {

        /// <summary>
        /// The domain name tested
        /// </summary>
        [JsonProperty("name")]
        public string DomainName { get; set; } = string.Empty;

        /// <summary>
        /// If the domain name is available for registration
        /// </summary>
        [JsonProperty("available")]
        public bool IsAvailable { get; set; } = false;

        /// <summary>
        /// Type of availability
        /// </summary>
        [JsonProperty("domainType"), JsonConverter(typeof(StringEnumConverter))]
        public AvailableResultDomainTypeEnum Type { get; set; }


        public AvailabilityResult() { }
    }
}
