using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.DiskImages
{
    /// <summary>
    /// The primary OS disk that is in the image
    /// </summary>
    public class ImageOSDisk
    {
        /// <summary>
        /// Uri to the VHD that is stored as a blob in an Azure Storage account
        /// </summary>
        [JsonProperty("blobUri")]
        public string? VhdStoredOnBlobUri { get; set; } = null;

        /// <summary>
        /// Type of caching
        /// </summary>
        [JsonProperty("caching", ItemConverterType = typeof(StringEnumConverter))]
        public CachingTypeNamesEnum CacheType { get; set; } = CachingTypeNamesEnum.None;

        /// <summary>
        /// Size of disk in gigabytes. Cannot be larger than 1023 (1023 GB).
        /// </summary>
        [JsonProperty("diskSizeGB")]
        public int SizeGB { get; set; } = 4096;

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
        /// Type of account. UltraSSD_LRS cannot be used!
        /// </summary>
        [JsonProperty("storageAccountType")]
        public DiskSkuNamesEnum AccountType { get; set; } = DiskSkuNamesEnum.StandardSSD_LRS;

        /// <summary>
        /// Generalized/Specialized state of the OS
        /// </summary>
        [JsonProperty("osState")]
        public OSStateTypeNamesEnum StateOfOS { get; set; } = OSStateTypeNamesEnum.Specialized;

        /// <summary>
        /// Type of OS loaded
        /// </summary>
        [JsonProperty("osType")]
        public OSTypeNamesEnum OperatingSystemType { get; set; } = OSTypeNamesEnum.Windows;

        public ImageOSDisk() { }

        /// <summary>
        /// Instantiate for an unmanaged disk
        /// </summary>
        /// <param name="osName">Type of OS on the disk</param>
        /// <param name="isGeneralized">If true, image is generalied, suitable to create a new VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="vhdBlobUri">Uri to VHD file stored as a blob in an Azure Storage account</param>
        public ImageOSDisk(OSTypeNamesEnum osName, bool isGeneralized, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, string vhdBlobUri)
        {
            if (!Enum.IsDefined(typeof(OSTypeNamesEnum), osName)) { throw new ArgumentOutOfRangeException(nameof(osName)); }
            if ((!Enum.IsDefined(typeof(DiskSkuNamesEnum), storageType)) || (storageType == DiskSkuNamesEnum.UltraSSD_LRS)) { throw new ArgumentOutOfRangeException(nameof(storageType)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if ((sizeInGB <= 0) || (sizeInGB > 1023)) { throw new ArgumentOutOfRangeException(nameof(sizeInGB)); }
            if (string.IsNullOrWhiteSpace(vhdBlobUri)) { throw new ArgumentNullException(nameof(vhdBlobUri)); }

            OperatingSystemType = osName;
            StateOfOS = (isGeneralized ? OSStateTypeNamesEnum.Generalized : OSStateTypeNamesEnum.Specialized);
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
        /// <param name="osName">Type of OS on the disk</param>
        /// <param name="isGeneralized">If true, image is generalied, suitable to create a new VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="diskUri">Uri to the managed disk</param>
        public ImageOSDisk(OSTypeNamesEnum osName, bool isGeneralized, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, ResourceUri diskUri)
        {
            if (!Enum.IsDefined(typeof(OSTypeNamesEnum), osName)) { throw new ArgumentOutOfRangeException(nameof(osName)); }
            if ((!Enum.IsDefined(typeof(DiskSkuNamesEnum), storageType)) || (storageType == DiskSkuNamesEnum.UltraSSD_LRS)) { throw new ArgumentOutOfRangeException(nameof(storageType)); }
            if (!Enum.IsDefined(typeof(CachingTypeNamesEnum), caching)) { throw new ArgumentOutOfRangeException(nameof(caching)); }
            if ((sizeInGB <= 0) || (sizeInGB > 1023)) { throw new ArgumentOutOfRangeException(nameof(sizeInGB)); }
            if ((diskUri == null) || (!diskUri.IsValid)) { throw new ArgumentNullException(nameof(diskUri)); }

            OperatingSystemType = osName;
            StateOfOS = (isGeneralized ? OSStateTypeNamesEnum.Generalized : OSStateTypeNamesEnum.Specialized);
            AccountType = storageType;
            LatestSnapshot = null;
            ManagedDisk = new SubResource(diskUri);
            SizeGB = sizeInGB;
            CacheType = caching;
            VhdStoredOnBlobUri = null;
        }
    }
}
