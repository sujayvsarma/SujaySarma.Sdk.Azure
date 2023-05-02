namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// State of a vNet rule
    /// </summary>
    public enum VirtualNetworkRuleState
    {
        deprovisioning,

        failed,

        networkSourceDeleted,

        provisioning,

        succeeded
    }
}
