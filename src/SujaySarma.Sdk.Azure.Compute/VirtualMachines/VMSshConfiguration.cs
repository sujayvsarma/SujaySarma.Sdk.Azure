using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// SSH Configuration for a VM
    /// </summary>
    public class VMSshConfiguration
    {
        [JsonProperty("publicKeys")]
        public List<VMSshPublicKey> Keys { get; set; } = new List<VMSshPublicKey>();

        public VMSshConfiguration() { }

        /// <summary>
        /// Add an SSH key to the collection. Existing keys are left as-is
        /// </summary>
        /// <param name="key">SSH key to add</param>
        public void AddKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) { throw new ArgumentNullException(nameof(key)); }
            if (Keys == null)
            {
                Keys = new List<VMSshPublicKey>();
            }

            Keys.Add(new VMSshPublicKey() { KeyData = key });
        }

        /// <summary>
        /// Clear all the keys
        /// </summary>
        public void Clear() => Keys?.Clear();
    }
}
