namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Current provisioning state of endpoints
    /// </summary>
    public enum ProvisioningState
    {
        Creating,

        Deleting,

        Failed,

        Succeeded,

        ResolvingDNS
    }
}
