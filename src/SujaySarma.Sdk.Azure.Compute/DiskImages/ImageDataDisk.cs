using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.DiskImages
{
    /// <summary>
    /// A Data disk (disks other than the primary disk with the OS) that are in the image
    /// </summary>
    public class ImageDataDisk
    {
        /// <summary>
        /// Uri to the VHD that is stored as a blob in an Azure Storage account
        /// </summary>
        [JsonProperty("blobUri")]
        public string? VhdStoredOnBlobUri { get; set; } = null;

        /// <summary>
        /// Type of caching
        /// </summary>
        [JsonProperty("caching"), JsonConverter(typeof(StringEnumConverter))]
        public CachingTypeNamesEnum CacheType { get; set; } = CachingTypeNamesEnum.None;

        /// <summary>
        /// Size of disk in gigabytes. Cannot be larger than 1023 (1023 GB).
        /// </summary>
        [JsonProperty("diskSizeGB")]
        public int SizeGB { get; set; } = 4;

        /// <summary>
        /// Logical unit number of the adapter/port attached to on the VM. Value must be 
        /// unique per data disk per VM.
        /// </summary>
        [JsonProperty("lun")]
        public int AttachedOnLUN { get; set; } = 1;

        /// <summary>
        /// URI to the managed disk resource
        /// </summary>
        [JsonProperty("managedDisk")]
        public SubResource? ManagedDisk { get; set; } = null;

        /// <summary>
        /// URI to the latest(?) snapshot
        /// </summary>
        [JsonProperty("snapshot")]
        public SubResource? LatestSnapshot { get; set; } = null;

        /// <summary>
        /// Type of account. UltraSSD_LRS can only be used for data and not for OS disks!
        /// </summary>
        [JsonProperty("storageAccountType")]
        public DiskSkuNamesEnum AccountType { get; set; } = DiskSkuNamesEnum.StandardSSD_LRS;

        public ImageDataDisk() { }

        /// <summary>
        /// Instantiate for an unmanaged disk
        /// </summary>
        /// <param name="attachedLUN">The logical unit number the disk is attached on to the VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="vhdBlobUri">Uri to VHD file stored as a blob in an Azure Storage account</param>
        public ImageDataDisk(int attachedLUN, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, string vhdBlobUri)
        {
            if ((attachedLUN < 0) || (attachedLUN > 254)) { throw new ArgumentOutOfRangeException(nameof(attachedLUN)); }
            if ((!Enum.IsDefined(typeof(DiskSkuNamesEnum), storageType)) || (storageType == DiskSkuNamesEnum.UltraSSD_LRS)) { throw new ArgumentOutOfRangeException(nameof(storageType)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if ((sizeInGB <= 0) || (sizeInGB > 1023)) { throw new ArgumentOutOfRangeException(nameof(sizeInGB)); }
            if (string.IsNullOrWhiteSpace(vhdBlobUri)) { throw new ArgumentNullException(nameof(vhdBlobUri)); }

            AttachedOnLUN = attachedLUN;
            AccountType = storageType;
            LatestSnapshot = null;
            ManagedDisk = null;
            SizeGB = sizeInGB;
            CacheType = caching;
            VhdStoredOnBlobUri = vhdBlobUri;
        }

        /// <summary>
        /// Instantiate for an managed disk
        /// </summary>
        /// <param name="attachedLUN">The logical unit number the disk is attached on to the VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="diskUri">Uri to the managed disk</param>
        public ImageDataDisk(int attachedLUN, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, ResourceUri diskUri)
        {
            if ((!Enum.IsDefined(typeof(DiskSkuNamesEnum), storageType)) || (storageType == DiskSkuNamesEnum.UltraSSD_LRS)) { throw new ArgumentOutOfRangeException(nameof(storageType)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if ((sizeInGB <= 0) || (sizeInGB > 1023)) { throw new ArgumentOutOfRangeException(nameof(sizeInGB)); }
            if ((diskUri == null) || (!diskUri.IsValid)) { throw new ArgumentNullException(nameof(diskUri)); }

            AttachedOnLUN = attachedLUN;
            AccountType = storageType;
            LatestSnapshot = null;
            ManagedDisk = new SubResource(diskUri);
            SizeGB = sizeInGB;
            CacheType = caching;
            VhdStoredOnBlobUri = null;
        }

        /// <summary>
        /// With the disk stored on a blob
        /// </summary>
        /// <param name="blob">Blob resource Uri</param>
        /// <returns>ImageDataDisk</returns>
        public ImageDataDisk WithBlob(ResourceUri blob)
        {
            VhdStoredOnBlobUri = blob.ToString();
            return this;
        }

        /// <summary>
        /// With the disk stored on a blob
        /// </summary>
        /// <param name="blob">Blob resource Uri</param>
        /// <returns>ImageDataDisk</returns>
        public ImageDataDisk WithBlob(string blob)
        {
            VhdStoredOnBlobUri = blob;
            return this;
        }

        /// <summary>
        /// Set the cache type
        /// </summary>
        /// <param name="cachingType">Type of cache</param>
        /// <returns>ImageDataDisk</returns>
        public ImageDataDisk WithCache(CachingTypeNamesEnum cachingType)
        {
            CacheType = cachingType;
            return this;
        }

        /// <summary>
        /// Set disk size
        /// </summary>
        /// <param name="size">Size of disk in GB (cannot be larger than 1023)</param>
        /// <returns>ImageDataDisk</returns>
        public ImageDataDisk WithSize(int size)
        {
            if ((size < 0) || (size > 1023)) { throw new ArgumentOutOfRangeException(nameof(size)); }
            SizeGB = size;
            return this;
        }

        /// <summary>
        /// With a managed disk
        /// </summary>
        /// <param name="managedDisk">Managed disk resource Uri</param>
        /// <returns>ImageDataDisk</returns>
        public ImageDataDisk WithManagedDisk(ResourceUri managedDisk)
        {
            VhdStoredOnBlobUri = managedDisk.ToString();
            return this;
        }

        /// <summary>
        /// With a managed disk
        /// </summary>
        /// <param name="managedDisk">Managed disk resource Uri</param>
        /// <returns>ImageDataDisk</returns>
        public ImageDataDisk WithManagedDisk(string managedDisk)
        {
            VhdStoredOnBlobUri = managedDisk;
            return this;
        }

        /// <summary>
        /// Set type of storage account
        /// </summary>
        /// <param name="storageAccountType"></param>
        /// <returns>ImageDataDisk</returns>
        public ImageDataDisk WithStorageAccountType(DiskSkuNamesEnum storageAccountType)
        {
            AccountType = storageAccountType;
            return this;
        }
    }
}
