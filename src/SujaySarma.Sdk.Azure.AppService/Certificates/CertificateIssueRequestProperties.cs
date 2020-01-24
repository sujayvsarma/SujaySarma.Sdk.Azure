using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Certificates
{
    /// <summary>
    /// Properties of a certificate issue request
    /// </summary>
    public class CertificateIssueRequestProperties
    {

        /// <summary>
        /// Resource Uri to the KeyVault where the private key is stored
        /// </summary>
        [JsonProperty("keyVaultId")]
        public string KeyVaultId { get; set; } = string.Empty;

        /// <summary>
        /// Name of the secret in the KeyVault
        /// </summary>
        [JsonProperty("keyVaultSecretName")]
        public string KeyVaultSecretName { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CertificateIssueRequestProperties() { }
    }
}
