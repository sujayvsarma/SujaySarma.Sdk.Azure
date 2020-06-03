using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Specifies information about a non-OS "data" disk attached to a VM
    /// </summary>
    public class VMDataDisk
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
        /// Size in gigabytes. Must be a maximum of 1023.
        /// </summary>
        [JsonProperty("diskSizeGB")]
        public int SizeGB { get; set; }

        /// <summary>
        /// The LUN of the adapter attached to.
        /// </summary>
        [JsonProperty("lun")]
        public int LogicalUnitNumber { get; set; }

        /// <summary>
        /// Name of the disk
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Describes if the disk is in the process of being detached
        /// </summary>
        [JsonProperty("toBeDetached")]
        public bool? IsBeingDetached { get; set; } = false;

        /// <summary>
        /// Describes if write-acceleration is enabled on the disk.
        /// </summary>
        [JsonProperty("writeAcceleratorEnabled")]
        public bool? IsWriteAccelerationEnabled { get; set; } = false;

        /// <summary>
        /// The source user image virtual hard disk. The virtual hard disk will be 
        /// copied before being attached to the virtual machine. If SourceImage is provided, 
        /// the destination virtual hard drive must not exist
        /// </summary>
        [JsonProperty("image")]
        public UriResource? CreateModeSourceImage { get; set; }

        /// <summary>
        /// The runtime Vhd file
        /// </summary>
        [JsonProperty("vhd")]
        public UriResource? VhdFile { get; set; }

        /// <summary>
        /// Reference to the managed disk
        /// </summary>
        [JsonProperty("managedDisk")]
        public VMManagedDisk? ManagedDisk { get; set; }

        public VMDataDisk() { }

        /// <summary>
        /// Create a data disk by copying an existing image
        /// </summary>
        /// <param name="name">Name of the disk</param>
        /// <param name="fromImage">URI to the Vhd's blob on a Storage Account</param>
        /// <param name="caching">Type of caching to enable</param>
        /// <param name="enableWriteAcceleration">Flag indicating whether to enable write acceleration on the disk</param>
        /// <param name="attachToLUN">The LUN to attach the disk to. Set to -1 to automatically pick a LUN (Azure will decide)</param>
        public static VMDataDisk FromImage(string name, ResourceUri fromImage, CachingTypeNamesEnum caching = CachingTypeNamesEnum.None, bool enableWriteAcceleration = false, int attachToLUN = -1)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if (attachToLUN < -1) { throw new ArgumentOutOfRangeException(nameof(attachToLUN)); }

            if ((fromImage == null) || (!fromImage.IsValid) ||
                (!fromImage.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!fromImage.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(fromImage));
            }

            VMDataDisk disk = new VMDataDisk()
            {
                Name = name,
                CreateUsing = DiskCreationOptionsEnum.FromImage,
                CreateModeSourceImage = new UriResource(fromImage.ToString()),
                Caching = caching,
                IsWriteAccelerationEnabled = enableWriteAcceleration
            };


            if (attachToLUN != -1)
            {
                disk.LogicalUnitNumber = attachToLUN;
            }

            return disk;
        }

        /// <summary>
        /// Create a data disk by attaching an unmanaged Vhd
        /// </summary>
        /// <param name="name">Name of the disk</param>
        /// <param name="unmanagedVhdFile">URI to the existing unmanaged Vhd on a Storage Account</param>
        /// <param name="caching">Type of caching to enable</param>
        /// <param name="enableWriteAcceleration">Flag indicating whether to enable write acceleration on the disk</param>
        /// <param name="attachToLUN">The LUN to attach the disk to. Set to -1 to automatically pick a LUN (Azure will decide)</param>
        public static VMDataDisk FromUmanagedVhd(string name, ResourceUri unmanagedVhdFile, CachingTypeNamesEnum caching = CachingTypeNamesEnum.None, bool enableWriteAcceleration = false, int attachToLUN = -1)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if (attachToLUN < -1) { throw new ArgumentOutOfRangeException(nameof(attachToLUN)); }

            if ((unmanagedVhdFile == null) || (!unmanagedVhdFile.IsValid) ||
                (!unmanagedVhdFile.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!unmanagedVhdFile.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(unmanagedVhdFile));
            }

            VMDataDisk disk = new VMDataDisk()
            {
                Name = name,
                CreateUsing = DiskCreationOptionsEnum.Copy,
                VhdFile = new UriResource(unmanagedVhdFile.ToString()),
                Caching = caching,
                IsWriteAccelerationEnabled = enableWriteAcceleration
            };

            if (attachToLUN != -1)
            {
                disk.LogicalUnitNumber = attachToLUN;
            }

            return disk;
        }

        /// <summary>
        /// Create a data disk by attaching an managed disk
        /// </summary>
        /// <param name="name">Name of the disk</param>
        /// <param name="managedDiskUri">URI to the existing unmanaged Vhd on a Storage Account</param>
        /// <param name="caching">Type of caching to enable</param>
        /// <param name="enableWriteAcceleration">Flag indicating whether to enable write acceleration on the disk</param>
        /// <param name="attachToLUN">The LUN to attach the disk to. Set to -1 to automatically pick a LUN (Azure will decide)</param>
        public static VMDataDisk FromManagedDisk(string name, ResourceUri managedDiskUri, DiskSkuNamesEnum typeOfDisk = DiskSkuNamesEnum.Standard_LRS, CachingTypeNamesEnum caching = CachingTypeNamesEnum.None, bool enableWriteAcceleration = false, int attachToLUN = -1)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new ArgumentNullException(nameof(name)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if (!Enum.IsDefined(typeof(DiskSkuNamesEnum), typeOfDisk)) { throw new ArgumentOutOfRangeException(nameof(typeOfDisk)); }
            if (attachToLUN < -1) { throw new ArgumentOutOfRangeException(nameof(attachToLUN)); }

            if ((managedDiskUri == null) || (!managedDiskUri.IsValid) ||
                (!managedDiskUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!managedDiskUri.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(managedDiskUri));
            }

            VMDataDisk disk = new VMDataDisk()
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

            if (attachToLUN != -1)
            {
                disk.LogicalUnitNumber = attachToLUN;
            }

            return disk;
        }

    }
}
