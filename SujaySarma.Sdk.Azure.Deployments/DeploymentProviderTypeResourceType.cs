using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Resource type supported by a deployment provider
    /// </summary>
    public class DeploymentProviderTypeResourceType
    {
        /// <summary>
        /// Supported API versions list
        /// </summary>
        [JsonProperty("apiVersions")]
        public List<string> ApiVersions { get; set; } = new List<string>();

        /// <summary>
        /// Resource type
        /// </summary>
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; } = string.Empty;

        /// <summary>
        /// Additional capabilities of the provider
        /// </summary>
        [JsonProperty("capabilities")]
        public string Capabilities { get; set; } = string.Empty;

        /// <summary>
        /// Supported locations
        /// </summary>
        [JsonProperty("locations")]
        public List<string> Locations { get; set; } = new List<string>();

        /// <summary>
        /// Additional properties of this provider
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Aliases of this provider
        /// </summary>
        [JsonProperty("aliases")]
        public List<DeploymentProviderAlias> Aliases { get; set; } = new List<DeploymentProviderAlias>();
    }
}
