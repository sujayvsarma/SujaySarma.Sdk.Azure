using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Subscriptions
{
    /// <summary>
    /// Policies applied to the subscription
    /// </summary>
    public class SubscriptionPolicy
    {
        #region Properties

        /// <summary>
        /// The Id of the location where the subscription is placed
        /// </summary>
        [JsonProperty("locationPlacementId")]
        public string? LocationPlacementId { get; set; }

        /// <summary>
        /// The quota Id
        /// </summary>
        [JsonProperty("quotaId")]
        public string? QuotaId { get; set; }

        /// <summary>
        /// Whether spending limit is on or off
        /// </summary>
        [JsonProperty("spendingLimit")]
        public string? SpendingLimit { get; set; }

        #endregion

        public SubscriptionPolicy()
        {
            LocationPlacementId = string.Empty;
            QuotaId = string.Empty;
            SpendingLimit = string.Empty;
        }
    }
}
