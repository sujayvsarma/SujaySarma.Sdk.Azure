using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Properties of a certificate email
    /// </summary>
    public class CertificateEmailProperties
    {
        /// <summary>
        /// Email address the mail was sent to
        /// </summary>
        [JsonProperty("emailId")]
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Date/time when the email was sent
        /// </summary>
        [JsonProperty("timeStamp")]
        public DateTime Timestamp { get; set; }
    }
}
