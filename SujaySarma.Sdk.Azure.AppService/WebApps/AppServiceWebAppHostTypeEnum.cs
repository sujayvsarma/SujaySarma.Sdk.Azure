namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Type of host that is being described
    /// </summary>
    public enum AppServiceWebAppHostTypeEnum
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = Standard,

        /// <summary>
        /// Standard (App service)
        /// </summary>
        Standard = 0,

        /// <summary>
        /// The KUDU / Scm Host
        /// </summary>
        Repository
    }
}
