using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Properties of the App Service Domain
    /// </summary>
    public class AppServiceDomainProperties
    {

        /// <summary>
        /// Status of registration
        /// </summary>
        [JsonProperty("registrationStatus"), JsonConverter(typeof(StringEnumConverter))]
        public DomainRegistrationStatusEnum RegistrationStatus { get; set; } = DomainRegistrationStatusEnum.Active;

        /// <summary>
        /// Provisioning state
        /// </summary>
        [JsonProperty("provisioningState"), JsonConverter(typeof(StringEnumConverter))]
        public ProvisioningStatusEnum ProvisioningState { get; set; } = ProvisioningStatusEnum.Default;

        /// <summary>
        /// List of nameservers attached to this domain. Will be NULL if not managed by Azure.
        /// </summary>
        [JsonProperty("nameServers")]
        public List<string>? Nameservers { get; set; } = new List<string>();

        /// <summary>
        /// If the domain has a privacy registration. Should always be true.
        /// </summary>
        [JsonProperty("privacy")]
        public bool HasPrivacyRegistration { get; set; } = true;

        /// <summary>
        /// Date and time when the domain was registered. If the domain was transferred into Azure, then 
        /// this would be the date/time when it was provisioned in Azure.
        /// </summary>
        [JsonProperty("createdTime")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Date and time when the domain registration will expire.
        /// </summary>
        [JsonProperty("expirationTime")]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// If the domain will renew automatically at the end of the current registration year
        /// </summary>
        [JsonProperty("autoRenew")]
        public bool AutomaticallyRenewsAnnually { get; set; } = true;

        /// <summary>
        /// If the domain has been configured correctly for DNS records management within Azure.
        /// </summary>
        [JsonProperty("readyForDnsRecordManagement")]
        public bool IsReadyForDnsRecordsManagement { get; set; } = true;

        /// <summary>
        /// List of managed host names attached to this domain
        /// </summary>
        [JsonProperty("managedHostNames")]
        public List<string>? ManagedHostNames { get; set; } = new List<string>();

        /// <summary>
        /// List of reasons why the domain cannot be renewed right now.
        /// </summary>
        [JsonProperty("domainNotRenewableReasons")]
        public List<string>? ReasonsWhyDomainCannotBeRenewedRightNow { get; set; } = new List<string>();

        /// <summary>
        /// Type of DNS
        /// </summary>
        [JsonProperty("dnsType")]
        public DomainRegistrationDnsTypeEnum DnsType { get; set; } = DomainRegistrationDnsTypeEnum.AzureDns;

        /// <summary>
        /// Resource Uri to the Azure DNS Zone that manages this domain
        /// </summary>
        [JsonProperty("dnsZoneId")]
        public string AzureDnsZoneId { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceDomainProperties() { }
    }
}
