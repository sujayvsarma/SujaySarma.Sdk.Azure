using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Compute.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// This structure provides runtime values of various things about the VM. It 
    /// contains an exhaustive duplication of quite a lot of stuff already in 
    /// VirtualMachine and VMProperties and their subproperties.
    /// </summary>
    public class VMInstanceView
    {
        /// <summary>
        /// Runtime locations of console log and screenshots captured at VM bootup. Only available if 
        /// diagnostics is enabled and the VM restarted.
        /// </summary>
        [JsonProperty("bootDiagnostics")]
        public VMInstanceViewBootDiagnostics? BootDiagnostics { get; set; } = null;

        /// <summary>
        /// The computer name assigned to the VM.
        /// </summary>
        [JsonProperty("computerName")]
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// Name of the running OS
        /// </summary>
        [JsonProperty("osName")]
        public string? OperatingSystemName { get; set; }

        /// <summary>
        /// Version of the running OS
        /// </summary>
        [JsonProperty("osVersion")]
        public string? OperatingSystemVersion { get; set; }

        /// <summary>
        /// Disks currently attached to the VM
        /// </summary>
        [JsonProperty("disks")]
        public List<VMInstanceViewDisk> Disks { get; set; } = new List<VMInstanceViewDisk>();

        /// <summary>
        /// Extensions on the VM
        /// </summary>
        [JsonProperty("extensions")]
        public List<VMInstanceViewExtension>? Extensions { get; set; } = null;

        /// <summary>
        /// The HyperV generation name
        /// </summary>
        [JsonProperty("hyperVGeneration")]
        public HyperVGenerationNamesEnum Generation { get; set; } = HyperVGenerationNamesEnum.V2;

        /// <summary>
        /// The Maintenance Operation status on the virtual machine.
        /// </summary>
        [JsonProperty("maintenanceRedeployStatus")]
        public VMInstanceViewMaintenanceRedeploymentStatus? MaintenanceRedeploymentStatus { get; set; } = null;

        /// <summary>
        /// Fault domain of the VM
        /// </summary>
        [JsonProperty("platformFaultDomain")]
        public string? FaultDomain { get; set; }

        /// <summary>
        /// Update domain of the VM
        /// </summary>
        [JsonProperty("platformUpdateDomain")]
        public string? UpdateDomain { get; set; }

        /// <summary>
        /// Only for Windows, the thumbprint of the RDP session certificate
        /// </summary>
        [JsonProperty("rdpThumbPrint")]
        public string? WindowsRemoteDesktopCertificateThumbprint { get; set; }

        /// <summary>
        /// Status of various things
        /// </summary>
        [JsonProperty("statuses")]
        public List<InstanceViewStatus>? Status { get; set; }

        /// <summary>
        /// Instance view of the VM Agent on the VM
        /// </summary>
        [JsonProperty("vmAgent")]
        public VMAgentInstanceView? Agent { get; set; }


        public VMInstanceView() { }
    }
}
