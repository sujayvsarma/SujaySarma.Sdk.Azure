
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Services that support encryption and statuses
    /// </summary>
    public class EncryptableServices
    {
        /// <summary>
        /// Blob service
        /// </summary>
        [JsonProperty("blob")]
        public EncryptionService Blob { get; set; } = new EncryptionService();

        /// <summary>
        /// File service
        /// </summary>
        [JsonProperty("file")]
        public EncryptionService File { get; set; } = new EncryptionService();

        /// <summary>
        /// Queue service
        /// </summary>
        [JsonProperty("queue")]
        public EncryptionService Queue { get; set; } = new EncryptionService();

        /// <summary>
        /// Table service
        /// </summary>
        [JsonProperty("table")]
        public EncryptionService Table { get; set; } = new EncryptionService();

        /// <summary>
        /// Default constructor
        /// </summary>
        public EncryptableServices() { }
    }
}
