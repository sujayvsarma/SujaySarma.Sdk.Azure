namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// The type of key used to encrypt the data of the disk
    /// </summary>
    public enum DiskDataEncryptionTypeNamesEnum
    {
        /// <summary>
        /// Disk is encrypted with Customer managed key at rest
        /// </summary>
        EncryptionAtRestWithCustomerKey,

        /// <summary>
        /// Disk is encrypted with XStore managed key at rest. It is the default encryption type
        /// </summary>
        EncryptionAtRestWithPlatformKey
    }
}
