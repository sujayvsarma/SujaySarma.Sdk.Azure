namespace SujaySarma.Sdk.Azure.Compute.Common
{
    /// <summary>
    /// States that an OS may exist in, on a disk
    /// </summary>
    public enum OSStateTypeNamesEnum
    {
        /// <summary>
        /// Generalized image. Needs to be provisioned during deployment time
        /// </summary>
        Generalized,

        /// <summary>
        /// Specialized image. Contains already provisioned OS Disk.
        /// </summary>
        Specialized

    }
}
