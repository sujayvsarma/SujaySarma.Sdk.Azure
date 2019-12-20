using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.ResourceSkus
{
    /// <summary>
    /// A SKU of a compute resource
    /// </summary>
    public class ComputeResourceSku
    {

        /// <summary>
        /// Type of resource. This is also the "type" portion of a Resource URI.
        /// </summary>
        [JsonProperty("resourceType")]
        public string Type { get; set; } = "virtualMachines";

        /// <summary>
        /// Name of the SKU (eg: "Basic_A0")
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Size name (eg: "A0")
        /// </summary>
        [JsonProperty("size")]
        public string SizeName { get; set; } = string.Empty;

        /// <summary>
        /// Tier name (eg: "Basic")
        /// </summary>
        [JsonProperty("tier")]
        public string Tier { get; set; } = string.Empty;

        /// <summary>
        /// SKU family name (eg: "BasicA0_A100Family")
        /// </summary>
        [JsonProperty("family")]
        public string Family { get; set; } = string.Empty;

        /// <summary>
        /// Locations this SKU is available at
        /// </summary>
        [JsonProperty("locations")]
        public List<string> Locations { get; set; } = new List<string>();

        /// <summary>
        /// List of capabilities. This is hard to use, use the other (public) dictionary
        /// </summary>
        [JsonProperty("capabilities")]
        private List<Dictionary<string, string>> _capabilities = new List<Dictionary<string, string>>();

        /// <summary>
        /// More information about the locations
        /// </summary>
        [JsonProperty("locationInfo")]
        public List<ComputeResourceSkuLocationInfo> LocationInfo { get; set; } = new List<ComputeResourceSkuLocationInfo>();


        /// <summary>
        /// Capabilities of a VM - rebuilt from the actual structure returned to make it 
        /// easier to use
        /// </summary>
        public Dictionary<string, string> Capabilities
        {
            get
            {
                if (_capabilitiesRebuilt == null)
                {
                    _capabilitiesRebuilt = new Dictionary<string, string>();
                    foreach(Dictionary<string, string> item in _capabilities)
                    {
                        _capabilitiesRebuilt.Add(item["name"], item["value"]);
                    }
                }

                return _capabilitiesRebuilt;
            }
        }
        private Dictionary<string, string>? _capabilitiesRebuilt = null;


        public ComputeResourceSku() { }
    }
}
