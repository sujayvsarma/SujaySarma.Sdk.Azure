
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Statistics of georeplication
    /// </summary>
    public class GeoReplicationStatistics
    {
        /// <summary>
        /// Flag indicating if failover is supported for this account
        /// </summary>
        [JsonProperty("canFailover")]
        public bool IsFailoverSupported { get; set; }

        /// <summary>
        /// Timestamp of the last geo sync happened. All WRITEs before this timestamp are guaranteed to be 
        /// available for READs.
        /// </summary>
        [JsonProperty("lastSyncTime")]
        public string? LastSyncTime { get; set; }

        /// <summary>
        /// Status of the georeplication process
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public GeoReplicationStatus Status { get; set; }
    }
}
