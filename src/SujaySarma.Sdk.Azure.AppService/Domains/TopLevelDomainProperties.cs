using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Properties of a TopLevelDomain
    /// </summary>
    public class TopLevelDomainProperties
    {
        /// <summary>
        /// The tld (eg "com"). No '.' is prefixed.
        /// </summary>
        [JsonProperty("name")]
        public string Tld { get; set; } = string.Empty;

        /// <summary>
        /// True if the Tld registration supports privacy registration
        /// </summary>
        [JsonProperty("privacy")]
        public bool SupportsPrivacyRegistration { get; set; } = true;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TopLevelDomainProperties() { }
    }
}
