using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// An artifact for a <see cref="GroupItem"/>.
    /// </summary>
    public class SkuArtifact
    {
        /// <summary>
        /// The name of the artifact. This is NOT for UI-use, but can be used as a sort of an index. There are many 
        /// possible values (from things we have seen repeated), but the meaning of that name is not guaranteed.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of artifact.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Absolute URI to the artifact.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SkuArtifact() { }
    }
}
