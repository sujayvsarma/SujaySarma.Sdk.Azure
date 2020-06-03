using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Listener for WinRM
    /// </summary>
    public class WinRMListener
    {
        /// <summary>
        /// Uri to the authorization certificate (uploaded to KeyVault as a secret)
        /// </summary>
        [JsonProperty("certificateUrl")]
        public string? CertificateURI { get; set; } = null;

        /// <summary>
        /// Protocol to be used
        /// </summary>
        [JsonProperty("protocol"), JsonConverter(typeof(StringEnumConverter))]
        public WinRMProtocolsEnum Protocol { get; set; } = WinRMProtocolsEnum.https;


        public WinRMListener() { }
    }
}
