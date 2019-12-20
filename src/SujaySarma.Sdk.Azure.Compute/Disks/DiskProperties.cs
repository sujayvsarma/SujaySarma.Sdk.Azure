using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using SujaySarma.Sdk.Azure.Compute.Common;
using SujaySarma.Sdk.Azure.Compute.Encryption;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    /// <summary>
    /// Properties of a disk
    /// </summary>
    public class DiskProperties
    {
        [JsonProperty("creationData")]
        public DiskCreationMetadata? CreationMetadata { get; set; } = null;

        /// <summary>
        /// Number of IOPS allowed for this disk. Only settable for UltraSSD disks. 
        /// Range is between: 4k and 256k.
        /// </summary>
        [JsonProperty("diskIOPSReadWrite")]
        public int ReadWriteIOPS { get; set; } = 0;

        /// <summary>
        /// Bandwidth allowed for this disk. Only settable for UltraSSD disks. 
        /// MBps = millions of bytes/sec. ISO notation, in powers of 10.
        /// </summary>
        [JsonProperty("diskMBpsReadWrite")]
        public int ReadWriteMBps { get; set; } = 0;

        /// <summary>
        /// Size of disk in bytes. Cannot be set.
        /// </summary>
        [JsonProperty("diskSizeBytes")]
        public int SizeInBytes { get; private set; } = 0;

        /// <summary>
        /// Mandatory for create. If present & changed in a disk-update, then the size is changed, 
        /// but only if disk is not attached to a running VM. Size can only be increased.
        /// </summary>
        [JsonProperty("diskSizeGB")]
        public int SizeInGB { get; set; } = 0;

        /// <summary>
        /// State of a disk
        /// </summary>
        [JsonProperty("diskState", ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DiskStateNamesEnum Status { get; set; } = DiskStateNamesEnum.Unattached;

        /// <summary>
        /// Options to encrypt the contents/data in the disk, at rest, using user/platform-provided keys
        /// </summary>
        [JsonProperty("encryption")]
        public DiskDataEncryptionProperties? ContentEncryptionProperties { get; set; } = null;

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

        /// <summary>
        /// Logical zones list for the disk. Seems to be optionally present, so check for NULL!
        /// </summary>
        [JsonProperty("zones")]
        public List<string>? Zones { get; set; } = null;

        public DiskProperties() { }
    }
}
