using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Request for domain name recommendations
    /// </summary>
    public class DomainNameRecommendationRqeuest
    {
        /// <summary>
        /// Comma-seperated list of keywords
        /// </summary>
        [JsonProperty("keywords")]
        public string Keywords { get; set; } = string.Empty;

        /// <summary>
        /// Maximum number of domain recommendations to return
        /// </summary>
        [JsonProperty("maxDomainRecommendations")]
        public int MaximumRecommendations { get; set; } = 5;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DomainNameRecommendationRqeuest() { }
    }
}
