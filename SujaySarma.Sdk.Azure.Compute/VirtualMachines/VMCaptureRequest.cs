using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Request sent to capture a VM into a template. 
    /// This structure is created internally and used.
    /// </summary>
    internal class VMCaptureRequest
    {
        /// <summary>
        /// Name of the blob storage container to copy the captured template into
        /// </summary>
        [JsonProperty("destinationContainerName")]
        public string DestinationContainerName { get; set; } = string.Empty;

        /// <summary>
        /// If set, if there is a naming conflict, destination Vhd files with 
        /// the same names are overwritten.
        /// </summary>
        [JsonProperty("overwriteVhds")]
        public bool ShouldOverwriteVhds { get; set; } = true;

        /// <summary>
        /// A prefix to prepend to the captured Vhd file names
        /// </summary>
        [JsonProperty("vhdPrefix")]
        public string VhdFileNamePrefix { get; set; } = string.Empty;


        public VMCaptureRequest() { }
    }
}
