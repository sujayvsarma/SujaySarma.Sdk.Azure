namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Progress status of blob restore process
    /// </summary>
    public enum BlobRestoreProgress
    {
        /// <summary>
        /// Process is complete
        /// </summary>
        Complete,

        /// <summary>
        /// Restore failed
        /// </summary>
        Failed,

        /// <summary>
        /// Operation is in progress
        /// </summary>
        InProgress
    }



}
