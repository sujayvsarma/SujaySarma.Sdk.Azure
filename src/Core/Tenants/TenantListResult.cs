using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Tenants
{
    /// <summary>
    /// List of tenants returned by the Tenants List endpoint
    /// </summary>
    public class TenantListResult : ListResultWithContinuations<Tenant>
    {
        public TenantListResult() : base() { }
    }
}
