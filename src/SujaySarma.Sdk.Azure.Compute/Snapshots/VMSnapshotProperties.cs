using System;

using Newtonsoft.Json;
using SujaySarma.Sdk.Azure.Compute.Common;
using SujaySarma.Sdk.Azure.Compute.Disks;
using SujaySarma.Sdk.Azure.Compute.Encryption;

namespace SujaySarma.Sdk.Azure.Compute.Snapshots
{
    /// <summary>
    /// Properties of the snapshot
    /// </summary>
    public class VMSnapshotProperties
    {

        /// <summary>
        /// Metadata to aid creation of disk. Cannot be changed once created
        /// </summary>
        [JsonProperty("creationData")]
        public DiskCreationMetadata CreationMetadata { get; set; } = new DiskCreationMetadata();

        /// <summary>
        /// Size of disk in bytes
        /// </summary>
        [JsonProperty("diskSizeBytes")]
        public int DiskSizeBytes { get; private set; } = 0;

        /// <summary>
        /// Size of the disk in GB units. Max is 1023 (1023 GB). Mandatory. Can only be increased, and 
        /// resize can be done only when disk is unattached (or VM is stopped-deallocated)
        /// </summary>
        [JsonProperty("diskSizeGB")]
        public int DiskSizeGB { get; set; } = 0;

        /// <summary>
        /// Options to encrypt the contents/data in the disk, at rest, using user/platform-provided keys
        /// </summary>
        [JsonProperty("encryption")]
        public DiskDataEncryptionProperties? ContentEncryptionProperties { get; set; }

        /// <summary>
        /// Options to encrypt the disk itself using Azure Disk Encryption and can contain multiple 
        /// settings per disk/snapshot
        /// </summary>
        [JsonProperty("encryptionSettingsCollection")]
        public AzureDiskEncryptionSettings? AzureDiskEncryptionSettings { get; set; } = null;

        /// <summary>
        /// Hyper V generation of the disk. Applies only to OS disks.
        /// </summary>
        [JsonProperty("hyperVGeneration", ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public HyperVGenerationNamesEnum? HyperVGeneration { get; set; } = null;

        /// <summary>
        /// Flag, Incremental snapshots occupy less space and can be diff'ed.
        /// </summary>
        [JsonProperty("incremental")]
        public bool IsIncrementalSnapshot { get; set; } = true;

        /// <summary>
        /// Type of operating system
        /// </summary>
        [JsonProperty("osType", ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public OSTypeNamesEnum? OperatingSystem { get; set; } = null;

        /// <summary>
        /// Disk provisioning status
        /// </summary>
        [JsonProperty("provisioningState")]
        public string ProvisioningStatus { get; set; } = "Succeeded";

        /// <summary>
        /// Date/time the disk was created. In UTC.
        /// </summary>
        [JsonProperty("timeCreated")]
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;

        /// <summary>
        /// A unique ID identifying the disk. Seems to be optionally present, so check for NULL!
        /// </summary>
        [JsonProperty("uniqueId")]
        public string? UniqueID { get; set; } = null;


        public VMSnapshotProperties() { }

    }
}
