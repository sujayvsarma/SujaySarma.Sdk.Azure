namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Result codes returned by maintenance operations on a VM
    /// </summary>
    public enum MaintenanceOperationResultCodesEnum
    {
        MaintenanceAborted = -2,

        RetryLater = -1,

        None = 0,

        MaintenanceCompleted

    }
}
