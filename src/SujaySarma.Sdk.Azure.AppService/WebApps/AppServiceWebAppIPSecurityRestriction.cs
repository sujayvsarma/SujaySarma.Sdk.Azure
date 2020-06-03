using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// IP security restrictions for an Azure App Service app
    /// </summary>
    public class AppServiceWebAppIPSecurityRestriction
    {
        /// <summary>
        /// "Allow" or "Deny" action to take
        /// </summary>
        [JsonProperty("action")]
        public string AllowOrDeny { get; set; } = "Deny";

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// IP Address to apply the restriction on. Can be a single 4-dotted IPv4 address, 
        /// or a CIDR notation. For single IP address formats, the SubnetMask property is required as well.
        /// </summary>
        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// Name of the rule
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Rule priority
        /// </summary>
        [JsonProperty("priority")]
        public int Priority { get; set; }

        /// <summary>
        /// Subnet Mask of the IP address. Required if IpAddress is set to a 
        /// single IP address (in 4-dotted format)
        /// </summary>
        [JsonProperty("subnetMask")]
        public string? SubnetMask { get; set; }

        /// <summary>
        /// An internal tag for the subnet traffic
        /// </summary>
        /// <remarks>This is documented in the API, but may not be returned ever!</remarks>
        [JsonProperty("subnetTrafficTag")]
        public string? InternalSubnetTrafficTag { get; private set; }

        /// <summary>
        /// The type of filter that is going to be applied
        /// </summary>
        [JsonProperty("tag"), JsonConverter(typeof(StringEnumConverter))]
        public IpSecurityRestrictionFilterType FilterType { get; set; }

        /// <summary>
        /// Resource Id of the virtual network's subnet
        /// </summary>
        [JsonProperty("vnetSubnetResourceId")]
        public string VirtualNetworkResourceId { get; set; } = string.Empty;

        /// <summary>
        /// An internal tag for traffic on the Vnet
        /// </summary>
        /// <remarks>This is documented in the API, but may not be returned ever!</remarks>
        [JsonProperty("vnetTrafficTag")]
        public string? InternalVnetTrafficTag { get; private set; }
    }
}
