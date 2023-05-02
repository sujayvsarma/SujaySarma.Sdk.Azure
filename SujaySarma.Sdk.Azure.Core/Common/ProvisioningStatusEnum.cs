
namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// Status of provisioning
    /// </summary>
    public enum ProvisioningStatusEnum
    {
        /// <summary>
        /// Default value
        /// </summary>
        Default = Succeeded,

        /// <summary>
        /// Successful
        /// </summary>
        Succeeded = 0,

        /// <summary>
        /// Provisioning is in progress
        /// </summary>
        InProgress,

        /// <summary>
        /// Provisioning failed
        /// </summary>
        Failed,

        /// <summary>
        /// In process of being deleted
        /// </summary>
        Deleting,

        /// <summary>
        /// Object has been cancelled
        /// </summary>
        Canceled
    }
}