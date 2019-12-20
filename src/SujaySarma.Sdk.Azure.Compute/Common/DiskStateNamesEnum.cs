namespace SujaySarma.Sdk.Azure.Compute.Common
{
    /// <summary>
    /// Names of states of a disk
    /// </summary>
    public enum DiskStateNamesEnum
    {
        /// <summary>
        /// Has an active SAS URI
        /// </summary>
        ActiveSAS,

        /// <summary>
        /// Disk created for an upload operation and a write token is active
        /// </summary>
        ActiveUpload,

        /// <summary>
        /// Attached to a running VM
        /// </summary>
        Attached,

        /// <summary>
        /// Ready to be created for upload by requesting a write token
        /// </summary>
        ReadyToUpload,

        /// <summary>
        /// Attached to a stopped/deallocated VM
        /// </summary>
        Reserved,

        /// <summary>
        /// A free and unused disk that is not attached to anything
        /// </summary>
        Unattached
    }
}
