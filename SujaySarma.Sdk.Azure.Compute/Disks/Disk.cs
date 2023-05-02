using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    /// <summary>
    /// A disk used by a VM
    /// </summary>
    public class Disk : AzureObjectBase
    {

        /// <summary>
        /// ID URI to the VM to which this disk is attached. NULL if the disk is not attached to anything.
        /// </summary>
        [JsonProperty("managedBy")]
        public string? ManagedBy { get; set; } = null;

        /// <summary>
        /// SKU type of the disk
        /// </summary>
        [JsonProperty("sku")]
        public DiskSku? Sku { get; set; } = null;

        /// <summary>
        /// Properties of the disk
        /// </summary>
        [JsonProperty("properties")]
        public DiskProperties Properties { get; set; } = new DiskProperties();

        /// <summary>
        /// Default constructor
        /// </summary>
        public Disk() { }

        /// <summary>
        /// Build a disk
        /// </summary>
        /// <param name="sku">Name of the SKU</param>
        /// <param name="tier">Tier of the SKU</param>
        /// <param name="properties">Other properties of the disk</param>
        public Disk(DiskSkuNamesEnum sku, string? tier = null, DiskProperties? properties = null)
        {
            Sku = new DiskSku(sku, tier);

            if ((sku != DiskSkuNamesEnum.UltraSSD_LRS) && (properties != null))
            {
                // some properties cannot be set for non-UltraSSD_LRS
                properties.ReadWriteIOPS = null;
                properties.ReadWriteMBps = null;
            }

            Properties = properties ?? new DiskProperties();
        }
    }
}
