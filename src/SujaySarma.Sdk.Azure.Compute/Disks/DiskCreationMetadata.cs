
using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    /// <summary>
    /// Metadata about the creation of the disk
    /// </summary>
    public class DiskCreationMetadata
    {
        /// <summary>
        /// Source of creation of the disk
        /// </summary>
        [JsonProperty("createOption")]
        public DiskCreationOptionsEnum CreationMode { get; set; } = DiskCreationOptionsEnum.Empty;

        /// <summary>
        /// If createOption is Copy, this is the ARM id of the source snapshot or disk.
        /// </summary>
        [JsonProperty("sourceResourceId")]
        public string? SourceSnapshotOrDiskResourceId { get; set; } = null;

        /// <summary>
        /// If this field is set, this is the unique id identifying the source of this resource.
        /// </summary>
        [JsonProperty("sourceUniqueId")]
        public string? SourceUniqueId { get; set; } = null;

        /// <summary>
        /// If createOption is Import, this is the URI of a blob to be imported into a managed disk.
        /// </summary>
        [JsonProperty("sourceUri")]
        public string? SourceBlobUri { get; set; } = null;

        /// <summary>
        /// Required if createOption is Import. The Azure Resource Manager identifier of the 
        /// storage account containing the blob to import as a disk.
        /// </summary>
        [JsonProperty("storageAccountId")]
        public string? SourceBlobStorageAccountId { get; set; } = null;

        /// <summary>
        /// If createOption is Upload, this is the size of the contents of the upload including the VHD footer. 
        /// This value should be between 20972032 (20 MiB + 512 bytes for the VHD footer) and 35183298347520 bytes 
        /// (32 TiB + 512 bytes for the VHD footer).
        /// </summary>
        [JsonProperty("uploadSizeBytes")]
        public long? VhdUploadSizeInBytes { get; set; } = null;

        /// <summary>
        /// Disk source information.
        /// </summary>
        [JsonProperty("imageReference")]
        public SubResource? SourceDiskImageReference { get; set; } = null;


        public DiskCreationMetadata() { }

        /// <summary>
        /// Create a disk by copying from a VM snapshot
        /// </summary>
        /// <param name="sourceSnapshotUri">ResourceUri to the VM's snapshot that is to be copied as a new disk</param>
        /// <returns>Disk creation metadata</returns>
        public static DiskCreationMetadata ViaCopy(ResourceUri sourceSnapshotUri)
        {
            if ((sourceSnapshotUri == null) || (!sourceSnapshotUri.IsValid) ||
                (!sourceSnapshotUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Compute")) || (!sourceSnapshotUri.Is(ResourceUriCompareLevel.Type, "snapshots")))
            {
                throw new ArgumentException(nameof(sourceSnapshotUri));
            }

            return new DiskCreationMetadata()
            {
                CreationMode = DiskCreationOptionsEnum.Copy,
                SourceSnapshotOrDiskResourceId = sourceSnapshotUri.ToString()
            };
        }


        /// <summary>
        /// Create a disk by importing an unmanaged disk's blob
        /// </summary>
        /// <param name="storageAccountUri">ResourceUri to the storage account containing the umanaged blob</param>
        /// <param name="vhdUri">Absolute Uri to the .vhd file</param>
        /// <returns>Disk creation metadata</returns>
        public static DiskCreationMetadata ViaImportUnmanagedDiskBlob(ResourceUri storageAccountUri, string vhdUri)
        {
            if ((storageAccountUri == null) || (!storageAccountUri.IsValid) ||
                (!storageAccountUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!storageAccountUri.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(storageAccountUri));
            }

            if ((!Uri.IsWellFormedUriString(vhdUri, UriKind.Absolute)) || (!vhdUri.EndsWith(".vhd", StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentException(nameof(vhdUri));
            }

            return new DiskCreationMetadata()
            {
                CreationMode = DiskCreationOptionsEnum.Import,
                SourceBlobStorageAccountId = storageAccountUri.ToString(),
                SourceBlobUri = vhdUri
            };
        }

        /// <summary>
        /// Create a disk by importing a managed disk file
        /// </summary>
        /// <param name="managedDiskUri">ResourceUri to the managed disk</param>
        /// <returns>Disk creation metadata</returns>
        public static DiskCreationMetadata ViaImportManagedDisk(ResourceUri managedDiskUri)
        {
            if ((managedDiskUri == null) || (!managedDiskUri.IsValid) ||
                (!managedDiskUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Compute")) || (!managedDiskUri.Is(ResourceUriCompareLevel.Type, "disks")))
            {
                throw new ArgumentException(nameof(managedDiskUri));
            }

            return new DiskCreationMetadata()
            {
                CreationMode = DiskCreationOptionsEnum.Copy,
                SourceSnapshotOrDiskResourceId = managedDiskUri.ToString()
            };
        }


        /// <summary>
        /// Create an image from a marketplace image
        /// </summary>
        /// <param name="osType">Type of OS we are provisioning for</param>
        /// <param name="marketplaceImageResourceUri">ResourceUri to the marketplace image</param>
        /// <returns>Disk creation metadata</returns>
        public static DiskCreationMetadata ViaPlatformImage(OSTypeNamesEnum osType, ResourceUri marketplaceImageResourceUri)
        {
            if (!Enum.IsDefined(typeof(OSTypeNamesEnum), osType)) { throw new ArgumentOutOfRangeException(nameof(osType)); }
            if ((marketplaceImageResourceUri == null) || (!marketplaceImageResourceUri.IsValid))
            {
                throw new ArgumentException(nameof(marketplaceImageResourceUri));
            }

            return new DiskCreationMetadata()
            {
                CreationMode = DiskCreationOptionsEnum.FromImage,
                SourceDiskImageReference = new SubResource()
                {
                    ResourceId = marketplaceImageResourceUri.ToString()
                }
            };
        }

        /// <summary>
        /// Create a new disk using the file-upload method. After a disk is created using method successfully, you must upload the 
        /// actual VHD file to Azure separately!
        /// </summary>
        /// <param name="diskSizeInBytes">Size of disk to be uploaded, in bytes</param>
        /// <returns>Disk creation metadata</returns>
        public static DiskCreationMetadata ViaUpload(long diskSizeInBytes)
        {
            if ((diskSizeInBytes < 20972032) || (diskSizeInBytes > 35183298347520))
            {
                throw new ArgumentOutOfRangeException(nameof(diskSizeInBytes));
            }

            return new DiskCreationMetadata()
            {
                CreationMode = DiskCreationOptionsEnum.Upload,
                VhdUploadSizeInBytes = diskSizeInBytes
            };
        }
    }
}
