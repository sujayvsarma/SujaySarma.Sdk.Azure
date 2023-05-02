using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        public string? SizeName { get; set; } = string.Empty;

        /// <summary>
        /// Tier name (eg: "Basic")
        /// </summary>
        [JsonProperty("tier")]
        public string? Tier { get; set; } = string.Empty;

        /// <summary>
        /// SKU family name (eg: "BasicA0_A100Family")
        /// </summary>
        [JsonProperty("family")]
        public string? Family { get; set; } = string.Empty;

        /// <summary>
        /// Locations this SKU is available at
        /// </summary>
        [JsonProperty("locations")]
        public List<string>? Locations { get; set; } = new List<string>();

        /// <summary>
        /// More information about the locations
        /// </summary>
        [JsonProperty("locationInfo")]
        public List<ComputeResourceSkuLocationInfo>? LocationInfo { get; set; } = new List<ComputeResourceSkuLocationInfo>();

        /// <summary>
        /// Capabilities of a VM - rebuilt from the actual structure returned to make it 
        /// easier to use (is never NULL!)
        /// </summary>
        public ReadOnlyDictionary<string, string> Capabilities
        {
            get
            {
                if (_capabilitiesRebuilt == null)
                {
                    _capabilitiesRebuilt = new Dictionary<string, string>();

                    if (_capabilities != null)
                    {
                        foreach (Dictionary<string, string> item in _capabilities)
                        {
                            _capabilitiesRebuilt.Add(item["name"], item["value"]);
                        }
                    }
                }

                return new ReadOnlyDictionary<string, string>(_capabilitiesRebuilt);
            }
        }
        private Dictionary<string, string>? _capabilitiesRebuilt = null;

#pragma warning disable IDE0044 // Add readonly modifier (Is de/hydrated by Json)

        [JsonProperty("capabilities")]
        private List<Dictionary<string, string>>? _capabilities = null;

#pragma warning restore IDE0044 // Add readonly modifier

        public ComputeResourceSku() { }

        /// <summary>
        /// Instantiate a compute resource Sku structure
        /// </summary>
        /// <param name="name">Name of the SKU (eg: "Basic_A0")</param>
        /// <param name="sizeName">Name of the size (eg: "A0")</param>
        /// <param name="tierName">Name of the tier the SKU belongs in (eg: "Basic")</param>
        /// <param name="familyName">Name of the SKU family (eg: "BasicA0_A100Family")</param>
        public ComputeResourceSku(string name, string? sizeName = null, string? tierName = null, string? familyName = null)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }

            Name = name;
            SizeName = ((string.IsNullOrWhiteSpace(sizeName)) ? null : sizeName);
            Tier = ((string.IsNullOrWhiteSpace(tierName)) ? null : tierName);
            Family = ((string.IsNullOrWhiteSpace(familyName)) ? null : familyName);
        }



        /// <summary>
        /// Clears all the locations
        /// </summary>
        public void ClearLocations()
        {
            Locations?.Clear();
            LocationInfo?.Clear();
        }

        /// <summary>
        /// Add a location to enable for this SKU
        /// </summary>
        /// <param name="locationName">Name of location to add</param>
        public void AddLocation(string locationName)
        {
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }
            if ((Locations != null) && (Locations.Count > 0))
            {
                foreach (string z in Locations)
                {
                    if (z.Equals(locationName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new ArgumentException($"{locationName} already exists in list.");
                    }
                }
            }

            if (Locations == null)
            {
                Locations = new List<string>();
            }

            if (LocationInfo == null)
            {
                LocationInfo = new List<ComputeResourceSkuLocationInfo>();
            }

            Locations.Add(locationName);
            LocationInfo.Add(new ComputeResourceSkuLocationInfo(locationName));
        }

        /// <summary>
        /// Clear all capabilities
        /// </summary>
        public void ClearCapabilities() { _capabilities = null; _capabilitiesRebuilt = null; }

        /// <summary>
        /// Add a capability to the SKU
        /// </summary>
        /// <param name="name">Name of capability</param>
        /// <param name="value">Value</param>
        public void AddCapability(string name, string value)
        {
            if (_capabilities == null)
            {
                _capabilities = new List<Dictionary<string, string>>();
            }

            if (_capabilitiesRebuilt != null)
            {
                // we can validate!
                if (_capabilitiesRebuilt.ContainsKey(name))
                {
                    throw new ArgumentException($"A capability with the name {name} already exists.");
                }
            }

            _capabilities.Add(new Dictionary<string, string>() { { name, value } });

            if (_capabilitiesRebuilt != null)
            {
                // also add it here
                _capabilitiesRebuilt.Add(name, value);
            }
        }
    }
}
