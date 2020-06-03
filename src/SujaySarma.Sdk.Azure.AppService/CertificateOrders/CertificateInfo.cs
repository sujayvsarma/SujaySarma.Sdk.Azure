using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;

namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Information about a certificate
    /// </summary>
    public class CertificateInfo
    {
        /// <summary>
        /// Issued by
        /// </summary>
        [JsonProperty("issuer")]
        public string IssuedBy { get; set; } = string.Empty;

        /// <summary>
        /// Date/time the certificate should be considered valid after
        /// </summary>
        [JsonProperty("notAfter")]
        public DateTime ValidityEndsAt { get; set; } = default;

        /// <summary>
        /// Date/time the certificate should be considered valid before
        /// </summary>
        [JsonProperty("notBefore")]
        public DateTime ValidityStartsAt { get; set; } = default;

        /// <summary>
        /// Raw data of the certificate (same thing if you were to open it up in a text editor)
        /// </summary>
        [JsonProperty("rawData")]
        public string RawData { get; set; } = string.Empty;

        /// <summary>
        /// Serial number
        /// </summary>
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Certificate signing algorithm
        /// </summary>
        [JsonProperty("signatureAlgorithm"), JsonConverter(typeof(StringEnumConverter))]
        public CertificateSignatureAlgorithmNamesEnum Algorithm { get; set; } = CertificateSignatureAlgorithmNamesEnum.sha256RSA;

        /// <summary>
        /// Certificate subject name
        /// </summary>
        [JsonProperty("subject")]
        public string SubjectName { get; set; } = string.Empty;

        /// <summary>
        /// Thumbprint
        /// </summary>
        [JsonProperty("thumbprint")]
        public string Thumbprint { get; set; } = string.Empty;

        /// <summary>
        /// Certificate version number
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; } = 0;


        /// <summary>
        /// Default constructor
        /// </summary>
        public CertificateInfo() { }
    }
}
