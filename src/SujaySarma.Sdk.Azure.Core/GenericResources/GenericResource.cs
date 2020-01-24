using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Core.GenericResources
{
    /// <summary>
    /// A generic resource
    /// </summary>
    public class GenericResource
    {

        /// <summary>
        /// Resource ID URI
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; } = string.Empty;

        /// <summary>
        /// Identity for the resource
        /// </summary>
        [JsonProperty("identity")]
        public ResourceIdentity? Identity { get; set; }

        /// <summary>
        /// Kind of resource
        /// </summary>
        [JsonProperty("kind")]
        public string? Kind { get; set; }

        /// <summary>
        /// Type of resource
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Azure datacenter region
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; } = "global";

        /// <summary>
        /// URI to the entity that manages this resource
        /// </summary>
        [JsonProperty("managedBy")]
        public string? ManagedBy { get; set; }

        /// <summary>
        /// Name of the resource
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// SKU purchase/subscription plan identification
        /// </summary>
        [JsonProperty("plan")]
        public ResourcePlan? Plan { get; set; }

        /// <summary>
        /// Properties specific to the resource
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object>? Properties { get; set; }

        /// <summary>
        /// The SKU that was instantiated
        /// </summary>
        [JsonProperty("sku")]
        public ResourceSku? Sku { get; set; }

        /// <summary>
        /// Tags attached to this resource
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, string>? Tags { get; set; }


        public GenericResource() { }


    }
}
