using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Subscriptions
{

    /// <summary>
    /// Results of the subscription list operation
    /// </summary>
    public class SubscriptionListResult : ListContinuableResult<Subscription>
    {

        #region Properties

        /// <summary>
        /// Count of results
        /// </summary>
        [JsonProperty("count")]
        public Dictionary<string, object> Count { get; set; }

        #endregion


        public SubscriptionListResult() : base()
        {
            Count = new Dictionary<string, object>();
        }
    }
}
