using Newtonsoft.Json;
using SujaySarma.Sdk.Azure.Common;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Certificates uploaded into a VM. Each set can be 
    /// from a different KeyVault store.
    /// </summary>
    public class VMCertificates
    {
        /// <summary>
        /// Reference to the vault the secret is stored in
        /// </summary>
        [JsonProperty("sourceVault")]
        public SubResource Vault { get; set; } = new SubResource();

        /// <summary>
        /// Certificates from the Vault.
        /// </summary>
        [JsonProperty("vaultCertificates")]
        public List<VMCertificateStoreCertificate>? Certificates { get; set; } = new List<VMCertificateStoreCertificate>();


        public VMCertificates() { }
    }
}
