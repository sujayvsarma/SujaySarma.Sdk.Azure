using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Properties of the VM
    /// </summary>
    public class VMProperties
    {
        /// <summary>
        /// Additional capabilities enabled/disabled on the VM
        /// </summary>
        [JsonProperty("additionalCapabilities")]
        public VMAdditionalCapabilities? Capabilities { get; set; }

        /// <summary>
        /// The availability set that the VM should be / is assigned to. VMs can be 
        /// added to an availability set only at time of creation.
        /// </summary>
        [JsonProperty("availabilitySet")]
        public SubResource? AvailabilitySet { get; set; }

        /// <summary>
        /// Only for Azure Spot VMs, specifies the billing details
        /// </summary>
        [JsonProperty("billingProfile")]
        public VMBillingProfile? SpotVMBillingProfile { get; set; }

        /// <summary>
        /// Only for Azure Spot VMs, specifies the eviction policy. Only eligible value 
        /// is "Deallocate"
        /// </summary>
        [JsonProperty("evictionPolicy")]
        public string? SpotVMEvictionPolicy { get; set; } = null;

        /// <summary>
        /// Specifies the boot diagnostics state
        /// </summary>
        [JsonProperty("diagnosticsProfile")]
        public VMDiagnosticsProfile? DiagnosticsProfile { get; set; }

        /// <summary>
        /// Hardware settings for the VM
        /// </summary>
        [JsonProperty("hardwareProfile")]
        public VMHardwareProfile? Hardware { get; set; } = null;

        /// <summary>
        /// Information about the dedicated host the VM resides in
        /// </summary>
        [JsonProperty("host")]
        public SubResource? DedicatedHostInfo { get; set; } = null;

        /// <summary>
        /// A view of a running instance of the VM
        /// </summary>
        [JsonProperty("instanceView")]
        public VMInstanceView? InstanceView { get; set; } = null;

        /// <summary>
        /// For hybrid licensed Windows OS VMs, specifies the type of Windows OS contained on the VM. 
        /// Value cannot be changed, set to NULL during updates to prevent errors.
        /// </summary>
        [JsonProperty("licenseType")]
        public string? OnPremWindowsLicenseType { get; set; } = null;

        /// <summary>
        /// The networking settings for the VM
        /// </summary>
        [JsonProperty("networkProfile")]
        public VMNetworkProfile? Networking { get; set; }

        /// <summary>
        /// OS settings
        /// </summary>
        [JsonProperty("osProfile")]
        public VMOSProfile? OSProfile { get; set; }

        /// <summary>
        /// Priority the VM runs at. One of: Low, Regular or Spot.
        /// </summary>
        [JsonProperty("priority")]
        public string? Priority { get; set; } = "Regular";

        /// <summary>
        /// Provisioning state of the VM
        /// </summary>
        [JsonProperty("provisioningState")]
        public string? ProvisioningState { get; set; } = "Suceeded";

        /// <summary>
        /// Specifies information about the proximity placement group that the virtual machine should be assigned to.
        /// </summary>
        [JsonProperty("proximityPlacementGroup")]
        public SubResource? ProximityPlacementGroup { get; set; }

        /// <summary>
        /// Information about the disks/storage attached to the VM
        /// </summary>
        [JsonProperty("storageProfile")]
        public VMStorageSettings? Storage { get; set; }

        /// <summary>
        /// The scaleset the VM should be assigned to. This can only be done at time of creation and 
        /// cannot be changed later.
        /// </summary>
        [JsonProperty("virtualMachineScaleSet")]
        public SubResource? Scaleset { get; set; }

        /// <summary>
        /// Child extension resources of the VM
        /// </summary>
        [JsonProperty("resources")]
        public List<VMExtension>? Resources { get; set; }

        /// <summary>
        /// Specifies the VM unique ID which is a 128-bits identifier that is encoded and stored 
        /// in all Azure IaaS VMs SMBIOS and can be read using platform BIOS commands
        /// </summary>
        [JsonProperty("vmId")]
        public string Id { get; set; } = string.Empty;


        public VMProperties()
        {
            // No VM can live without these, so provision them
            Networking = new VMNetworkProfile();
            OSProfile = new VMOSProfile();
            Storage = new VMStorageSettings();
        }


        /// <summary>
        /// Create a regular virtual machine. 
        /// </summary>
        /// <param name="vmSizeName">Name of the size of the VM</param>
        /// <returns>VMProperties instance</returns>
        public static VMProperties CreateRegularVM(string vmSizeName)
            => new VMProperties()
            {
                Hardware = new VMHardwareProfile(vmSizeName)
            };

        /// <summary>
        /// Create a Spot VM
        /// </summary>
        /// <param name="vmSizeName">Name of the size of the VM</param>
        /// <param name="spotBiddingMaximumPrice">Specifies the maximum price you are willing to pay for a Azure Spot VM/VMSS (use -1 to match on-demand pricing)</param>
        /// <param name="deallocateOnEviction">If set, when the Spot VM is evicted, it will be automatically deallocated (recommended!).</param>
        /// <returns>VMProperties instance</returns>
        public static VMProperties CreateSpotVM(string vmSizeName, double spotBiddingMaximumPrice = -1, bool deallocateOnEviction = true)
            => new VMProperties()
            {
                Hardware = new VMHardwareProfile(vmSizeName),
                SpotVMBillingProfile = new VMBillingProfile(spotBiddingMaximumPrice),
                SpotVMEvictionPolicy = (deallocateOnEviction ? "Deallocate" : null)
            };

        /// <summary>
        /// Create a VM on a dedicated host
        /// </summary>
        /// <param name="vmSizeName">Name of the size of the VM</param>
        /// <param name="dedicatedHostUri">ResourceUri of the dedicated host</param>
        /// <returns>VMProperties instance</returns>
        public static VMProperties CreateOnDedicatedHost(string vmSizeName, ResourceUri dedicatedHostUri)
            => new VMProperties()
            {
                Hardware = new VMHardwareProfile(vmSizeName),
                DedicatedHostInfo = new SubResource(dedicatedHostUri)
            };

        /// <summary>
        /// Create a VM on an availability set
        /// </summary>
        /// <param name="vmSizeName">Name of the size of the VM</param>
        /// <param name="availabilitySetUri">ResourceUri of the availability set</param>
        /// <returns>VMProperties instance</returns>
        public static VMProperties CreateWithAvailabilitySet(string vmSizeName, ResourceUri availabilitySetUri)
            => new VMProperties()
            {
                Hardware = new VMHardwareProfile(vmSizeName),
                AvailabilitySet = new SubResource(availabilitySetUri)
            };

        /// <summary>
        /// Create a VM on a VM Scale Set
        /// </summary>
        /// <param name="vmSizeName">Name of the size of the VM</param>
        /// <param name="vmScaleSetUri">ResourceUri of the VM Scale Set</param>
        /// <returns>VMProperties instance</returns>
        public static VMProperties CreateWithScaleSet(string vmSizeName, ResourceUri vmScaleSetUri)
            => new VMProperties()
            {
                Hardware = new VMHardwareProfile(vmSizeName),
                Scaleset = new SubResource(vmScaleSetUri)
            };


        /// <summary>
        /// Enable boot diagnostics on the VM
        /// </summary>
        /// <param name="diagnosticsStorageAccount">Resource Uri to the storage account where the diagnostics logs and screenshots should be saved</param>
        public void EnableBootDiagnostics(ResourceUri diagnosticsStorageAccount)
            => DiagnosticsProfile = new VMDiagnosticsProfile(diagnosticsStorageAccount);

        /// <summary>
        /// Disable boot diagnostics on the VM
        /// </summary>
        public void DisableBootDiagnostics() => DiagnosticsProfile = null;

        /// <summary>
        /// Enable Ultra SSD disks on the VM
        /// </summary>
        public void EnableUltraSSD() => Capabilities = new VMAdditionalCapabilities(true);

        /// <summary>
        /// Change the size of the VM
        /// </summary>
        /// <param name="newHardwareSizeName">Name of the new size</param>
        public void ChangeHardwareSize(string newHardwareSizeName) => Hardware = new VMHardwareProfile(newHardwareSizeName);



    }
}
