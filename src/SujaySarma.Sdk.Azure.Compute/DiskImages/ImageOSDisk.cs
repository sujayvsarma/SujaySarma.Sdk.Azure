using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

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
    }
}
