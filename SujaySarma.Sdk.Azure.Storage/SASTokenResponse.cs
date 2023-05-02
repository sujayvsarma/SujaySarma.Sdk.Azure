using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// The token response
    /// </summary>
    public class SASTokenResponse
    {
        /// <summary>
        /// The token
        /// </summary>
        [JsonProperty("accountSasToken")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SASTokenResponse() { }
    }
}
