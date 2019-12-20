using Newtonsoft.Json;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// Identity of a resource
    /// </summary>
    public class ResourceIdentity
    {
        /// <summary>
        /// ID of the principal for the resource identity
        /// </summary>
        [JsonProperty("principalId")]
        public string PrincipalId { get; set; } = string.Empty;

        /// <summary>
        /// ID of the tenant that contains the principal (as defined by PrincipalId)
        /// </summary>
        [JsonProperty("tenantId")]
        public string TenantId { get; set; } = string.Empty;

        /// <summary>
        /// Type of identity. One/both of: SystemAssigned, UserAssigned
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; } = "UserAssigned";

        /// <summary>
        /// If the identity is user assigned, then the list of them
        /// </summary>
        [JsonProperty("userAssignedIdentities")]
        public Dictionary<string, string>? UserAssignedIdentities { get; set; }

        /// <summary>
        /// Returns if the VM has a user assigned identity
        /// </summary>
        public bool HasUserAssignedIdentity => (string.IsNullOrWhiteSpace(Type) || (Type.Contains("UserAssigned", System.StringComparison.InvariantCultureIgnoreCase)));

        /// <summary>
        /// Returns if the VM has a system assigned identity
        /// </summary>
        public bool HasSystemAssignedIdentity => (string.IsNullOrWhiteSpace(Type) || (Type.Contains("SystemAssigned", System.StringComparison.InvariantCultureIgnoreCase)));

        public ResourceIdentity() { }
    }
}
