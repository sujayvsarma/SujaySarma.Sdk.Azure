using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Parameters for the RunCommand. 
    /// This structure is created internally and used.
    /// </summary>
    internal class RunCommandParameter
    {
        /// <summary>
        /// Name of the parameter
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Value of the parameter
        /// </summary>
        [JsonProperty("value")]
        public string? Value { get; set; } = null;


        public RunCommandParameter() { }
    }
}
