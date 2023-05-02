using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// The Maintenance Operation status on the virtual machine.
    /// </summary>
    public class VMInstanceViewMaintenanceRedeploymentStatus
    {

        /// <summary>
        /// True if customer is allowed to perform (adhoc) maintenance
        /// </summary>
        [JsonProperty("isCustomerInitiatedMaintenanceAllowed")]
        public bool CanMaintenanceBeInitiatedByCustomer { get; set; } = false;

        /// <summary>
        /// The message returned for the last maintenance operation. Can be NULL.
        /// </summary>
        [JsonProperty("lastOperationMessage")]
        public string? LastMaintenanceOperationMessage { get; set; }

        /// <summary>
        /// Result code of the last maintenance operation. May be NULL.
        /// </summary>
        [JsonProperty("lastOperationResultCode"), JsonConverter(typeof(StringEnumConverter))]
        public MaintenanceOperationResultCodesEnum? LastMaintenanceOperationResult { get; set; } = MaintenanceOperationResultCodesEnum.None;

        /// <summary>
        /// Start time for the maintenance window
        /// </summary>
        [JsonProperty("maintenanceWindowStartTime")]
        public string? MaintenanceWindowStartTime { get; set; }

        /// <summary>
        /// End time for the maintenance window
        /// </summary>
        [JsonProperty("maintenanceWindowEndTime")]
        public string? MaintenanceWindowEndTime { get; set; }

        /// <summary>
        /// Start time for the pre-maintenance window (notification period)
        /// </summary>
        [JsonProperty("preMaintenanceWindowStartTime")]
        public string? PreMaintenanceWindowStartTime { get; set; }

        /// <summary>
        /// End time for the pre-maintenance window (notification period)
        /// </summary>
        [JsonProperty("preMaintenanceWindowEndTime")]
        public string? PreMaintenanceWindowEndTime { get; set; }


        public VMInstanceViewMaintenanceRedeploymentStatus() { }
    }
}
