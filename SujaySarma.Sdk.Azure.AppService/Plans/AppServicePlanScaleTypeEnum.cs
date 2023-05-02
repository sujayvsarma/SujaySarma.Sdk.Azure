namespace SujaySarma.Sdk.Azure.AppService.Plans
{
    /// <summary>
    /// Type of scaling applicable to a particular SKU of an App Service Plan
    /// </summary>
    public enum AppServicePlanScaleTypeEnum
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = none,

        /// <summary>
        /// No scaling
        /// </summary>
        none = 0,

        /// <summary>
        /// Must scale manually
        /// </summary>
        manual,

        /// <summary>
        /// Can scale automatically (based on triggers and thresholds)
        /// </summary>
        automatic
    }
}
