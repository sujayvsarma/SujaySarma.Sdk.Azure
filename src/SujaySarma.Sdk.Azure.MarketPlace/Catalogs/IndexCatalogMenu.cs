using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// This is the actual structure returned by the Azure Catalog API
    /// </summary>
    public class IndexCatalogMenu
    {

        /// <summary>
        /// Static menus
        /// </summary>
        [JsonProperty("staticMenus")]
        public List<IndexCatalogMenuItem> StaticMenu { get; set; } = new List<IndexCatalogMenuItem>();

        /// <summary>
        /// Dynamic menus. Usual Ids are: "gettingStarted" and "recent30"
        /// </summary>
        [JsonProperty("dynamicMenus")]
        public List<IndexCatalogMenuItem> DynamicMenu { get; set; } = new List<IndexCatalogMenuItem>();

    }
}
