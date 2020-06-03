using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;
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

        /// <summary>
        /// Create VM operating system profile for Windows OS
        /// </summary>
        /// <param name="hostName">Hostname for the VM</param>
        /// <param name="administratorUserName">Username to use for the Administrator user</param>
        /// <param name="administratorPasword">Password for the Administrator user</param>
        /// <param name="timeZoneName">Name of the timezone</param>
        /// <param name="enableAutomaticUpdates">Enable automatic (Windows) updates</param>
        /// <param name="provisionVMAgent">Flag to provision the VMAgent service inside the VM</param>
        /// <returns>The OS profile</returns>
        public static VMOSProfile CreateProfileForWindows(string hostName, string administratorUserName, string administratorPasword, string timeZoneName,
            bool enableAutomaticUpdates = true, bool provisionVMAgent = true)
        {
            if (!IsValidHostName(hostName)) { throw new ArgumentNullException(nameof(hostName)); }
            if (string.IsNullOrWhiteSpace(administratorUserName)) { throw new ArgumentNullException(nameof(administratorUserName)); }
            if (string.IsNullOrWhiteSpace(administratorPasword)) { throw new ArgumentNullException(nameof(administratorPasword)); }
            if (string.IsNullOrWhiteSpace(timeZoneName)) { throw new ArgumentNullException(nameof(timeZoneName)); }

            VMOSProfile profile = new VMOSProfile()
            {
                Hostname = hostName,
                AdministratorUserName = administratorUserName,
                AdministratorPassword = administratorPasword,

                Windows = new VMSettingsForWindows()
                {
                    AdditionalCommands = null,
                    EnabledAutomaticUpdates = enableAutomaticUpdates,
                    ShouldProvisionVMAgent = provisionVMAgent,
                    TimeZoneName = timeZoneName,
                    WinRM = new WinRMConfiguration()
                    {
                        Listeners = new List<WinRMListener>()
                        {
                            { new WinRMListener() { CertificateURI = null, Protocol = WinRMProtocolsEnum.https } }
                        }
                    }
                }
            };

            return profile;
        }

        /// <summary>
        /// Create VM operating system profile for Linux OS
        /// </summary>
        /// <param name="hostName">Hostname for the VM</param>
        /// <param name="administratorUserName">Username to use for the Administrator user</param>
        /// <param name="administratorPasword">Password for the Administrator user</param>
        /// <param name="timeZoneName">Name of the timezone</param>
        /// <param name="provisionVMAgent">Flag to provision the VMAgent service inside the VM</param>
        /// <param name="sshPublicKey">SSH public key for SSH login (cannot be null if password auth is disabled)</param>
        /// <param name="enablePasswordAuthentication">Flag to enable password auth (if FALSE, the SSH key should be provided)</param>
        /// <returns>The OS profile</returns>
        public static VMOSProfile CreateProfileForLinux(string hostName, string administratorUserName, string administratorPasword, string timeZoneName,
            string? sshPublicKey = null, bool provisionVMAgent = true, bool enablePasswordAuthentication = false)
        {
            if (!IsValidHostName(hostName)) { throw new ArgumentNullException(nameof(hostName)); }
            if (string.IsNullOrWhiteSpace(administratorUserName)) { throw new ArgumentNullException(nameof(administratorUserName)); }
            if (string.IsNullOrWhiteSpace(administratorPasword)) { throw new ArgumentNullException(nameof(administratorPasword)); }
            if (string.IsNullOrWhiteSpace(timeZoneName)) { throw new ArgumentNullException(nameof(timeZoneName)); }

            VMOSProfile profile = new VMOSProfile()
            {
                Hostname = hostName,
                AdministratorUserName = administratorUserName,
                AdministratorPassword = administratorPasword,

                Linux = new VMSettingsForLinux()
                {
                    ShouldProvisionVMAgent = provisionVMAgent,
                    IsPasswordAuthenticationEnabled = enablePasswordAuthentication,

                    SshConfiguration = string.IsNullOrWhiteSpace(sshPublicKey) switch
                    {
                        true => null,
                        false => new VMSshConfiguration()
                        {
                            Keys = new List<VMSshPublicKey>()
                            {
                                new VMSshPublicKey()
                                {
                                    KeyData = sshPublicKey ?? throw new ArgumentNullException(nameof(sshPublicKey))
                                }
                            }
                        }
                    }
                }
            };

            return profile;
        }


        /// <summary>
        /// Inject certificates intot he VM
        /// </summary>
        /// <param name="keyVault">Resource Uri to the Azure KeyVault store containing the secrets to inject into the VM</param>
        public void InjectCertificates(ResourceUri keyVault)
        {
            if ((keyVault == null) || (!keyVault.IsValid) || (!keyVault.Is(ResourceUriCompareLevel.Provider, "Microsoft.KeyVault")) ||
                (!keyVault.Is(ResourceUriCompareLevel.Type, "vaults")))
            {
                throw new ArgumentException(nameof(keyVault));
            }

            if (Certificates == null)
            {
                Certificates = new List<VMCertificates>();
            }

            // validate if Keyvault is already added
            string vaultId = keyVault.ToString();
            foreach (VMCertificates vault in Certificates)
            {
                if (vault.Vault?.ResourceId == vaultId)
                {
                    throw new ArgumentException($"The vault '{vaultId}' is already injected into this VM.");
                }
            }

            Certificates.Add(new VMCertificates(keyVault));
        }

        /// <summary>
        /// Validate the hostname
        /// </summary>
        /// <param name="hostName">Hostname to check</param>
        /// <returns>True if hostname is valid.</returns>
        public static bool IsValidHostName(string hostName)
        {
            if (string.IsNullOrWhiteSpace(hostName)) { return false; }

            if (hostName.Length > 64)
            {
                return false;
            }

            char[] nameChars = hostName.ToCharArray();
            for (int i = 0; i < nameChars.Length; i++)
            {
                if (nameChars[i] == '_')
                {
                    if ((i == 0) || (i == (nameChars.Length - 1)))
                    {
                        // cannot start or end with '_'
                        return false;
                    }

                    continue;
                }

                if (nameChars[i] == '-')
                {
                    if ((i == 0) || (i == (nameChars.Length - 1)))
                    {
                        // cannot start or end with '-'
                        return false;
                    }

                    continue;
                }

                if (!char.IsLetterOrDigit(nameChars[i]))
                {
                    // dont allow any other special character
                    return false;
                }
            }

            return true;
        }
    }
}
