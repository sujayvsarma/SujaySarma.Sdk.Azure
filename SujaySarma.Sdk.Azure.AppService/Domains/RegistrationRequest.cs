using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Request for registration of a domain
    /// </summary>
    public class RegistrationRequest
    {

        /// <summary>
        /// Location of the domain. Will always be global.
        /// </summary>
        [JsonProperty("location")]
        public string Location => "global";

        /// <summary>
        /// Properties of the request
        /// </summary>
        [JsonProperty("properties")]
        public RegistrationRequestProperties Properties { get; set; }

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, string>? Tags { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegistrationRequest() => Properties = new RegistrationRequestProperties();

        /// <summary>
        /// Create a request
        /// </summary>
        /// <param name="autoRenew">Flag to auto-renew every year</param>
        /// <param name="contact">Contact information to use for admin/billing/tech/registrant contact for the domain</param>
        /// <param name="consentIpAddress">IP address of user creating this request</param>
        /// <param name="consentAgreementKeys">Keys of terms that have been agreed to</param>
        public RegistrationRequest(bool autoRenew, DomainRegistrationContact contact, string consentIpAddress, string[] consentAgreementKeys)
        {
            Properties = new RegistrationRequestProperties()
            {
                AdministrativeContact = contact,
                BillingContact = contact,
                RegistrantContact = contact,
                TechnicalContact = contact,
                Consent = new DomainPurchaseConsent()
                {
                    AgreedAt = DateTime.UtcNow,
                    AgreedByIpAddress = consentIpAddress,
                    AgreedTerms = consentAgreementKeys
                },
                TypeOfDns = DomainRegistrationDnsTypeEnum.AzureDns,
                RenewAutomatically = autoRenew
            };
        }
    }

}
