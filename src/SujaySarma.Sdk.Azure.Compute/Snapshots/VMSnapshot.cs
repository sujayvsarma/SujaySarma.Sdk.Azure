
using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Disks;

using System;

namespace SujaySarma.Sdk.Azure.Compute.Snapshots
{
    /// <summary>
    /// Virtual machine snapshot
    /// </summary>
    public class VMSnapshot : AzureObjectBase
    {

        /// <summary>
        /// The resource Id of the entity managing the snapshot
        /// </summary>
        [JsonProperty("managedBy")]
        public string? ManagedBy { get; set; }

        /// <summary>
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public VMSnapshotProperties Properties { get; set; } = new VMSnapshotProperties();


        public VMSnapshot() { }

        /// <summary>
        /// Create a snapshot of a VM
        /// </summary>
        /// <param name="creationMetadata">Disk creation metadata</param>
        /// <param name="sizeGB">Size of disk in GB (0 to 1023)</param>
        /// <param name="managedBy">Resource Id of the entity managing the snapshot</param>
        public VMSnapshot(DiskCreationMetadata creationMetadata, int sizeGB, ResourceUri? managedBy = null)
        {
            if ((managedBy != null) && (!managedBy.IsValid)) { throw new ArgumentException(nameof(managedBy)); }

            Properties = new VMSnapshotProperties(creationMetadata, sizeGB);
            ManagedBy = managedBy?.ToString();
        }
    }
}
