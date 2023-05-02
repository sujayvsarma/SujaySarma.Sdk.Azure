namespace SujaySarma.Sdk.Azure.Compute.Common
{
    /// <summary>
    /// Possible sources of a disk's creation
    /// </summary>
    public enum DiskCreationOptionsEnum
    {
        /// <summary>
        /// Disk will be attached to a VM
        /// </summary>
        Attach,

        /// <summary>
        /// Create a new disk or snapshot by copying from a disk or snapshot specified by the given sourceResourceId.
        /// </summary>
        Copy,

        /// <summary>
        /// Create an empty data disk of a size given by diskSizeGB
        /// </summary>
        Empty,

        /// <summary>
        /// Create a new disk from a platform image specified by the given imageReference
        /// </summary>
        FromImage,

        /// <summary>
        /// Create a disk by importing from a blob specified by a sourceUri in a storage account specified by storageAccountId.
        /// </summary>
        Import,

        /// <summary>
        /// Create a new disk by copying from a backup recovery point
        /// </summary>
        Restore,

        /// <summary>
        /// Create a new disk by obtaining a write token and using it to directly upload the contents of the disk
        /// </summary>
        Upload
    }
}
