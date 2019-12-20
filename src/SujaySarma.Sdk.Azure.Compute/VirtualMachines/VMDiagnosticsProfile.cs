using Newtonsoft.Json;

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
    }
}
