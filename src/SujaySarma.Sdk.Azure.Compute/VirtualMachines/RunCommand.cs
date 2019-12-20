using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Commands to run on a VM
    /// </summary>
    internal class RunCommand
    {

        /// <summary>
        /// The command to run
        /// </summary>
        [JsonProperty("commandId")]
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// Command-line input parameters for the command
        /// </summary>
        [JsonProperty("parameters")]
        public List<RunCommandParameter>? Parameters { get; set; } = null;

        /// <summary>
        /// If provided, instead of whatever is specified by <see cref="Command"/>, the lines specified here will 
        /// be executed.
        /// </summary>
        [JsonProperty("script")]
        public List<string>? Script { get; set; } = null;


        public RunCommand() { }

    }

    /// <summary>
    /// Parameters for the RunCommand
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

    /// <summary>
    /// Results from a RunCommand execution
    /// </summary>
    internal class RunCommandResult
    {
        [JsonProperty("value")]
        public List<InstanceViewStatus>? Status { get; set; }

        public RunCommandResult() { }
    }
}
