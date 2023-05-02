using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// Marketing material attached to a <see cref="IndexItem"/>.
    /// </summary>
    public class MarketingMaterial
    {
        /// <summary>
        /// Path to the SKU item in the marketplace. (usually of the format (marketplace/partners/[publisherId]/[skuId])
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Uri to the learning/marketing material
        /// </summary>
        [JsonProperty("learnUri")]
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MarketingMaterial() { }
    }
}
