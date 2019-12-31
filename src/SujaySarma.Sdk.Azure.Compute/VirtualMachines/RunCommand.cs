using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Commands to run on a VM. 
    /// This structure is created internally and used.
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
}
