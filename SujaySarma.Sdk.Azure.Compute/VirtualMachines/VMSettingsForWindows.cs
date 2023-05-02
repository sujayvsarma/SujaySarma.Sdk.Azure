using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Settings specific to the Windows OS
    /// </summary>
    public class VMSettingsForWindows
    {
        /// <summary>
        /// Indicate if auto updates (Windows Update) should be enabled
        /// </summary>
        [JsonProperty("enableAutomaticUpdates")]
        public bool EnabledAutomaticUpdates { get; set; } = true;

        /// <summary>
        /// Indicate if the VM agent should be provisioned
        /// </summary>
        [JsonProperty("provisionVMAgent")]
        public bool ShouldProvisionVMAgent { get; set; } = true;

        /// <summary>
        /// Name of the timezone to set
        /// </summary>
        [JsonProperty("timeZone")]
        public string TimeZoneName { get; set; } = "Pacific Standard Time";

        /// <summary>
        /// Specifies additional base-64 encoded XML formatted information that can be included 
        /// in the Unattend.xml file, which is used by Windows Setup
        /// </summary>
        [JsonProperty("additionalUnattendContent")]
        public List<VMUnattendedWindowsSetupContent>? AdditionalCommands { get; set; } = null;

        /// <summary>
        /// WindowsRM configuration
        /// </summary>
        [JsonProperty("winRM")]
        public WinRMConfiguration? WinRM { get; set; }


        public VMSettingsForWindows() { }
    }
}
