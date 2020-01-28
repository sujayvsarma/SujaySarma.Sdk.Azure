namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Redundancy mode of an App Service website
    /// </summary>
    public enum AppServiceRedundancyModeEnum
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = None,

        /// <summary>
        /// No redundancy
        /// </summary>
        None = 0,

        /// <summary>
        /// Manually scaled redundancy
        /// </summary>
        Manual,

        /// <summary>
        /// Georedundancy
        /// </summary>
        GeoRedundant,

        /// <summary>
        /// Active/passive failover style redundancy
        /// </summary>
        Failover,

        /// <summary>
        /// Active/active redundancy
        /// </summary>
        ActiveActive
    }
}
