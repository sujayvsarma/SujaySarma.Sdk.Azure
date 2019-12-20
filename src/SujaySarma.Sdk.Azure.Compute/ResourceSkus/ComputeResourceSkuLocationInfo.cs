using System.Collections.Generic;

using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.ResourceSkus
{
    /// <summary>
    /// More details of the location embedded in a resource sku metadata element
    /// </summary>
    public class ComputeResourceSkuLocationInfo
    {

        /// <summary>
        /// The internal name of the location (eg: "westus")
        /// </summary>
        [JsonProperty("location")]
        public string InternalName { get; set; } = string.Empty;

        /// <summary>
        /// The zone names this sku can be at in this location
        /// </summary>
        [JsonProperty("zones")]
        public List<string> Zones { get; set; } = new List<string>();


        public ComputeResourceSkuLocationInfo() { }
    }
}
