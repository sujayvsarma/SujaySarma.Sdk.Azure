namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Status of georeplication
    /// </summary>
    public enum GeoReplicationStatus
    {
        /// <summary>
        /// Initial sync is in progress
        /// </summary>
        Bootstrap,

        /// <summary>
        /// Secondary location is active and operational
        /// </summary>
        Live,

        /// <summary>
        /// Secondary location is not available
        /// </summary>
        Unavailable
    }
}
