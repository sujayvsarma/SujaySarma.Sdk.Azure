using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.LogAnalytics
{
    /// <summary>
    /// The response returned by the Log Analytics API to a ComputeLogAnalyticsRequest.
    /// This class is used internally by the LogAnalytics client!
    /// </summary>
    public class ComputeLogAnalyticsResponse
    {
        /// <summary>
        /// Properties of the response
        /// </summary>
        [JsonProperty("properties")]
        public ComputeLogAnalyticsResponseProperties Properties { get; set; } = new ComputeLogAnalyticsResponseProperties();

        public ComputeLogAnalyticsResponse() { }
    }
}
