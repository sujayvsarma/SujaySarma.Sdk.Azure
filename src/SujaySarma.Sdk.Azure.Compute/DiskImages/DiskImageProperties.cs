using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

namespace SujaySarma.Sdk.Azure.Compute.DiskImages
{
    /// <summary>
    /// Properties of a disk image
    /// </summary>
    public class DiskImageProperties
    {
        /// <summary>
        /// The HyperV generation of the VM created from the image
        /// </summary>
        [JsonProperty("hyperVGeneration")]
        public HyperVGenerationNamesEnum HyperVGeneration { get; set; }

        /// <summary>
        /// State of provisioning
        /// </summary>
        [JsonProperty("provisioningState")]
        public string ProvisioningState { get; set; } = "Succeeded";

        /// <summary>
        /// URI to the VM from which this image was created
        /// </summary>
        [JsonProperty("sourceVirtualMachine")]
        public SubResource SourceVirtualMachine { get; set; } = new SubResource();

        /// <summary>
        /// Storage settings for the image
        /// </summary>
        [JsonProperty("storageProfile")]
        public ImageStorageProfile StorageProfile { get; set; } = new ImageStorageProfile();


        public DiskImageProperties() { }
    }
}
