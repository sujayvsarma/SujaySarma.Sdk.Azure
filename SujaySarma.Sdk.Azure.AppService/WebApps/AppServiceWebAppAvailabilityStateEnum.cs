namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Management information availability state for the app
    /// </summary>
    public enum AppServiceWebAppAvailabilityStateEnum
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = Normal,

        /// <summary>
        /// Normal mode
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Limited functionality
        /// </summary>
        Limited,

        /// <summary>
        /// Recovering from disaster
        /// </summary>
        DisasterRecoveryMode
    }
}
