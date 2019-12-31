namespace SujaySarma.Sdk.Azure.Compute.Common
{
    /// <summary>
    /// Types of disk SKUs
    /// </summary>
    public enum DiskSkuNamesEnum
    {
        /// <summary>
        /// Standard (with standard hard disk) SKU with locally redundant storage
        /// </summary>
        Standard_LRS,

        /// <summary>
        /// Standard (with SSD) SKU with locally redundant storage
        /// </summary>
        StandardSSD_LRS,

        /// <summary>
        /// Premium with locally redundant storage
        /// </summary>
        Premium_LRS,

        /// <summary>
        /// Ultra SSD SKU with locally redundant storage
        /// </summary>
        UltraSSD_LRS,

        /// <summary>
        /// Standard SKU with zone-redundant storage
        /// </summary>
        Standard_ZRS
    }
}
