
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// This is the top-level catalog that contains all other catalogs in the marketplace system
    /// (Examples: "Compute_MP", "Database_MP", etc)
    /// </summary>
    public class IndexCatalog
    {

        /// <summary>
        /// ID of the catalog item
        /// </summary>
        [JsonProperty("id")]
        public string CatalogID { get; set; } = string.Empty;

        /// <summary>
        /// UI-friendly display name of the catalog
        /// </summary>
        [JsonProperty("text")]
        public string Name { get; set; } = string.Empty;

        // These two are meant for service calls. Information is set here when the 
        // CatalogEntry is retrieved. We use it to retrieve the groups and its items 
        // in the same langauge as the master list.
        internal string Language = "en";
        internal string Locale = "en-us";


        /// <summary>
        /// Default constructor
        /// </summary>
        public IndexCatalog() { }

    }
}
