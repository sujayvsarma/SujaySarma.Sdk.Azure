
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Properties of the request
    /// </summary>
    public class RegistrationRequestProperties
    {
        /// <summary>
        /// Renew automatically at the end of the period. Azure Domains are registered for 
        /// one year at a time, so renewal will occur one year from now.
        /// </summary>
        [JsonProperty("autoRenew")]
        public bool RenewAutomatically { get; set; } = true;

        /// <summary>
        /// If privacy registration is required. Should always be True. Azure will ignore it 
        /// for domains that don't support a privacy registration (like .IN domains)
        /// </summary>
        [JsonProperty("privacy")]
        public bool PrivacyRegistrationRequired => true;

        /// <summary>
        /// Consent information
        /// </summary>
        [JsonProperty("consent")]
        public DomainPurchaseConsent Consent { get; set; } = new DomainPurchaseConsent();

        /// <summary>
        /// Administrative contact
        /// </summary>
        [JsonProperty("contactAdmin")]
        public DomainRegistrationContact AdministrativeContact { get; set; } = new DomainRegistrationContact();

        /// <summary>
        /// Billing contact
        /// </summary>
        [JsonProperty("contactBilling")]
        public DomainRegistrationContact BillingContact { get; set; } = new DomainRegistrationContact();

        /// <summary>
        /// Registration contact
        /// </summary>
        [JsonProperty("contactRegistrant")]
        public DomainRegistrationContact RegistrantContact { get; set; } = new DomainRegistrationContact();

        /// <summary>
        /// Technical contact
        /// </summary>
        [JsonProperty("contactTech")]
        public DomainRegistrationContact TechnicalContact { get; set; } = new DomainRegistrationContact();

        /// <summary>
        /// Type of DNS server that will be used
        /// </summary>
        [JsonProperty("dnsType"), JsonConverter(typeof(StringEnumConverter))]
        public DomainRegistrationDnsTypeEnum TypeOfDns { get; set; } = DomainRegistrationDnsTypeEnum.AzureDns;

        /// <summary>
        /// If there is an existing Azure DNS Zone for this domain, then the resource Id of that zone.
        /// </summary>
        [JsonProperty("dnsZoneId")]
        public string? AzureDnsZoneId { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegistrationRequestProperties() { }
    }

}
