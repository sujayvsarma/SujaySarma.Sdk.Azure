
using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

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
    }
}
