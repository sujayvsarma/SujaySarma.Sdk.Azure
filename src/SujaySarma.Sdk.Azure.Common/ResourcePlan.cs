using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// SKU purchase/subscription plan identification
    /// </summary>
    public class ResourcePlan
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Offer ID
        /// </summary>
        [JsonProperty("product")] 
        public string? Product { get; set; }

        /// <summary>
        /// Promo Code if applied
        /// </summary>
        [JsonProperty("promotionCode")] 
        public string? PromotionalCode { get; set; }

        /// <summary>
        /// Publisher ID
        /// </summary>
        [JsonProperty("publisher")] 
        public string? Publisher { get; set; }

        /// <summary>
        /// Version of the plan
        /// </summary>
        [JsonProperty("version")] 
        public string? Version { get; set; }


        public ResourcePlan() { }
    }
}
