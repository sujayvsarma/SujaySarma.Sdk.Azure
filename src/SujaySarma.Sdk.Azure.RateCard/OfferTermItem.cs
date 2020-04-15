using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.RateCard
{
    /// <summary>
    /// A single offer in a rate card
    /// </summary>
    public class OfferTermItem
    {
        /// <summary>
        /// Monetary credit value if any
        /// </summary>
        [JsonProperty("Credit")]
        public int? Credit { get; set; } = null;

        /// <summary>
        /// Tiered discounts if any. Key would be the bill incurred and the value the percentage of 
        /// discount to be applied to that slab.
        /// </summary>
        [JsonProperty("TieredDiscount")]
        public Dictionary<string, double>? TieredDiscounts { get; set; } = null;

        /// <summary>
        /// Name of the term
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Effective date of start of this term
        /// </summary>
        [JsonProperty("EffectiveDate")]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Ids of meters excluded from this offer
        /// </summary>
        [JsonProperty("ExcludedMeterIds")]
        public List<string>? ExcludedMeters { get; set; } = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public OfferTermItem() { }
    }

}
