using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Subscriptions
{

    /// <summary>
    /// A geographic location that objects created within a subscription may be 
    /// placed under. This is purely a result-data structure and there are no code flows 
    /// that require the caller to generate this data.
    /// </summary>
    public class SubscriptionLocation
    {

        #region Properties

        /// <summary>
        /// The URI ID of the location
        /// </summary>
        [JsonProperty("id")]
        public string LocationId { get; set; }

        /// <summary>
        /// Internal name of the location
        /// </summary>
        [JsonProperty("name")]
        public string InternalName { get; set; }

        /// <summary>
        /// The user-friendly display name of the location
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Longitude coordinates
        /// </summary>
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude coordinates
        /// </summary>
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Id of the subscription
        /// </summary>
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        #endregion


        public SubscriptionLocation()
        {
            LocationId = "/subscriptions/00000000-0000-0000-0000-000000000000/locations/earthscore";
            SubscriptionId = "00000000-0000-0000-0000-000000000000";
            InternalName = "EarthsCore";
            DisplayName = "Earth's Core";
            Longitude = 0.000;
            Latitude = 0.000;
        }

    }
}
