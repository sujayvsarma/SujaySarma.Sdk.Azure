using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// An email sent out for a certificate order
    /// </summary>
    public class CertificateEmail
    {

        /// <summary>
        /// Resource Id
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; } = string.Empty;

        /// <summary>
        /// Kind of entity ("email")
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; } = string.Empty;

        /// <summary>
        /// Name of email
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type ("certificateEmail")
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public CertificateEmailProperties Properties { get; set; } = new CertificateEmailProperties();

    }
}
