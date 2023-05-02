using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Extension resources installed onto the VM
    /// </summary>
    public class VMExtension : AzureObjectBase
    {
        /// <summary>
        /// Properties of the extension
        /// </summary>
        [JsonProperty("properties")]
        public VMExtensionProperties Properties { get; set; } = new VMExtensionProperties();

        public VMExtension() { }
    }
}
