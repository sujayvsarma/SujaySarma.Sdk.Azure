namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// The type of filter that is going to be applied to an IP security rule
    /// </summary>
    public enum IpSecurityRestrictionFilterType
    {
        /// <summary>
        /// Default route filter
        /// </summary>
        Default,

        /// <summary>
        /// An XFF proxy filter
        /// </summary>
        XffProxy
    }
}
