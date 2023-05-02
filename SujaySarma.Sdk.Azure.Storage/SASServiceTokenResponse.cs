using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// The token response
    /// </summary>
    public class SASServiceTokenResponse
    {
        /// <summary>
        /// The token
        /// </summary>
        [JsonProperty("serviceSasToken")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SASServiceTokenResponse() { }
    }
}
