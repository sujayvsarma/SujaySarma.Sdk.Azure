using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Networking settings of a VM
    /// </summary>
    public class VMNetworkProfile
    {
        /// <summary>
        /// Network interfaces connected to this VM
        /// </summary>
        [JsonProperty("networkInterfaces")]
        public List<VMNetworkInterface>? Interfaces { get; set; }


        public VMNetworkProfile()
        {
            Interfaces = new List<VMNetworkInterface>();
        }

        /// <summary>
        /// Add a network interface to the VM
        /// </summary>
        /// <param name="makePrimary">Flag indicating if this should be the primary interface</param>
        public void Add(bool makePrimary)
        {
            if (Interfaces != null)
            {
                foreach (VMNetworkInterface card in Interfaces)
                {
                    if ((card.Properties != null) && (card.Properties.IsPrimary))
                    {
                        card.Properties.IsPrimary = false;
                    }
                }
            }

            if (Interfaces == null)
            {
                Interfaces = new List<VMNetworkInterface>();
            }

            Interfaces.Add(new VMNetworkInterface(makePrimary));
        }

        /// <summary>
        /// Clear all network interfaces from this profile
        /// </summary>
        public void Clear() => Interfaces?.Clear();

        /// <summary>
        /// Promote a network interface as the primary for this VM
        /// </summary>
        /// <param name="interfaceId">Resource Uri to the network interface to promote</param>
        public void MakePrimary(ResourceUri interfaceId)
        {
            if ((Interfaces == null) || (Interfaces.Count == 0))
            {
                throw new InvalidOperationException("There are no interfaces present in this profile.");
            }

            string cardId = interfaceId.ToString();
            foreach (VMNetworkInterface card in Interfaces)
            {
                if (card.Id.Equals(cardId, StringComparison.InvariantCultureIgnoreCase))
                {
                    card.Properties = new VMNetworkInterfaceProperties(true);
                }
                else
                {
                    if ((card.Properties != null) && (card.Properties.IsPrimary))
                    {
                        card.Properties.IsPrimary = false;
                    }
                }
            }
        }
    }
}
