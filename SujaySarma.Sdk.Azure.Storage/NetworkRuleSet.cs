
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Set of firewall rules to be applied on a storage account
    /// </summary>
    public class NetworkRuleSet
    {
        /// <summary>
        /// Specifies whether traffic from one or more Azure systems are bypass this rule. 
        /// Is a combination of comma-seperated string values, from: Logging, Metrics and AzureServices
        /// </summary>
        [JsonProperty("bypass")]
        public string BypassAction { get; set; } = "None";

        /// <summary>
        /// Default action to take
        /// </summary>
        [JsonProperty("defaultAction"), JsonConverter(typeof(StringEnumConverter))]
        public RuleAction DefaultAction { get; set; }

        /// <summary>
        /// Collection of IP rules
        /// </summary>
        [JsonProperty("ipRules")]
        public List<IPRule> Rules { get; set; } = new List<IPRule>();

        /// <summary>
        /// Collection of vNet rules
        /// </summary>
        [JsonProperty("virtualNetworkRules")]
        public List<VirtualNetworkRule> VNetRules { get; set; } = new List<VirtualNetworkRule>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public NetworkRuleSet() { }
    }
}
