using Newtonsoft.Json;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Information about the loaded operating system
    /// </summary>
    public class VMOSProfile
    {
        /// <summary>
        /// Password for the provisioned administrator account. Must follow policies for 
        /// the respective OS.
        /// </summary>
        [JsonProperty("adminPassword")]
        public string? AdministratorPassword { get; set; } = null;

        /// <summary>
        /// Username of the administrator user account.
        /// </summary>
        [JsonProperty("adminUsername")]
        public string? AdministratorUserName { get; set; } = null;

        /// <summary>
        /// Flag indicating if extensions should be allowed to run on the VM
        /// </summary>
        [JsonProperty("allowExtensionOperations")]
        public bool? AllowExtensionsToExecute { get; set; } = true;

        /// <summary>
        /// The computer's hostname. Cannot be updated after VM creation!
        /// </summary>
        [JsonProperty("computerName")]
        public string? Hostname { get; set; } = null;

        /// <summary>
        /// Base64-encoded custom data max size of 64KB, will be saved as a binary file 
        /// within the VM.
        /// </summary>
        [JsonProperty("customData")]
        public string? CustomData { get; set; } = null;

        /// <summary>
        /// If set, the host will signal the VM when it has been provisioned.
        /// </summary>
        [JsonProperty("requireGuestProvisionSignal")]
        public bool? RequiresGuestProvisioningSignal { get; set; }

        /// <summary>
        /// Settings specific to Linux. NULL if a Windows VM.
        /// </summary>
        [JsonProperty("linuxConfiguration")]
        public VMSettingsForLinux? Linux { get; set; } = null;

        /// <summary>
        /// Settings specific to Windows. NULL if a Linux VM.
        /// </summary>
        [JsonProperty("windowsConfiguration")]
        public VMSettingsForWindows? Windows { get; set; } = null;

        /// <summary>
        /// Certificates uploaded into the VM
        /// </summary>
        [JsonProperty("secrets")]
        public List<VMCertificates>? Certificates { get; set; } = null;


        public VMOSProfile() { }
    }
}
