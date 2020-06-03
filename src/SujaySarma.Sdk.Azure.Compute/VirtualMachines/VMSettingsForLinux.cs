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

        /// <summary>
        /// Enable for password authentication
        /// </summary>
        public void WithPasswordAuthentication() => IsPasswordAuthenticationEnabled = true;

        /// <summary>
        /// Enable for SSH authentication
        /// </summary>
        /// <param name="sshPublicKey">SSH public key</param>
        public void WithSshAuthentication(string sshPublicKey)
            => SshConfiguration = new VMSshConfiguration()
            {
                Keys = new System.Collections.Generic.List<VMSshPublicKey>()
                {
                    new VMSshPublicKey()
                    {
                        KeyData = sshPublicKey

                    }
                }
            };

        /// <summary>
        /// Adds an SSH key to the collection without disturbing the existing collection
        /// </summary>
        /// <param name="sshPublicKey">SSH public key to add</param>
        public void AddSshKey(string sshPublicKey)
        {
            if (SshConfiguration == null)
            {
                SshConfiguration = new VMSshConfiguration();
            }

            SshConfiguration.AddKey(sshPublicKey);
        }

    }
}
