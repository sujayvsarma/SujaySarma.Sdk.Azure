using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Specifies the storage settings for the virtual machine disks
    /// </summary>
    public class VMStorageSettings
    {
        /// <summary>
        /// Non-OS ("data") disks attached
        /// </summary>
        [JsonProperty("dataDisks")]
        public List<VMDataDisk>? DataDisks { get; set; }

        /// <summary>
        /// Information about the primary (OS) disk attached
        /// </summary>
        [JsonProperty("osDisk")]
        public VMOSDisk PrimaryDisk { get; set; } = new VMOSDisk();

        /// <summary>
        /// Reference to the marketplace offering that was instantiated
        /// </summary>
        [JsonProperty("imageReference")]
        public VMMarketplaceOfferingReference? MarketplaceImageReference { get; set; }

        public VMStorageSettings() { }
    }
}
