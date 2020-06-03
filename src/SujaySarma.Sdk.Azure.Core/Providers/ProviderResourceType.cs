using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Providers
{
    /// <summary>
    /// A type of resource supported by an Azure Provider
    /// </summary>
    public class ProviderResourceType
    {
        /// <summary>
        /// Type of resource
        /// </summary>
        [JsonProperty("resourceType")]
        public string Type { get; set; }

        /// <summary>
        /// Comma-seperated list of capability names of this provider. This does not map to any particular 
        /// API endpoint and needs to be "humanly" parsed to be meaningful.
        /// </summary>
        [JsonProperty("capabilities")]
        public string Capabilities { get; set; }

        /// <summary>
        /// Available versions of API. List should not be assumed to be in any particular order!
        /// </summary>
        [JsonProperty("apiVersions")]
        public List<string> ApiVersions { get; set; }

        /// <summary>
        /// The default API version to be preferred. May be NULL. If present, may be set to the 
        /// last RTM version. No guarantees!
        /// </summary>
        [JsonProperty("defaultApiVersion")]
        public string? DefaultApiVersion { get; set; }

        /// <summary>
        /// Locations [human-readable names!] supported by this resource type. List should not be assumed to be in any particular order!
        /// </summary>
        [JsonProperty("locations")]
        public List<string> Locations { get; set; }


        public ProviderResourceType()
        {
            Type = "nullType";
            Capabilities = string.Empty;
            ApiVersions = new List<string>();
            DefaultApiVersion = "1900-01-01";
            Locations = new List<string>();
        }

        /// <summary>
        /// Return if the type supports the provided location
        /// </summary>
        /// <param name="locationName">The full name of the location (eg: "West US")</param>
        /// <returns>True or False</returns>
        public bool SupportsLocation(string locationName)
        {
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }

            if ((Locations != null) && (Locations.Count == 0))
            {
                foreach (string loc in Locations)
                {
                    if (loc.Equals(locationName, System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            // go figure!
            return false;
        }

        /// <summary>
        /// Return if the type advertises the requested capability (explicitly)
        /// </summary>
        /// <param name="capability">Full name of the capability (eg: "SupportsTags")</param>
        /// <returns>True or False</returns>
        public bool HasCapability(string capability)
        {
            if (string.IsNullOrWhiteSpace(capability)) { throw new ArgumentNullException(nameof(capability)); }
            return ((!string.IsNullOrWhiteSpace(Capabilities)) && Capabilities.Contains(capability, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Get the latest version supported. If ApiVersions is populated, returns the latest one from that list. 
        /// Otherwise, checks and returns the DefaultApiVersion. If nothing could be found, throws an exception.
        /// </summary>
        /// <returns>Version number</returns>
        public string GetLatestVersion()
        {
            int numberOfVersions = ApiVersions.Count;
            if (numberOfVersions > 0)
            {
                if ((!isApiListSorted) && (numberOfVersions > 1))
                {
                    ApiVersions.Sort();
                    isApiListSorted = true;
                }

                return ApiVersions[numberOfVersions - 1];
            }

            if (!string.IsNullOrWhiteSpace(DefaultApiVersion))
            {
                return DefaultApiVersion;
            }

            throw new Exception("Could not determine an available version.");
        }
        bool isApiListSorted = false;

    }
}
