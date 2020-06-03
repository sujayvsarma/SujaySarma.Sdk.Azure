using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.DiskEncryptionSets
{
    /// <summary>
    /// Defines a disk encryption set
    /// </summary>
    public class DiskEncryptionSet : AzureObjectBase
    {
        /// <summary>
        /// The encryption set's identity
        /// </summary>
        [JsonProperty("identity")]
        public DiskEncryptionSetIdentity? Identity { get; set; } = null;

        /// <summary>
        /// Properties of the set
        /// </summary>
        [JsonProperty("properties")]
        public DiskEncryptionSetProperties? Properties { get; set; } = null;


        public DiskEncryptionSet() : base() { }

        /// <summary>
        /// Create a disk encryption set with no managed identity
        /// </summary>
        /// <param name="encryptionKeyKeyVaultUri">The Resource Uri to the KeyVault where the key is interned</param>
        /// <param name="encryptionKeyKeyUri">The absolute Uri to the key in the KeyVault</param>
        public DiskEncryptionSet(ResourceUri encryptionKeyKeyVaultUri, string encryptionKeyKeyUri) => Properties = new DiskEncryptionSetProperties(encryptionKeyKeyVaultUri, encryptionKeyKeyUri);

        /// <summary>
        /// Create a disk encryption set with system-managed identity
        /// </summary>
        /// <param name="systemIdentityTenantId">Azure tenant Id</param>
        /// <param name="systemIdentityPrincipalId">Principal Id of identity (application) in the Azure tenant</param>
        /// <param name="encryptionKeyKeyVaultUri">The Resource Uri to the KeyVault where the key is interned</param>
        /// <param name="encryptionKeyKeyUri">The absolute Uri to the key in the KeyVault</param>
        public DiskEncryptionSet(Guid systemIdentityTenantId, Guid systemIdentityPrincipalId, ResourceUri encryptionKeyKeyVaultUri, string encryptionKeyKeyUri)
            : this(encryptionKeyKeyVaultUri, encryptionKeyKeyUri)
            => Identity = new DiskEncryptionSetIdentity(systemIdentityTenantId, systemIdentityPrincipalId, DiskEncryptionSetIdentityTypeNamesEnum.SystemAssigned);
    }
}
