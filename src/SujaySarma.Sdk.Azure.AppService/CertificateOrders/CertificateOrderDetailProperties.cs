using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Properties of the order
    /// </summary>
    public class CertificateOrderDetailProperties
    {

        /// <summary>
        /// Reasons why the certificate may not be immediately renewable (for example it does not 
        /// expire any time soon)
        /// </summary>
        [JsonProperty("appServiceCertificateNotRenewableReasons")]
        public List<string>? ReasonsWhyNotRenewableImmediately { get; set; } = new List<string>();

        /// <summary>
        /// If set, automatically renews at the end of the current validity period
        /// </summary>
        [JsonProperty("autoRenew")]
        public bool AutoRenew { get; set; } = false;

        /// <summary>
        /// Name of the domain
        /// </summary>
        [JsonProperty("distinguishedName")]
        public string AppServiceDomainName { get; set; } = string.Empty;

        /// <summary>
        /// Certificate key size
        /// </summary>
        /// <remarks>
        ///     We want the numeric value here, so DONT use the SEC for Json
        /// </remarks>
        [JsonProperty("keySize")]
        public CertificateKeySizesEnum KeySize { get; set; } = CertificateKeySizesEnum.Default;

        /// <summary>
        /// Type of certificate
        /// </summary>
        [JsonProperty("productType"), JsonConverter(typeof(StringEnumConverter))]
        public CertificateTypesEnum Type { get; set; } = CertificateTypesEnum.StandardDomainValidatedSsl;

        /// <summary>
        /// Validity in years
        /// </summary>
        [JsonProperty("validityInYears")]
        public CertificateValidityPeriod Validity { get; set; } = CertificateValidityPeriod.Default;

        /// <summary>
        /// If a previous CSR was generated, then that TEXT can be provided here.
        /// </summary>
        [JsonProperty("csr")]
        public string? LastGeneratedCertificateRequestText { get; set; } = null;

        /// <summary>
        /// If the connected domain needs to be verified, the token for the verification
        /// </summary>
        [JsonProperty("domainVerificationToken")]
        public string? DomainVerificationToken { get; set; } = null;

        /// <summary>
        /// Date/time when the certificate will expire
        /// </summary>
        [JsonProperty("expirationTime")]
        public DateTime ExpiresAt { get; set; } = default;

        /// <summary>
        /// True if the certificate's private key is stored elsewhere (as in the case of .CER files).
        /// </summary>
        [JsonProperty("isPrivateKeyExternal")]
        public bool IsPrivateKeyStoredExternally { get; set; } = false;

        /// <summary>
        /// Date/time when this certificate was last issued
        /// </summary>
        [JsonProperty("lastCertificateIssuanceTime")]
        public DateTime LastIssuedAt { get; set; } = default;

        /// <summary>
        /// Date/time when the certificate is scheduled for auto-renewal. NULL if we are not going to 
        /// auto-renew it.
        /// </summary>
        [JsonProperty("nextAutoRenewalTimeStamp")]
        public DateTime? NextAutoRenewalAt { get; set; } = default;

        /// <summary>
        /// Status of Provisioning
        /// </summary>
        [JsonProperty("provisioningState"), JsonConverter(typeof(StringEnumConverter))]
        public ProvisioningStatusEnum ProvisioningStatus { get; set; } = ProvisioningStatusEnum.Default;

        /// <summary>
        /// Certificate serial number
        /// </summary>
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public CertificateOrderStatus OrderStatus { get; set; } = CertificateOrderStatus.Default;

        /// <summary>
        /// Intermediate certificate
        /// </summary>
        [JsonProperty("intermediate")]
        public CertificateInfo? IntermediateCertificate { get; set; } = null;

        /// <summary>
        /// Root certificate
        /// </summary>
        [JsonProperty("root")]
        public CertificateInfo? RootCertificate { get; set; } = null;

        /// <summary>
        /// Signed certificate
        /// </summary>
        [JsonProperty("signedCertificate")]
        public CertificateInfo? SignedCertificate { get; set; } = null;


        /*
         * There is another property called "certificates" here, but it is not documented clearly enough 
         * and it is an optional parameter, so we are not going to use it here!
         */


        /// <summary>
        /// Default constructor
        /// </summary>
        public CertificateOrderDetailProperties() { }
    }
}
