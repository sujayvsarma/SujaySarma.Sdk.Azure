using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Rane of blobs that are to be restored
    /// </summary>
    public class BlobRestoreRange
    {
        /// <summary>
        /// Start of the range. Is inclusive value and an empty value means start of blob
        /// </summary>
        [JsonProperty("startRange")]
        public string? Start { get; set; }

        /// <summary>
        /// End of range. Is inclusive value and an empty value means end of blob
        /// </summary>
        [JsonProperty("endRange")]
        public string? End { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlobRestoreRange() { }
    }

}
