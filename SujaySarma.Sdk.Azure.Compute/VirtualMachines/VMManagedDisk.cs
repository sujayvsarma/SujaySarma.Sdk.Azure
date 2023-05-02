using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;

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
        [JsonProperty("storageAccountType"), JsonConverter(typeof(StringEnumConverter))]
        public DiskSkuNamesEnum Type { get; set; } = DiskSkuNamesEnum.Standard_LRS;


        public VMManagedDisk() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diskUri"></param>
        /// <param name="type"></param>
        public VMManagedDisk(ResourceUri diskUri, DiskSkuNamesEnum type = DiskSkuNamesEnum.Standard_LRS)
        {
            if ((diskUri == null) || (!diskUri.IsValid)) { throw new ArgumentException(nameof(diskUri)); }
            if (!Enum.IsDefined(typeof(DiskSkuNamesEnum), type)) { throw new ArgumentOutOfRangeException(nameof(type)); }

            Id = diskUri.ToString();
            Type = type;
        }
    }
}
