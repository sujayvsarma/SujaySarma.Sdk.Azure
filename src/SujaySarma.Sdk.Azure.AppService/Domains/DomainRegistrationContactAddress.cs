
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Address of the contact for a domain registration
    /// </summary>
    public class DomainRegistrationContactAddress
    {
        /// <summary>
        /// Address line 1
        /// </summary>
        [JsonProperty("address1")]
        public string Address1 { get; set; } = string.Empty;

        /// <summary>
        /// Address line 2 (optional)
        /// </summary>
        [JsonProperty("address2")]
        public string? Address2 { get; set; } = null;

        /// <summary>
        /// City name
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// State or province name
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Country name
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Postal code
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DomainRegistrationContactAddress() { }
    }

}
