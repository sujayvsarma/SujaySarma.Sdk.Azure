using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

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
        public string VhdStoredOnBlobUri { get; set; } = string.Empty;

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
        /// Logical unit number of the adapter/port attached to on the VM. Value must be 
        /// unique per data disk per VM.
        /// </summary>
        [JsonProperty("lun")]
        public int AttachedOnLUN { get; set; } = 1;

        /// <summary>
        /// URI to the managed disk resource
        /// </summary>
        [JsonProperty("managedDisk")]
        public SubResource ManagedDisk { get; set; } = new SubResource();

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
    }
}
