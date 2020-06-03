using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Consent for purchase
    /// </summary>
    public class DomainPurchaseConsent
    {
        /// <summary>
        /// Date and time agreed at, in UTC
        /// </summary>
        [JsonProperty("agreedAt")]
        public DateTime AgreedAt { get; set; }

        /// <summary>
        /// IP address of where the user sent this request
        /// </summary>
        [JsonProperty("agreedBy")]
        public string AgreedByIpAddress { get; set; } = "0.0.0.0";

        /// <summary>
        /// Terms agreed to
        /// </summary>
        [JsonProperty("agreementKeys")]
        public string[] AgreedTerms { get; set; } = new string[] { "DNPA", "DNTA" };

        /// <summary>
        /// Default constructor
        /// </summary>
        public DomainPurchaseConsent() { }
    }
}
