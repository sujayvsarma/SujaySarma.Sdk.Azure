using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Compute.LogAnalytics
{
    /// <summary>
    /// Request to retrieve compute resource log analytics
    /// </summary>
    public class ComputeLogAnalyticsRequest
    {

        /// <summary>
        /// SAS URI to a blob container to which the Log Analytics system can write the 
        /// output of this request to
        /// </summary>
        [JsonProperty("blobContainerSasUri")]
        public string BlobContainerSasUri { get; set; } = string.Empty;

        /// <summary>
        /// The starting date/time to being the logs from
        /// </summary>
        [JsonProperty("fromTime")]
        public DateTime TimeWindowStart { get; set; } = DateTime.MinValue;

        /// <summary>
        /// The ending date/time to query the logs until
        /// </summary>
        [JsonProperty("toTime")]
        public DateTime TimeWindowEnd { get; set; } = DateTime.MinValue;

        /// <summary>
        /// If set, logs are grouped by the name of the operation. Default: False
        /// </summary>
        [JsonProperty("groupByOperationName")]
        public bool GroupByOperationName { get; set; } = false;

        /// <summary>
        /// If set, logs are grouped by the name of the resource. Default: True
        /// </summary>
        [JsonProperty("groupByResourceName")]
        public bool GroupByResourceName { get; set; } = true;

        /// <summary>
        /// If set, logs are grouped by the throttle policy setting. Default: False
        /// </summary>
        [JsonProperty("groupByThrottlePolicy")]
        public bool GroupByThrottlePolicy { get; set; } = false;

        /// <summary>
        /// Intervals (in minutes) to poll for log entries in. Use the ENUM only as this requires specific 
        /// names for the intervals.
        /// </summary>
        [JsonProperty("intervalLength")]
        public ComputeLogAnalyticsRequestIntervals Length { get; set; } = ComputeLogAnalyticsRequestIntervals.ThreeMins;


        public ComputeLogAnalyticsRequest() { }
    }


    /// <summary>
    /// Length of log required (calculated from starting time)
    /// </summary>
    public enum ComputeLogAnalyticsRequestIntervals
    {
        /// <summary>
        /// 3 minutes
        /// </summary>
        ThreeMins,

        /// <summary>
        /// 5 minutes
        /// </summary>
        FiveMins,

        /// <summary>
        /// 30 minutes
        /// </summary>
        ThirtyMins,

        /// <summary>
        /// 60 minutes
        /// </summary>
        SixtyMins
    }
}
