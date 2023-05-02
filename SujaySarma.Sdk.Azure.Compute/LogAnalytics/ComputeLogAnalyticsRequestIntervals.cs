namespace SujaySarma.Sdk.Azure.Compute.LogAnalytics
{
    /// <summary>
    /// Length of log required (calculated from starting time)
    /// </summary>
    public enum ComputeLogAnalyticsRequestIntervals
    {
        /// <summary>
        /// 3 minutes
        /// </summary>
        ThreeMins,

        /// <summary>
        /// 5 minutes
        /// </summary>
        FiveMins,

        /// <summary>
        /// 30 minutes
        /// </summary>
        ThirtyMins,

        /// <summary>
        /// 60 minutes
        /// </summary>
        SixtyMins
    }
}
