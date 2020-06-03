using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.Certificates
{
    /// <summary>
    /// Properties of an issued certificate
    /// </summary>
    public class IssuedCertificateProperties
    {
        /// <summary>
        /// Resource Uri to the KeyVault where the certificate is stored
        /// </summary>
        [JsonProperty("keyVaultId")]
        public string KeyVaultId { get; set; } = string.Empty;

        /// <summary>
        /// Name of the secret to the KeyVault where the certificate is stored
        /// </summary>
        [JsonProperty("keyVaultSecretName")]
        public string KeyVaultSecretName { get; set; } = string.Empty;

        /// <summary>
        /// State of provisioning the KeyVault secret
        /// </summary>
        [JsonProperty("provisioningState"), JsonConverter(typeof(StringEnumConverter))]
        public KeyVaultSecretStatusEnum KeyVaultSecretStatus { get; set; } = KeyVaultSecretStatusEnum.Unknown;

        /// <summary>
        /// Default constructor
        /// </summary>
        public IssuedCertificateProperties() { }
    }
}
