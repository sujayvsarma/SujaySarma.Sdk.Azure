using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Settings for boot-up logging and diagnostics
    /// </summary>
    public class VMBootDiagnostics
    {
        /// <summary>
        /// If this is/should be enabled
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// Uri to a Azure Storage Blob account where the diagnostics information 
        /// should be stored.
        /// </summary>
        [JsonProperty("storageUri")]
        public string? StorageUri { get; set; } = null;
    }
}
