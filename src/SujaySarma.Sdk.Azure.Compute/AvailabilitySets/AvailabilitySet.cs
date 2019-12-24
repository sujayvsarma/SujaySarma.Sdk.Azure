using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Compute.AvailabilitySets
{
    /// <summary>
    /// A virtual machine availability set
    /// </summary>
    public class AvailabilitySet : AzureObjectBase
    {
        /// <summary>
        /// Properties of the set
        /// </summary>
        [JsonProperty("properties")]
        public AvailabilitySetProperties Properties { get; set; } = new AvailabilitySetProperties();


        public AvailabilitySet() { }
    }
}
