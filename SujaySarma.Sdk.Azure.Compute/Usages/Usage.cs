using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Compute.Usages
{
    /// <summary>
    /// Usage information of a Compute resource. 
    /// This structure is created in response to a request
    /// </summary>
    public class Usage
    {
        /// <summary>
        /// Name of the usage counter
        /// </summary>
        [JsonProperty("name")]
        public LocalizedStringValue Name { get; set; } = new LocalizedStringValue();

        /// <summary>
        /// Unit of measure
        /// </summary>
        [JsonProperty("unit")]
        public string Unit { get; set; } = "Count";

        /// <summary>
        /// Usage limit (max)
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// The current usage
        /// </summary>
        [JsonProperty("currentValue")]
        public int Current { get; set; }


        public Usage() { }
    }
}
