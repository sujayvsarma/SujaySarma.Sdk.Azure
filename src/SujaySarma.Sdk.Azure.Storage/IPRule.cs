
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// A firewall rule to allow IP address ranges
    /// </summary>
    public class IPRule
    {
        /// <summary>
        /// Action for an IP ACL rule
        /// </summary>
        [JsonProperty("action"), JsonConverter(typeof(StringEnumConverter))]
        public VirtualNetworkRuleAccessAction Action { get; set; }

        /// <summary>
        /// A single IPv4 address or a range in CIDR format
        /// </summary>
        [JsonProperty("value")]
        public string IPv4AddressRange { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public IPRule() { }
    }
}
