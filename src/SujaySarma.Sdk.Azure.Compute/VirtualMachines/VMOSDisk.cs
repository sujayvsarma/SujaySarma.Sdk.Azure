using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Specifies information about the OS disk attached to a VM
    /// </summary>
    public class VMOSDisk
    {
        /// <summary>
        /// Caching
        /// </summary>
        [JsonProperty("caching", ItemConverterType = typeof(StringEnumConverter))]
        public CachingTypeNamesEnum Caching { get; set; } = CachingTypeNamesEnum.None;

        /// <summary>
        /// Must be either Attach or FromImage. Other values are not allowed here.
        /// </summary>
        [JsonProperty("createOption", ItemConverterType = typeof(StringEnumConverter))]
        public DiskCreationOptionsEnum CreateUsing { get; set; } = DiskCreationOptionsEnum.Empty;

        /// <summary>
        /// Settings for the ephemeral disk used for the OS disk. Can only be "Local".
        /// </summary>
        [JsonProperty("diffDiskSettings")]
        public string? DifferentialDiskSettings { get; set; } = "Local";

        /// <summary>
        /// Size in gigabytes. Must be a maximum of 1023.
        /// </summary>
        [JsonProperty("diskSizeGB")]
        public int SizeGB { get; set; }

        /// <summary>
        /// Encryption settings for the OS disk
        /// </summary>
        [JsonProperty("encryptionSettings")]
        public VMDiskEncryptionSettings? Encryption { get; set; }

        /// <summary>
        /// The source user image virtual hard disk. The virtual hard disk will be 
        /// copied before being attached to the virtual machine. If SourceImage is provided, 
        /// the destination virtual hard drive must not exist
        /// </summary>
        [JsonProperty("image")]
        public UriResource? CreateModeSourceImage { get; set; }

        /// <summary>
        /// Reference to the managed disk
        /// </summary>
        [JsonProperty("managedDisk")]
        public VMManagedDisk? ManagedDisk { get; set; }

        /// <summary>
        /// Name of the disk
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The OS that is loaded
        /// </summary>
        [JsonProperty("osType", ItemConverterType = typeof(StringEnumConverter))]
        public OSTypeNamesEnum OSName { get; set; } = OSTypeNamesEnum.Windows;

        /// <summary>
        /// The runtime Vhd file
        /// </summary>
        [JsonProperty("vhd")]
        public UriResource? VhdFile { get; set; }

        /// <summary>
        /// Describes if write-acceleration is enabled on the disk.
        /// </summary>
        [JsonProperty("writeAcceleratorEnabled")]
        public bool? IsWriteAccelerationEnabled { get; set; } = false;


        public VMOSDisk() { }
    }
}
