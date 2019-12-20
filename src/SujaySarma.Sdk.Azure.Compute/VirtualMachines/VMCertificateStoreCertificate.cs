using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// A single certificate injected into a VM
    /// </summary>
    public class VMCertificateStoreCertificate
    {
        /// <summary>
        /// The certificate store to inject into. On Windows, this is in the LocalMachine account. 
        /// For Linux, certificates are stored as files in the /var/lib/waagent directory.
        /// </summary>
        [JsonProperty("certificateStore")]
        public string StoreName { get; set; } = "LocalMachine";

        /// <summary>
        /// Uri to the certificate that was injected. 
        /// </summary>
        [JsonProperty("certificateUrl")]
        public string CertificateUrl { get; set; } = string.Empty;


        public VMCertificateStoreCertificate() { }
    }
}
