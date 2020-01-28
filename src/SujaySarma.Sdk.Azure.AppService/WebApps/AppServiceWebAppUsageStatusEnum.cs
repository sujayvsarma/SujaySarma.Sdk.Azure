namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Status of an App Service's usage versus usage quotas
    /// </summary>
    public enum AppServiceWebAppUsageStatusEnum
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = Normal,

        /// <summary>
        /// Normal, under the limits/quotas
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Has exceeded one or more quotas (may be in suspended state)
        /// </summary>
        Exceeded
    }
}
