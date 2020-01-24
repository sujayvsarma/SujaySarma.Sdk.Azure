using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Subscriptions
{
    /// <summary>
    /// The subscription item. This is purely a result-data structure and there are no code flows 
    /// that require the caller to generate this data.
    /// </summary>
    public class Subscription
    {
        #region Properties

        /// <summary>
        /// The absolute Id of the subscription
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; }

        /// <summary>
        /// The source of authorization
        /// </summary>
        [JsonProperty("authorizationSource")]
        public string AuthorizationSource { get; set; }

        /// <summary>
        /// The subscription Id of the subscription
        /// </summary>
        [JsonProperty("subscriptionId")]
        public Guid Id { get; set; }

        /// <summary>
        /// The tenant Id of the subscription
        /// </summary>
        [JsonProperty("tenantId")]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Display name of the subscription
        /// </summary>
        [JsonProperty("displayName")]
        public string Name { get; set; }

        /// <summary>
        /// State of the subscription
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Tenants that can manage this subscription. The key is always "tenantId".
        /// </summary>
        [JsonProperty("managedByTenants")]
        public KeyValuePair<string, Guid>? ManagedByTenants { get; set; }

        /// <summary>
        /// Policies applied to the subscription
        /// </summary>
        [JsonProperty("subscriptionPolicies")]
        public SubscriptionPolicy Policy { get; set; }

        #endregion

        public Subscription()
        {
            ResourceId = string.Empty;
            AuthorizationSource = string.Empty;
            Id = Guid.Empty;
            TenantId = Guid.Empty;
            Name = string.Empty;
            State = string.Empty;
            ManagedByTenants = new KeyValuePair<string, Guid>();
            Policy = new SubscriptionPolicy();
        }
    }
}
