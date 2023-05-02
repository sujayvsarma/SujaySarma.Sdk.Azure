using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// An item in the <see cref="IndexCatalogMenuItem"/>
    /// </summary>
    public class IndexCatalogMenuItemItem
    {
        /// <summary>
        /// The item Id (<seealso cref="IndexItem.Id"/>)
        /// </summary>
        [JsonProperty("id")]
        public string ItemId { get; set; } = string.Empty;

        /// <summary>
        /// The display title
        /// </summary>
        [JsonProperty("text")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Absolute URI to a (PNG) file that has the icon to display for this item.
        /// </summary>
        [JsonProperty("icon")]
        public string IconUri { get; set; } = string.Empty;

    }
}
