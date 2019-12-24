using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.LogAnalytics
{
    /// <summary>
    /// Properties of a ComputeLogAnalyticsResponse
    /// </summary>
    public class ComputeLogAnalyticsResponseProperties
    {

        /// <summary>
        /// The URI to the output file (usually a .csv file) containing the results of the queried data
        /// </summary>
        [JsonProperty("output")]
        public string OutputFileUri { get; set; } = string.Empty;


        public ComputeLogAnalyticsResponseProperties() { }
    }
}
