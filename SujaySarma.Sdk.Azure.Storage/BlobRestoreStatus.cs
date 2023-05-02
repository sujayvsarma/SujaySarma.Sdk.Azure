using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Status (results) of a blob restore operation
    /// </summary>
    public class BlobRestoreStatus
    {
        /// <summary>
        /// Reason for failure (if failed)
        /// </summary>
        [JsonProperty("failureReason")]
        public string? FailureReason { get; set; }

        /// <summary>
        /// Original parameters of the request
        /// </summary>
        [JsonProperty("parameters")]
        public BlobRestoreParameters Parameters { get; set; } = new BlobRestoreParameters();

        /// <summary>
        /// Id to track the restore operation
        /// </summary>
        [JsonProperty("restoreId")]
        public string? OperationId { get; set; }

        /// <summary>
        /// Status of blob restore operation
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public BlobRestoreProgress Status { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlobRestoreStatus() { }
    }



}
