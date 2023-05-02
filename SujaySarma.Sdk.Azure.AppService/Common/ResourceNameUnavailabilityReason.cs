
namespace SujaySarma.Sdk.Azure.AppService.Common
{
    /// <summary>
    /// Reason why an app service resource name is not available
    /// </summary>
    public enum ResourceNameUnavailabilityReason
    {
        /// <summary>
        /// Validation errors
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// Another resource already exists with the same name
        /// </summary>
        AlreadyExists

    }
}