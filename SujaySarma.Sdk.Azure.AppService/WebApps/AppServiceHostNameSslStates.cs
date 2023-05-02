using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// SSL state for a hostname added to an app service
    /// </summary>
    public class AppServiceHostNameSslStates
    {
        /// <summary>
        /// Type of host
        /// </summary>
        [JsonProperty("hostType"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceWebAppHostTypeEnum HostType { get; set; } = AppServiceWebAppHostTypeEnum.Default;

        /// <summary>
        /// Hostname of this host
        /// </summary>
        [JsonProperty("name")]
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// Status of SSL on this host
        /// </summary>
        [JsonProperty("sslState"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceWebAppSslTypeEnum State { get; set; } = AppServiceWebAppSslTypeEnum.Default;

        /// <summary>
        /// Thumbprint of the SSL Certificate
        /// </summary>
        [JsonProperty("thumbprint")]
        public string? SslCertificateThumbprint { get; set; } = null;

        /// <summary>
        /// If set to TRUE during an UPDATE operation, will cause this configuration to be updated 
        /// at Azure-end
        /// </summary>
        [JsonProperty("toUpdate")]
        public bool MustUpdateConfiguration { get; set; } = false;

        /// <summary>
        /// The virtual IP address assigned if IP-based SSL was enabled
        /// </summary>
        [JsonProperty("virtualIP")]
        public string? IpBasedSslVirtualIpAddress { get; set; } = null;


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceHostNameSslStates() { }
    }
}
