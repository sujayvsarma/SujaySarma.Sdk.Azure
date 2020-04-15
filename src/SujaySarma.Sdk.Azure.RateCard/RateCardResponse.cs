using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.RateCard
{
    /// <summary>
    /// Structure encapsulating the entire ratecard response data
    /// </summary>
    public class RateCardResponse
    {
        /// <summary>
        /// All the offers applicable for this rate card/subscription
        /// </summary>
        [JsonProperty("OfferTerms")]
        public List<OfferTermItem> OfferTerms { get; set; } = new List<OfferTermItem>();

        /// <summary>
        /// Applicable meters / rates
        /// </summary>
        [JsonProperty("Meters")]
        public List<MeterItem> Meters { get; set; } = new List<MeterItem>();

        /// <summary>
        /// ISO currency name the rates are in (eg: "USD")
        /// </summary>
        [JsonProperty("Currency")]
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// The ISO locale code (eg: "en-US")
        /// </summary>
        [JsonProperty("Locale")]
        public string ISOLocale { get; set; } = string.Empty;

        /// <summary>
        /// If true, tax rates are included in the returned pricing. Typically, this would be False.
        /// </summary>
        [JsonProperty("IsTaxIncluded")]
        public bool IsTaxIncluded { get; set; } = false;

        /// <summary>
        /// The metering region name
        /// </summary>
        [JsonProperty("MeterRegion")]
        public string Region { get; set; } = string.Empty;

        /// <summary>
        /// The tags
        /// </summary>
        [JsonProperty("Tags")]
        public Dictionary<string, object> Tags { get; set; } = new Dictionary<string, object>();
    }

}
