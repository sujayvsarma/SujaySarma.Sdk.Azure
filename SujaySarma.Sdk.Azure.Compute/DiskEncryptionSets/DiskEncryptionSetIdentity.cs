using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;

namespace SujaySarma.Sdk.Azure.Compute.DiskEncryptionSets
{
    /// <summary>
    /// Identity of an disk encryption set
    /// </summary>
    public class DiskEncryptionSetIdentity
    {
        /// <summary>
        /// The object id of the Managed Identity Resource. 
        /// This will be sent to the RP from ARM via the x-ms-identity-principal-id header 
        /// in the PUT request if the resource has a systemAssigned(implicit) identity
        /// </summary>
        [JsonProperty("principalId")]
        public string? PrincipalId { get; set; }

        /// <summary>
        /// The tenant id of the Managed Identity Resource. 
        /// This will be sent to the RP from ARM via the x-ms-client-tenant-id header 
        /// in the PUT request if the resource has a systemAssigned(implicit) identity
        /// </summary>
        [JsonProperty("tenantId")]
        public string? TenantId { get; set; }

        /// <summary>
        /// The type of Managed Identity used by the DiskEncryptionSet
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public DiskEncryptionSetIdentityTypeNamesEnum Type { get; set; } = DiskEncryptionSetIdentityTypeNamesEnum.SystemAssigned;

        public DiskEncryptionSetIdentity() { }

        /// <summary>
        /// Create a disk encryption identity
        /// </summary>
        /// <param name="tenantId">Azure tenant Id</param>
        /// <param name="principalId">Principal Id of identity (application) in the Azure tenant</param>
        /// <param name="type">Type of identity (we only accept SystemAssigned)</param>
        public DiskEncryptionSetIdentity(Guid tenantId, Guid principalId, DiskEncryptionSetIdentityTypeNamesEnum type)
        {
            if (tenantId == default) { throw new ArgumentNullException(nameof(tenantId)); }
            if (principalId == default) { throw new ArgumentNullException(nameof(principalId)); }
            if (!Enum.IsDefined(typeof(DiskEncryptionSetIdentityTypeNamesEnum), type)) { throw new ArgumentOutOfRangeException(nameof(type)); }

            PrincipalId = principalId.ToString("d");
            TenantId = tenantId.ToString("d");
            Type = type;
        }
    }
}
