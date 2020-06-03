using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.VirtualMachines;

using System;
using System.Collections.Generic;

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
        public SubResource? ProximityPlacementGroup { get; set; } = null;

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

        /// <summary>
        /// Create properties object
        /// </summary>
        /// <param name="numberOfFaultDomains">Number of fault domains</param>
        /// <param name="numberOfUpdateDomains">Number of update domains</param>
        /// <param name="proximityGroup">Resource Uri of the Proximity Placement Group to assign the availability set to</param>
        public AvailabilitySetProperties(int numberOfFaultDomains, int numberOfUpdateDomains, ResourceUri? proximityGroup = null)
        {
            FaultDomainCount = numberOfFaultDomains;
            UpdateDomainCount = numberOfUpdateDomains;
            ProximityPlacementGroup = ((proximityGroup == null) ? null : new SubResource(proximityGroup));

            VirtualMachines = new List<SubResource>();
        }

        /// <summary>
        /// Add a virtual machine to the availability set
        /// </summary>
        /// <param name="virtualMachine">ResourceUri of the VM to add</param>
        public void AddVirtualMachine(ResourceUri virtualMachine)
        {
            if ((!virtualMachine.Is(ResourceUriCompareLevel.Provider, "Microsoft.Compute")) || (!virtualMachine.Is(ResourceUriCompareLevel.Type, "virtualMachines")))
            {
                throw new ArgumentException($"{nameof(virtualMachine)} does not represent a virtual machine.");
            }

            string resourceUri = virtualMachine.ToString();
            if (VirtualMachines == null)
            {
                VirtualMachines = new List<SubResource>();
            }

            if (VirtualMachines.Count > 0)
            {
                foreach (SubResource resource in VirtualMachines)
                {
                    if (resource.ResourceId.Equals(resourceUri, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new ArgumentException($"A virtual machine with the resource Id '{resourceUri}' is already added to this availability set.");
                    }
                }
            }

            VirtualMachines.Add(new SubResource(resourceUri));
        }

        /// <summary>
        /// Remove a virtual machine from the availability set
        /// </summary>
        /// <param name="virtualMachine">ResourceUri of the VM to remove</param>
        public void RemoveVirtualMachine(ResourceUri virtualMachine)
        {
            if ((!virtualMachine.Is(ResourceUriCompareLevel.Provider, "Microsoft.Compute")) || (!virtualMachine.Is(ResourceUriCompareLevel.Type, "virtualMachines")))
            {
                throw new ArgumentException($"{nameof(virtualMachine)} does not represent a virtual machine.");
            }

            if ((VirtualMachines == null) || (VirtualMachines.Count == 0))
            {
                return;
            }

            string resourceUri = virtualMachine.ToString();
            SubResource? toRemove = null;
            foreach (SubResource resource in VirtualMachines)
            {
                if (resource.ResourceId.Equals(resourceUri, StringComparison.InvariantCultureIgnoreCase))
                {
                    toRemove = resource;
                    break;
                }
            }

            if (toRemove != null)
            {
                VirtualMachines.Remove(toRemove);
            }
        }
    }
}
