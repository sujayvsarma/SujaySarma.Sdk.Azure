using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// A resource that is used within another resource. Used to cross-reference 
    /// resources in complex hierarchies.
    /// </summary>
    public class SubResource
    {
        /// <summary>
        /// ResourceId URI of the subresource
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; } = string.Empty;

        /// <summary>
        /// Create a new sub resource
        /// </summary>
        public SubResource() { }

        /// <summary>
        /// Create a new sub resource
        /// </summary>
        /// <param name="resourceId">The Resource Id URI string</param>
        public SubResource(string resourceId) => ResourceId = (string.IsNullOrWhiteSpace(resourceId) ? throw new ArgumentNullException(nameof(resourceId)) : resourceId);

        /// <summary>
        /// Create a new sub resource
        /// </summary>
        /// <param name="resourceId">A ResourceUri object containing the resource's URI</param>
        public SubResource(ResourceUri resourceId) => ResourceId = ((resourceId == null) ? throw new ArgumentNullException(nameof(resourceId)) : resourceId.ToString());
    }
}
