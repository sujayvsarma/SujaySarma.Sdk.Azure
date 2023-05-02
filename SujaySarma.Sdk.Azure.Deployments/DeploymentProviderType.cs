using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Type of a provider required for a deployment request
    /// </summary>
    public class DeploymentProviderType
    {
        /// <summary>
        /// ProviderId
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Namespace of the provider
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; } = string.Empty;

        /// <summary>
        /// Registration policy
        /// </summary>
        [JsonProperty("registrationPolicy")]
        public string? RegistrationPolicy { get; set; }

        /// <summary>
        /// Status of registration
        /// </summary>
        [JsonProperty("registrationState")]
        public string? RegistrationState { get; set; }

        /// <summary>
        /// Collection of resource types this provider supports
        /// </summary>
        [JsonProperty("resourceTypes")]
        public List<DeploymentProviderTypeResourceType> ResourceTypes { get; set; } = new List<DeploymentProviderTypeResourceType>();
    }
}
