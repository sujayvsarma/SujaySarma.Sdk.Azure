namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Type of action that will be run as part of auto-heal
    /// </summary>
    public enum AppServiceWebAppAutoHealActionType
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = LogEvent,

        /// <summary>
        /// Log the event and continue
        /// </summary>
        LogEvent = 0,

        /// <summary>
        /// Recycle the app pool
        /// </summary>
        Recycle,

        /// <summary>
        /// Custom action - run a script, send an alert, etc
        /// </summary>
        CustomAction
    }
}
