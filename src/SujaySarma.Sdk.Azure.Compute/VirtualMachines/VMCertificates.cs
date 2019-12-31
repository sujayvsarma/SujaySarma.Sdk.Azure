using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;
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
        /// Certificates from the Vault. You cannot actually add certificates here. All the certificates in the specified KeyVault 
        /// will be injected into the VM.
        /// </summary>
        [JsonProperty("vaultCertificates")]
        public List<VMCertificateStoreCertificate>? Certificates { get; set; } = new List<VMCertificateStoreCertificate>();


        public VMCertificates() { }

        /// <summary>
        /// Initialize the VM certificate store
        /// </summary>
        /// <param name="keyVault">Resource Uri to the Azure KeyVault store containing the secrets to inject into the VM</param>
        public VMCertificates(ResourceUri keyVault)
        {
            if ((keyVault == null) || (!keyVault.IsValid) || (!keyVault.Is(ResourceUriCompareLevel.Provider, "Microsoft.KeyVault")) ||
                (!keyVault.Is(ResourceUriCompareLevel.Type, "vaults")))
            {
                throw new ArgumentException(nameof(keyVault));
            }

            Vault = new SubResource(keyVault);
            Certificates = new List<VMCertificateStoreCertificate>();
        }
    }
}
