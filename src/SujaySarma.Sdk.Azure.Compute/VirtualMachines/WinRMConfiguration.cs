using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// WinRM configuration
    /// </summary>
    public class WinRMConfiguration
    {
        /// <summary>
        /// WinRM listeners
        /// </summary>
        [JsonProperty("listeners")]
        public List<WinRMListener>? Listeners { get; set; } = null;


        public WinRMConfiguration() { }
    }
}
