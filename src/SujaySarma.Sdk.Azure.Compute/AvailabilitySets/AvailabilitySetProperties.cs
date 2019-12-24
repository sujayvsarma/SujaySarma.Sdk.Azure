using System.Collections.Generic;
using Newtonsoft.Json;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.VirtualMachines;

namespace SujaySarma.Sdk.Azure.Compute.AvailabilitySets
{
    /// <summary>
    /// Properties of a virtual machine availability set
    /// </summary>
    public class AvailabilitySetProperties
    {

        /// <summary>
        /// The fault domain count
        /// </summary>
        [JsonProperty("platformFaultDomainCount")]
        public int FaultDomainCount { get; set; }

        /// <summary>
        /// The update domain count
        /// </summary>
        [JsonProperty("platformUpdateDomainCount")]
        public int UpdateDomainCount { get; set; }

        /// <summary>
        /// The proximity placement group resource Id that the availability set 
        /// should be assigned to
        /// </summary>
        [JsonProperty("proximityPlacementGroup")]
        public SubResource ProximityPlacementGroup { get; set; } = new SubResource();

        /// <summary>
        /// Status of the set
        /// </summary>
        [JsonProperty("statuses")]
        public InstanceViewStatus? Status { get; set; }

        /// <summary>
        /// The list of virtual machines assigned to this availability set
        /// </summary>
        [JsonProperty("virtualMachines")]
        public List<SubResource>? VirtualMachines { get; set; } = null;

        public AvailabilitySetProperties() { }
    }
}
