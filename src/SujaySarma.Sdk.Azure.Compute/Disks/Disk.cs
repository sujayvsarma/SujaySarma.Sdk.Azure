
using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

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
    }
}
