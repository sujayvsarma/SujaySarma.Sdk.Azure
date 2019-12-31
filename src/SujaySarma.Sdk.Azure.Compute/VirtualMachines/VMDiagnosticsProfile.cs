using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Settings for in-VM diagnostics
    /// </summary>
    public class VMDiagnosticsProfile
    {

        /// <summary>
        /// Boot-time diagnostics
        /// </summary>
        [JsonProperty("bootDiagnostics")]
        public VMBootDiagnostics BootDiagnostics { get; set; } = new VMBootDiagnostics();

        public VMDiagnosticsProfile() { }

        /// <summary>
        /// Enable boot diagnostics
        /// </summary>
        /// <param name="storageAccountBlobUri">Uri to a Azure Storage Blob account where the diagnostics information should be stored.</param>
        public VMDiagnosticsProfile(ResourceUri storageAccountBlobUri) => BootDiagnostics = new VMBootDiagnostics(storageAccountBlobUri);
    }
}
