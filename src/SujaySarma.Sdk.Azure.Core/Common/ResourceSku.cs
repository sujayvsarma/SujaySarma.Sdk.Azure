using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// The SKU instantiated to create a resource
    /// </summary>
    public class ResourceSku
    {

        /// <summary>
        /// Name of the SKU
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// SKU's tier of privileges or operation
        /// </summary>
        [JsonProperty("tier")]
        public string? Tier { get; set; } = null;

        /// <summary>
        /// Family the SKU belongs to
        /// </summary>
        [JsonProperty("family")]
        public string? Family { get; set; } = null;

        /// <summary>
        /// Size name of the SKU
        /// </summary>
        [JsonProperty("size")]
        public string? Size { get; set; } = null;

        /// <summary>
        /// Capacity limits of the SKU
        /// </summary>
        [JsonProperty("capacity")]
        public int? Capacity { get; set; } = null;

        /// <summary>
        /// Model number or name of the SKU
        /// </summary>
        [JsonProperty("model")]
        public string? Model { get; set; } = null;


        public ResourceSku() { }
    }
}
