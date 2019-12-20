using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [JsonProperty("type", ItemConverterType = typeof(StringEnumConverter))]
        public DiskEncryptionSetIdentityTypeNamesEnum Type { get; set; } = DiskEncryptionSetIdentityTypeNamesEnum.SystemAssigned;

    }
}
