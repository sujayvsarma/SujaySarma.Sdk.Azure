namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// The HTTP pipeline mode for managed runtimes on the App Service
    /// </summary>
    public enum AppServiceWebAppManagedPipelineMode
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = Integrated,


        /// <summary>
        /// ASP/ASP.NET Classic Pipeline
        /// </summary>
        Classic,

        /// <summary>
        /// ASP.NET Integrated pipeline
        /// </summary>
        Integrated = 0

    }
}
