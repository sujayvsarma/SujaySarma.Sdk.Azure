using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.AvailabilitySets
{
    /// <summary>
    /// A virtual machine availability set
    /// </summary>
    public class AvailabilitySet : AzureObjectBase
    {

        /// <summary>
        /// SKU of the availability set
        /// </summary>
        [JsonProperty("sku")]
        public ResourceSku? Sku { get; set; } = null;

        /// <summary>
        /// Properties of the set
        /// </summary>
        [JsonProperty("properties")]
        public AvailabilitySetProperties Properties { get; set; } = new AvailabilitySetProperties();

        public AvailabilitySet() { }

        /// <summary>
        /// Create a availability set
        /// </summary>
        /// <param name="locationName">Name of the location</param>
        /// <param name="numberOfFaultDomains">Number of fault domains</param>
        /// <param name="numberOfUpdateDomains">Number of update domains</param>
        /// <param name="allVirtualMachinesMustHaveManagedDisks">If set, creates an Aligned availability set, otherwise creates a classic availability set. Will also 
        /// restrict the type of VMs added later</param>
        /// <param name="proximityGroup">Resource Uri of the Proximity Placement Group to assign the availability set to</param>
        /// <param name="tags">Tags to attach to the VM</param>
        public AvailabilitySet(string locationName, int numberOfFaultDomains, int numberOfUpdateDomains, bool allVirtualMachinesMustHaveManagedDisks = true,
            ResourceUri? proximityGroup = null, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }
            if (numberOfFaultDomains <= 0) { throw new ArgumentOutOfRangeException(nameof(numberOfFaultDomains)); }
            if (numberOfUpdateDomains <= 0) { throw new ArgumentOutOfRangeException(nameof(numberOfUpdateDomains)); }

            Location = locationName;
            Sku = new ResourceSku()
            {
                Name = (allVirtualMachinesMustHaveManagedDisks ? "Aligned" : "Classic")
            };
            Tags = tags;

            Properties = new AvailabilitySetProperties(numberOfFaultDomains, numberOfUpdateDomains, proximityGroup);
        }

        /// <summary>
        /// Add a virtual machine to the availability set
        /// </summary>
        /// <param name="virtualMachine">ResourceUri of the VM to add</param>
        public void AddVirtualMachine(ResourceUri virtualMachine)
        {
            if (Properties == null)
            {
                Properties = new AvailabilitySetProperties();
            }

            Properties.AddVirtualMachine(virtualMachine);
        }

        /// <summary>
        /// Remove a virtual machine from the availability set
        /// </summary>
        /// <param name="virtualMachine">ResourceUri of the VM to remove</param>
        public void RemoveVirtualMachine(ResourceUri virtualMachine)
        {
            if (Properties != null)
            {
                if ((Properties.VirtualMachines == null) || (Properties.VirtualMachines.Count == 0))
                {
                    return;
                }

                Properties.RemoveVirtualMachine(virtualMachine);
            }
        }

    }
}
