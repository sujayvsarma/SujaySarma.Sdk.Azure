using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;

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
        [JsonProperty("caching"), JsonConverter(typeof(StringEnumConverter))]
        public CachingTypeNamesEnum Caching { get; set; } = CachingTypeNamesEnum.None;

        /// <summary>
        /// Must be either Attach or FromImage. Other values are not allowed here.
        /// </summary>
        [JsonProperty("createOption"), JsonConverter(typeof(StringEnumConverter))]
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
        [JsonProperty("osType"), JsonConverter(typeof(StringEnumConverter))]
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

        /// <summary>
        /// Create a data disk by copying an existing image
        /// </summary>
        /// <param name="name">Name of the disk</param>
        /// <param name="fromImage">URI to the Vhd's blob on a Storage Account</param>
        /// <param name="caching">Type of caching to enable</param>
        /// <param name="enableWriteAcceleration">Flag indicating whether to enable write acceleration on the disk</param>
        public static VMOSDisk FromImage(string name, ResourceUri fromImage, CachingTypeNamesEnum caching = CachingTypeNamesEnum.None, bool enableWriteAcceleration = false)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }

            if ((fromImage == null) || (!fromImage.IsValid) ||
                (!fromImage.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!fromImage.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(fromImage));
            }

            VMOSDisk disk = new VMOSDisk()
            {
                Name = name,
                CreateUsing = DiskCreationOptionsEnum.FromImage,
                CreateModeSourceImage = new UriResource(fromImage.ToString()),
                Caching = caching,
                IsWriteAccelerationEnabled = enableWriteAcceleration
            };

            return disk;
        }

        /// <summary>
        /// Create a data disk by attaching an unmanaged Vhd
        /// </summary>
        /// <param name="name">Name of the disk</param>
        /// <param name="unmanagedVhdFile">URI to the existing unmanaged Vhd on a Storage Account</param>
        /// <param name="caching">Type of caching to enable</param>
        /// <param name="enableWriteAcceleration">Flag indicating whether to enable write acceleration on the disk</param>
        public static VMOSDisk FromUmanagedVhd(string name, ResourceUri unmanagedVhdFile, CachingTypeNamesEnum caching = CachingTypeNamesEnum.None, bool enableWriteAcceleration = false)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }

            if ((unmanagedVhdFile == null) || (!unmanagedVhdFile.IsValid) ||
                (!unmanagedVhdFile.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!unmanagedVhdFile.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(unmanagedVhdFile));
            }

            VMOSDisk disk = new VMOSDisk()
            {
                Name = name,
                CreateUsing = DiskCreationOptionsEnum.Copy,
                VhdFile = new UriResource(unmanagedVhdFile.ToString()),
                Caching = caching,
                IsWriteAccelerationEnabled = enableWriteAcceleration
            };

            return disk;
        }

        /// <summary>
        /// Create a data disk by attaching an managed disk
        /// </summary>
        /// <param name="name">Name of the disk</param>
        /// <param name="managedDiskUri">URI to the existing unmanaged Vhd on a Storage Account</param>
        /// <param name="caching">Type of caching to enable</param>
        /// <param name="enableWriteAcceleration">Flag indicating whether to enable write acceleration on the disk</param>
        public static VMOSDisk FromManagedDisk(string name, ResourceUri managedDiskUri, DiskSkuNamesEnum typeOfDisk = DiskSkuNamesEnum.Standard_LRS, CachingTypeNamesEnum caching = CachingTypeNamesEnum.None, bool enableWriteAcceleration = false)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if (!Enum.IsDefined(typeof(DiskSkuNamesEnum), typeOfDisk)) { throw new ArgumentOutOfRangeException(nameof(typeOfDisk)); }

            if ((managedDiskUri == null) || (!managedDiskUri.IsValid) ||
                (!managedDiskUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!managedDiskUri.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(managedDiskUri));
            }

            VMOSDisk disk = new VMOSDisk()
            {
                Name = name,
                CreateUsing = DiskCreationOptionsEnum.Attach,
                ManagedDisk = new VMManagedDisk()
                {
                    Id = managedDiskUri.ToString(),
                    Type = typeOfDisk
                },
                Caching = caching,
                IsWriteAccelerationEnabled = enableWriteAcceleration
            };

            return disk;
        }

    }
}
