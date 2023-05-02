using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Reference to the marketplace offering that was instantiated as a VM
    /// </summary>
    public class VMMarketplaceOfferingReference
    {
        /// <summary>
        /// Resource Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The offer or platform image from the marketplace
        /// </summary>
        [JsonProperty("offer")]
        public string? Offer { get; set; }

        /// <summary>
        /// Reference to the publisher of the image
        /// </summary>
        [JsonProperty("publisher")]
        public string? Publisher { get; set; }

        /// <summary>
        /// The SKU that was used
        /// </summary>
        [JsonProperty("sku")]
        public string? SKU { get; set; }

        /// <summary>
        /// Specifies the version of the platform image or marketplace image used to create the virtual machine. 
        /// The allowed formats are Major.Minor.Build or 'latest'. Major, Minor, and Build are decimal numbers. 
        /// Specify 'latest' to use the latest version of an image available at deploy time. Even if you use 'latest', 
        /// the VM image will not automatically update after deploy time even if a new version becomes available.
        /// </summary>
        [JsonProperty("version")]
        public string? Version { get; set; }


        public VMMarketplaceOfferingReference() { }
    }
}
