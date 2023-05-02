using Newtonsoft.Json;

using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Instantiate a location info structure
        /// </summary>
        /// <param name="location">Name of the location (eg: "westus")</param>
        public ComputeResourceSkuLocationInfo(string location)
        {
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }
            InternalName = location;
        }

        /// <summary>
        /// Clears all the zones
        /// </summary>
        public void ClearZones() => Zones?.Clear();

        /// <summary>
        /// Add an availability zone
        /// </summary>
        /// <param name="zoneName">Name of availability zone to add</param>
        public void AddZone(string zoneName)
        {
            if (string.IsNullOrWhiteSpace(zoneName)) { throw new ArgumentNullException(nameof(zoneName)); }
            if ((Zones != null) && (Zones.Count > 0))
            {
                foreach (string z in Zones)
                {
                    if (z.Equals(zoneName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new ArgumentException($"{zoneName} already exists in list.");
                    }
                }
            }

            if (Zones == null)
            {
                Zones = new List<string>();
            }

            Zones.Add(zoneName);
        }
    }
}
