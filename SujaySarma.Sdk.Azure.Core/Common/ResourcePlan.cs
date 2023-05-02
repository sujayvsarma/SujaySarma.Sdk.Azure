using Newtonsoft.Json;

using System;

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

        /// <summary>
        /// Create a new resource plan
        /// </summary>
        public ResourcePlan() { }

        /// <summary>
        /// Create a resource plan
        /// </summary>
        /// <param name="name">Name of the plan</param>
        /// <param name="offerId">The Offer or product ID of the item</param>
        /// <param name="publisherId">Id of the publisher</param>
        /// <param name="version">Version number</param>
        /// <param name="promoCode">Promo code if applied/claimed</param>
        public ResourcePlan(string name, string offerId, string publisherId, string version, string? promoCode = null)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (string.IsNullOrWhiteSpace(offerId)) { throw new ArgumentNullException(nameof(offerId)); }
            if (string.IsNullOrWhiteSpace(publisherId)) { throw new ArgumentNullException(nameof(publisherId)); }
            if (string.IsNullOrWhiteSpace(version)) { throw new ArgumentNullException(nameof(version)); }

            Name = name;
            Product = offerId;
            Publisher = publisherId;
            Version = version;
            PromotionalCode = promoCode;
        }
    }
}
