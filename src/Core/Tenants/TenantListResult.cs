namespace SujaySarma.Sdk.Azure.Tenants
{
    /// <summary>
    /// List of tenants returned by the Tenants List endpoint
    /// </summary>
    public class TenantListResult : Common.ListContinuableResult<Tenant>
    {
        public TenantListResult() : base() { }
    }
}
