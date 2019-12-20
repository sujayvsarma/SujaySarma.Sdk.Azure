using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Settings specific to the Linux OS
    /// </summary>
    public class VMSettingsForLinux
    {
        /// <summary>
        /// Get/set if password based logins are disabled. If true, one must use key-based 
        /// logins
        /// </summary>
        [JsonProperty("disablePasswordAuthentication")]
        public bool IsPasswordAuthenticationEnabled { get; set; } = false;

        /// <summary>
        /// Get/set if the VM agent should be provisioned.
        /// </summary>
        [JsonProperty("provisionVMAgent")]
        public bool ShouldProvisionVMAgent { get; set; } = true;

        /// <summary>
        /// SSH Login Configuration
        /// </summary>
        [JsonProperty("ssh")]
        public VMSshConfiguration? SshConfiguration { get; set; } = null;


        public VMSettingsForLinux() { }
    }
}
