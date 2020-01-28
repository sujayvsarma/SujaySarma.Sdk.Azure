namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Type of SSL that is enabled on an App Service host
    /// </summary>
    public enum AppServiceWebAppSslTypeEnum
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = Disabled,

        /// <summary>
        /// Disabled
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// SNI-based SSL is enabled
        /// </summary>
        SniEnabled,

        /// <summary>
        /// IP Address-based SSL is enabled
        /// </summary>
        IpBasedEnabled

    }
}
