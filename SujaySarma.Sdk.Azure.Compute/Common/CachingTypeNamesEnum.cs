namespace SujaySarma.Sdk.Azure.Compute.Common
{
    /// <summary>
    /// Type of caching requirements
    /// </summary>
    public enum CachingTypeNamesEnum
    {
        /// <summary>
        /// No caching. Default for "Standard" storage types.
        /// </summary>
        None = 0,

        /// <summary>
        /// Only reads are cached. Default for "Premium" storage types.
        /// </summary>
        ReadOnly,

        /// <summary>
        /// Both reads and writes are cached. 
        /// </summary>
        ReadWrite

    }
}
