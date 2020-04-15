using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Request for an SAS service-level token
    /// </summary>
    public class SASServiceTokenRequest : SASTokenRequest
    {
        /// <summary>
        /// Canonicalized path to the resource
        /// </summary>
        [JsonProperty("canonicalizedResource")]
        public string CanonicalResourceUri { get; set; } = string.Empty;

        /// <summary>
        /// Partition key range start
        /// </summary>
        [JsonProperty("startPk")]
        public string? PartitionKeyRangeStart { get; set; }

        /// <summary>
        /// Partition key range end
        /// </summary>
        [JsonProperty("endPk")]
        public string? PartitionKeyRangeEnd { get; set; }

        /// <summary>
        /// Row key range start
        /// </summary>
        [JsonProperty("startRk")]
        public string? RowKeyRangeStart { get; set; }

        /// <summary>
        /// Row key range end
        /// </summary>
        [JsonProperty("endRk")]
        public string? RowKeyRangeEnd { get; set; }

        /// <summary>
        /// Response header override string for Cache Control
        /// </summary>
        [JsonProperty("rscc")]
        public string? ResponseHeaderOverrideCacheControl { get; set; }

        /// <summary>
        /// Response header override string for Content Disposition
        /// </summary>
        [JsonProperty("rscd")]
        public string? ResponseHeaderOverrideContentDisposition { get; set; }

        /// <summary>
        /// Response header override string for Content Encoding
        /// </summary>
        [JsonProperty("rsce")]
        public string? ResponseHeaderOverrideContentEncoding { get; set; }

        /// <summary>
        /// Response header override string for Content Language
        /// </summary>
        [JsonProperty("rscl")]
        public string? ResponseHeaderOverrideContentLanguage { get; set; }

        /// <summary>
        /// Response header override string for Content Type
        /// </summary>
        [JsonProperty("rsct")]
        public string? ResponseHeaderOverrideContentType { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SASServiceTokenRequest() : base() { }
    }
}
