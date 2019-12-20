using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// The SKU instantiated to create a resource
    /// </summary>
    public class ResourceSku
    {
        [JsonProperty("capacity")]
        public int? Capacity { get; set; } = null;

        [JsonProperty("family")]
        public string? Family { get; set; } = null;

        [JsonProperty("model")]
        public string? Model { get; set; } = null;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("size")]
        public string? Size { get; set; } = null;

        [JsonProperty("tier")]
        public string? Tier { get; set; } = null;


        public ResourceSku() { }
    }
}
