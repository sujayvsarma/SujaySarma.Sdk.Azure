using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// A virtual network rule
    /// </summary>
    public class VirtualNetworkRule
    {

        /// <summary>
        /// Action to be taken
        /// </summary>
        [JsonProperty("action"), JsonConverter(typeof(StringEnumConverter))]
        public VirtualNetworkRuleAccessAction Action { get; set; }

        /// <summary>
        /// Id of the rule
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// State of rule
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(StringEnumConverter))]
        public VirtualNetworkRuleState State { get; set; }

    }
}
