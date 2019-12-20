using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Compute.Common;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Reference to the Managed Disk
    /// </summary>
    public class VMManagedDisk
    {
        /// <summary>
        /// Resource Id of the disk
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Type of account. UltraSSD_LRS can only be used with data disks, 
        /// it cannot be used with OS Disk.
        /// </summary>
        [JsonProperty("storageAccountType", ItemConverterType = typeof(StringEnumConverter))]
        public DiskSkuNamesEnum Type { get; set; } = DiskSkuNamesEnum.Standard_LRS;
    }
}
