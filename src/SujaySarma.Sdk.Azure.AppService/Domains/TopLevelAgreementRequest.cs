using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Request to retrieve the agreements required for a 
    /// TLD registration or in-bound transfer
    /// </summary>
    public class TopLevelAgreementRequest
    {
        /// <summary>
        /// Set true when retrieving for a domain transfer, and false when retrieving 
        /// for a domain purchase.
        /// </summary>
        [JsonProperty("forTransfer")]
        public bool IncludeAgreementsForDomainTransfer { get; set; } = false;

        /// <summary>
        /// Set true based on the SupportsPrivacyRegistration property of a TopLevelDomain
        /// </summary>
        [JsonProperty("includePrivacy")]
        public bool IncludePrivacyAgreements { get; set; } = true;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TopLevelAgreementRequest() { }

    }
}
