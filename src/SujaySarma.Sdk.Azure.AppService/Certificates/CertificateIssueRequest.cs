using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.Certificates
{
    /// <summary>
    /// Request to issue a certificate
    /// </summary>
    public class CertificateIssueRequest
    {

        /// <summary>
        /// Kind of resource, always "certificates"
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; } = "certificates";

        /// <summary>
        /// Location to place the certificate
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; } = "global";

        /// <summary>
        /// Properties of the request
        /// </summary>
        [JsonProperty("properties")]
        public CertificateIssueRequestProperties Properties { get; set; } = new CertificateIssueRequestProperties();

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, string>? Tags { get; set; } = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CertificateIssueRequest() { }
    }
}
