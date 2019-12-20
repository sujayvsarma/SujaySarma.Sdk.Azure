namespace SujaySarma.Sdk.Azure.Compute.Common
{
    /// <summary>
    /// Level of access required for a disk
    /// </summary>
    public enum AccessLevelEnum
    {
        /// <summary>
        /// No access
        /// </summary>
        None = 0,

        /// <summary>
        /// Read-only
        /// </summary>
        Read,

        /// <summary>
        /// (Read +) Write access
        /// </summary>
        Write
    }
}
