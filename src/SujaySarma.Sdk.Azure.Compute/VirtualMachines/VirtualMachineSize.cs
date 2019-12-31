
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Size of a virtual machine. 
    /// This structure is created internally and used.
    /// </summary>
    public class VirtualMachineSize
    {
        /// <summary>
        /// Name of the size (eg: "Basic_A0")
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Maximum number of data disks allowed for this size
        /// </summary>
        [JsonProperty("maxDataDiskCount")]
        public int MaximumNumberOfDisks { get; set; } = 0;

        /// <summary>
        /// Maximum size of memory in MB
        /// </summary>
        [JsonProperty("memoryInMB")]
        public int MaximumMemoryInMB { get; set; } = 0;

        /// <summary>
        /// Number of cores in this size
        /// </summary>
        [JsonProperty("numberOfCores")]
        public int NumberOfCores { get; set; } = 0;

        /// <summary>
        /// Size of the OS disk in MB
        /// </summary>
        [JsonProperty("osDiskSizeInMB")]
        public int SizeOfOSDiskInMB { get; set; } = 0;

        /// <summary>
        /// Size of resource disk as allowed in by this size
        /// </summary>
        [JsonProperty("resourceDiskSizeInMB")]
        public int ResourceDiskSizeInMB { get; set; } = 0;


        public VirtualMachineSize() { }
    }
}
