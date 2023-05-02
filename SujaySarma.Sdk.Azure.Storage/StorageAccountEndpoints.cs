
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Endpoints available on a Storage Account
    /// </summary>
    public class StorageAccountEndpoints : StorageAccountEndpointUris
    {
        /// <summary>
        /// Internet-routable endpoint Uri
        /// </summary>
        [JsonProperty("internetEndpoints")]
        public StorageAccountEndpointUris InternetEndpoints { get; set; } = new StorageAccountEndpointUris();

        /// <summary>
        /// Microsoft-routed endpoint Uri
        /// </summary>
        [JsonProperty("microsoftEndpoints")]
        public StorageAccountEndpointUris MicrosoftEndpoints { get; set; } = new StorageAccountEndpointUris();

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountEndpoints() { }
    }
}
