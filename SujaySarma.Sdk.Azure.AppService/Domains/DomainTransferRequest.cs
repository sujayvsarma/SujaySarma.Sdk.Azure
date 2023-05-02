using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Request to transfer a domain
    /// </summary>
    public class DomainTransferRequest
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
        public DomainTransferRequestProperties Properties { get; set; }

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, string>? Tags { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DomainTransferRequest() => Properties = new DomainTransferRequestProperties();

        /// <summary>
        /// Create a request
        /// </summary>
        /// <param name="authCode">Authorization code for transfer</param>
        /// <param name="autoRenew">Flag to auto-renew every year</param>
        /// <param name="contact">Contact information to use for admin/billing/tech/registrant contact for the domain</param>
        /// <param name="consentIpAddress">IP address of user creating this request</param>
        /// <param name="consentAgreementKeys">Keys of terms that have been agreed to</param>
        public DomainTransferRequest(string authCode, bool autoRenew, DomainRegistrationContact contact, string consentIpAddress, string[] consentAgreementKeys)
        {
            Properties = new DomainTransferRequestProperties()
            {
                TransferAuthorizationCode = authCode,
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
