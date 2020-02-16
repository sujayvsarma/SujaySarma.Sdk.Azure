using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// Links in a <see cref="IndexItem"/>.
    /// </summary>
    public class Link
    {
        /// <summary>
        /// An Id (for uniqueness) for this item. Typically numeric, but may be different.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// A UI-friendly name for the link
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// The link address
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Link() { }
    }
}
