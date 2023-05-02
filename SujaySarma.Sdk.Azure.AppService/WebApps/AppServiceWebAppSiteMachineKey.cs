using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Machine key for an App Service
    /// </summary>
    public class AppServiceWebAppSiteMachineKey
    {

        /// <summary>
        /// Decryption algorithm
        /// </summary>
        [JsonProperty("decryption")]
        public string DecryptionAlgorithm { get; set; } = string.Empty;

        /// <summary>
        /// Decryption key
        /// </summary>
        [JsonProperty("decryptionKey")]
        public string DecryptionKey { get; set; } = string.Empty;

        /// <summary>
        /// Validation algorithm
        /// </summary>
        [JsonProperty("validation")]
        public string ValidationAlgorithm { get; set; } = string.Empty;

        /// <summary>
        /// Validation key
        /// </summary>
        [JsonProperty("validationKey")]
        public string ValidationKey { get; set; } = string.Empty;

    }
}
