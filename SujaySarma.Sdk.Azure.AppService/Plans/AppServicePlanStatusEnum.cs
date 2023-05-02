namespace SujaySarma.Sdk.Azure.AppService.Plans
{
    /// <summary>
    /// Status of an app service plan
    /// </summary>
    public enum AppServicePlanStatusEnum
    {
        /// <summary>
        /// Default value
        /// </summary>
        Default = Ready,

        /// <summary>
        /// Being created
        /// </summary>
        Creating,

        /// <summary>
        /// Is pending operation
        /// </summary>
        Pending,

        /// <summary>
        /// Ready for use
        /// </summary>
        Ready = 0
    }
}
