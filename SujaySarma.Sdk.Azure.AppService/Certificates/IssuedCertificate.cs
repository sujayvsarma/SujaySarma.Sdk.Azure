using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.Certificates
{
    /// <summary>
    /// A certificate that has been issued
    /// </summary>
    public class IssuedCertificate : AzureObjectBase
    {

        /// <summary>
        /// Properties of the certificate
        /// </summary>
        [JsonProperty("properties")]
        public IssuedCertificateProperties Properties { get; set; } = new IssuedCertificateProperties();


        /// <summary>
        /// Default constructor
        /// </summary>
        public IssuedCertificate() { }
    }
}
