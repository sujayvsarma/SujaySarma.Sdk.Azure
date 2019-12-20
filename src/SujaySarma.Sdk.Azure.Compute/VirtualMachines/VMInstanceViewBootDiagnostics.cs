using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Runtime locations of console log and screenshots captured at VM bootup. Only available if 
    /// diagnostics is enabled and the VM restarted.
    /// </summary>
    public class VMInstanceViewBootDiagnostics
    {
        /// <summary>
        /// Uri to the blob containing the console screenshot during VM bootup
        /// </summary>
        [JsonProperty("consoleScreenshotBlobUri")]
        public string? ConsoleScreenshot { get; set; }


        /// <summary>
        /// Only for Linux VMs, Uri to the blob containing the logs sent to the console 
        /// during VM bootup
        /// </summary>
        [JsonProperty("serialConsoleLogBlobUri")]
        public string? ConsoleLog { get; set; }

        /// <summary>
        /// Only set if there were errors during bootup -- in which case the structure contains 
        /// details of the error(s) encountered.
        /// </summary>
        [JsonProperty("status")]
        public InstanceViewStatus? Status { get; set; }


        public VMInstanceViewBootDiagnostics() { }
    }
}
