using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Initialize a new set of properties
        /// </summary>
        /// <param name="generation">The HyperV generation of the VM created from the image</param>
        /// <param name="sourceVM">URI to the VM from which this image was created</param>
        /// <param name="primaryDisk">Properties of the primary or OS disk</param>
        /// <param name="isZoneResilient">Flag indicating if the storage or disk image is zone-resilient</param>
        /// <param name="dataDisks">Collection of data disks in the image</param>
        public DiskImageProperties(HyperVGenerationNamesEnum generation, ResourceUri sourceVM, ImageOSDisk primaryDisk, bool isZoneResilient = false, IEnumerable<ImageDataDisk>? dataDisks = null)
        {
            if (!Enum.IsDefined(typeof(HyperVGenerationNamesEnum), generation)) { throw new ArgumentOutOfRangeException(nameof(generation)); }
            if ((sourceVM == null) || (!sourceVM.IsValid)) { throw new ArgumentException(nameof(sourceVM)); }

            HyperVGeneration = generation;
            SourceVirtualMachine = new SubResource(sourceVM);
            StorageProfile = new ImageStorageProfile(primaryDisk, isZoneResilient, dataDisks);
        }

        /// <summary>
        /// Set Hyper V generation
        /// </summary>
        /// <param name="generation">Hyper V generation</param>
        /// <returns>DiskImageProperties</returns>
        public DiskImageProperties WithGeneration(HyperVGenerationNamesEnum generation)
        {
            HyperVGeneration = generation;
            return this;
        }

        /// <summary>
        /// Set source VM
        /// </summary>
        /// <param name="virtualMachine">Source VM instance</param>
        /// <returns>DiskImageProperties</returns>
        public DiskImageProperties WithVirtualMachine(VirtualMachines.VirtualMachine virtualMachine)
        {
            SourceVirtualMachine = new SubResource(virtualMachine.ResourceId);
            return this;
        }

        /// <summary>
        /// Set source VM
        /// </summary>
        /// <param name="virtualMachine">Source VM resource Uri</param>
        /// <returns>DiskImageProperties</returns>
        public DiskImageProperties WithVirtualMachine(ResourceUri virtualMachine)
        {
            SourceVirtualMachine = new SubResource(virtualMachine);
            return this;
        }
    }
}
