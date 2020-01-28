using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// Response object element in many Usages API.
    /// </summary>
    /// <remarks>
    ///     The official object is structured VERY strangely. We restructure the object in our 
    ///     Client class, so this class is never returned to the caller.
    /// </remarks>
    public class UsageAPIResponseItem
    {
        /// <summary>
        /// Unit of measurement
        /// </summary>
        [JsonProperty("unit")]
        public string UnitName { get; set; } = string.Empty;

        /// <summary>
        /// Date/time the counter resets
        /// </summary>
        [JsonProperty("nextResetTime")]
        public DateTime NextResetAt { get; set; }

        /// <summary>
        /// Current value of this metric
        /// </summary>
        [JsonProperty("currentValue")]
        public int Value { get; set; }

        /// <summary>
        /// Maximum limit this metric can be (if limited, otherwise is -1)
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; } = -1;

        /// <summary>
        /// Name of the counter
        /// </summary>
        [JsonProperty("name")]
        public LocalizedStringValue Name { get; set; } = new LocalizedStringValue();


        /// <summary>
        /// Default constructor
        /// </summary>
        public UsageAPIResponseItem() { }
    }
}
