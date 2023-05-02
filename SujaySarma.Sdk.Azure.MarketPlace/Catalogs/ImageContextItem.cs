using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// Image contexts for the <see cref="IndexItem"/>
    /// </summary>
    public class ImageContextItem
    {
        /// <summary>
        /// Context where the image is used. Typically "ibiza" (code name for Azure Portal generation?)
        /// </summary>
        [JsonProperty("context")]
        public string? Context { get; set; } = "ibiza";

        /// <summary>
        /// Images list (Id values are usually: "small", "medium", "large" and "wide")
        /// </summary>
        [JsonProperty("items")]
        public List<SkuArtifact> Items { get; set; } = new List<SkuArtifact>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ImageContextItem() { }
    }
}
