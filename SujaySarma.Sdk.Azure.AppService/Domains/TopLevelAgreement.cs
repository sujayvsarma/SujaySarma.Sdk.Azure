using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// An agreement or tos for a TLD registration or transfer
    /// </summary>
    public class TopLevelAgreement
    {
        /// <summary>
        /// The key or code name for the agreement. This should be included in the consent parameter 
        /// during registration / transfer.
        /// </summary>
        [JsonProperty("agreementKey")]
        public string AgreementCode { get; set; } = string.Empty;

        /// <summary>
        /// Display name of the agreement
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Html content of the agreement
        /// </summary>
        [JsonProperty("content")]
        public string ContentHtml { get; set; } = string.Empty;

        /// <summary>
        /// URL to the full text of the original agreement
        /// </summary>
        [JsonProperty("url")]
        public string AgreementOriginalTextUri { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TopLevelAgreement() { }
    }
}
