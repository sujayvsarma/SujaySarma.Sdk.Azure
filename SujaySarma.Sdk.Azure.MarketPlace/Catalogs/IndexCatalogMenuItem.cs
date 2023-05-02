using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// An item in the <see cref="IndexCatalogMenu"/>
    /// </summary>
    public class IndexCatalogMenuItem
    {

        /// <summary>
        /// The Catalog ID (<seealso cref="IndexCatalog.CatalogID"/>)
        /// </summary>
        [JsonProperty("id")]
        public string CatalogId { get; set; } = string.Empty;

        /// <summary>
        /// The display title
        /// </summary>
        [JsonProperty("text")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// ISO language code (eg: "en")
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; set; } = "en";

        /// <summary>
        /// ISO locale code (eg: "en-us")
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; } = "en-us";


        /// <summary>
        /// Items in the category
        /// </summary>
        [JsonProperty("items")]
        public List<IndexCatalogMenuItemItem> Items { get; set; } = new List<IndexCatalogMenuItemItem>();

    }
}
