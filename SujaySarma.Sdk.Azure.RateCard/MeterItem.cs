using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.RateCard
{
    /// <summary>
    /// A single meter object in a rate card
    /// </summary>
    public class MeterItem
    {
        /// <summary>
        /// Start date of this meter
        /// </summary>
        [JsonProperty("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Quantum of resources included (for free) with this meter
        /// </summary>
        [JsonProperty("IncludedQuantity")]
        public int IncludedQuantity { get; set; } = 0;

        /// <summary>
        /// Name of category of the meter
        /// </summary>
        [JsonProperty("MeterCategory")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Name of the subcategory of the meter
        /// </summary>
        [JsonProperty("MeterSubCategory")]
        public string? SubCategory { get; set; } = null;

        /// <summary>
        /// Id of the meter
        /// </summary>
        [JsonProperty("MeterId")]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the meter
        /// </summary>
        [JsonProperty("MeterName")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The rates under this meter. Key is simply an index and value is the rate. See the Unit property for 
        /// details of the billing time slab unit
        /// </summary>
        [JsonProperty("MeterRates")]
        public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();

        /// <summary>
        /// The unit of rates (eg: "1 Hour" for a per hour rate)
        /// </summary>
        [JsonProperty("Unit")]
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the region (eg: "US West")
        /// </summary>
        [JsonProperty("MeterRegion")]
        public string RegionName { get; set; } = string.Empty;

        /// <summary>
        /// Status of the meter - Active or Deprecated
        /// </summary>
        [JsonProperty("MeterStatus")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("MeterTags")]
        public Dictionary<string, object> Tags { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeterItem() { }
    }

}
