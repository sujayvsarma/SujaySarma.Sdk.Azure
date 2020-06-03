namespace SujaySarma.Sdk.Azure.Compute.Common
{
    /// <summary>
    /// Types of commands that can be run on an Azure VM
    /// </summary>
    public enum VMCommandTypesEnum
    {

        /*
         * ORDER is NOT important. The NAME is important.
         * We are using Enum.GetName() to transform it to an Azure CommandId. 
         * So, match the casing!
         */

        /// <summary>
        /// Get network configuration (ifconfig on Linux, ipconfig on Windows). Does not require 
        /// the set of commands
        /// </summary>
        ifconfig,

        /// <summary>
        /// Run a shell script (bash script/commands on Linux, .cmd script/commands on Windows). 
        /// Requires you to provide the command(s) to execute.
        /// </summary>
        RunShellScript,

        /// <summary>
        /// Run a PowerShell script.
        /// Requires you to provide the command(s) to execute.
        /// </summary>
        RunPowerShellScript

    }
}
