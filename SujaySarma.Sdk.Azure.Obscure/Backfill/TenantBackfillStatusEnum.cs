namespace SujaySarma.Sdk.Azure.Obscure.Backfill
{
    /// <summary>
    /// Enumeration used to provide status of a tenant backfilling operation
    /// </summary>
    public enum TenantBackfillStatusEnum
    {
        Cancelled = -2,
        Failed = -1,
        NotStarted = 0,
        NotStartedButGroupsExist,
        Started,
        Completed
    }
}
