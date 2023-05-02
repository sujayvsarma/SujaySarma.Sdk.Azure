using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Request to restore a blob
    /// </summary>
    public class BlobRestoreRequest
    {
        /// <summary>
        /// Timestamp to restore to
        /// </summary>
        [JsonProperty("timeToRestore")]
        public string PointInTime { get; set; } = string.Empty;

        /// <summary>
        /// Blob ranges to restore. Must contain atleast one element.
        /// </summary>
        [JsonProperty("blobRanges")]
        public List<BlobRestoreRange> Ranges { get; set; } = new List<BlobRestoreRange>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlobRestoreRequest() { }
    }

}
