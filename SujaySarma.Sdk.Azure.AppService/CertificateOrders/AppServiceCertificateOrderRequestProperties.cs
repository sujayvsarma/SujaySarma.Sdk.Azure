using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Properties of a certificate order
    /// </summary>
    public class AppServiceCertificateOrderRequestProperties
    {

        /// <summary>
        /// If set, automatically renews at the end of the current validity period
        /// </summary>
        [JsonProperty("autoRenew")]
        public bool AutoRenew { get; set; } = false;

        /// <summary>
        /// Name of the domain
        /// </summary>
        [JsonProperty("distinguishedName")]
        public string? AppServiceDomainName { get; set; } = null;

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

        /*
         * There is another property called "certificates" here, but it is not documented clearly enough 
         * and it is an optional parameter, so we are not going to use it here!
         */

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceCertificateOrderRequestProperties() { }
    }
}
