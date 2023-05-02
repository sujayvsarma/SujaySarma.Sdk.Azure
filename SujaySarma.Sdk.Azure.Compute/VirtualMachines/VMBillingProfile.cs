using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Billing profile for Spot VMs
    /// </summary>
    public class VMBillingProfile
    {

        /// <summary>
        /// Specifies the maximum price you are willing to pay for a Azure Spot VM/VMSS. This price is in US Dollars. 
        /// Set to -1 to indicate on-demand price-match. VM is provisioned/kept alive only as long as this is higher than 
        /// the current Azure Spot price.
        /// </summary>
        [JsonProperty("maxPrice")]
        public double MaximumBidPrice { get; set; } = -1;


        public VMBillingProfile() { }


        public VMBillingProfile(double bidPrice)
        {
            if ((bidPrice < -1) || (bidPrice == 0)) { throw new ArgumentOutOfRangeException(nameof(bidPrice)); }
            MaximumBidPrice = bidPrice;
        }
    }
}
