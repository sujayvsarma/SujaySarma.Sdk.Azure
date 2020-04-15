using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Parameters for a blob restore request
    /// </summary>
    public class BlobRestoreParameters
    {
        /// <summary>
        /// Ranges of blobs to restore
        /// </summary>
        [JsonProperty("blobRanges")]
        public List<BlobRestoreRange> Ranges { get; set; } = new List<BlobRestoreRange>();

        /// <summary>
        /// Point in time (timestamp) to restore to
        /// </summary>
        [JsonProperty("timeToRestore")]
        public string RestoreTo { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlobRestoreParameters() { }
    }



}
