namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Type of availability of a domain name
    /// </summary>
    public enum AvailableResultDomainTypeEnum
    {
        /// <summary>
        /// Regular domain purchase, the full price will be charged 
        /// as per current Azure rates.
        /// </summary>
        Regular = 0,

        /// <summary>
        /// Domain was soft-deleted earlier, no charges will be incurred 
        /// as it will simply be restored to the account.
        /// </summary>
        SoftDeleted
    }
}
