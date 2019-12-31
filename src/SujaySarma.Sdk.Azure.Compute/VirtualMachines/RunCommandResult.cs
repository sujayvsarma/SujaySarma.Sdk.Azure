using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Results from a RunCommand execution. 
    /// This structure is created internally and used.
    /// </summary>
    internal class RunCommandResult
    {
        [JsonProperty("value")]
        public List<InstanceViewStatus>? Status { get; set; }

        public RunCommandResult() { }
    }
}
