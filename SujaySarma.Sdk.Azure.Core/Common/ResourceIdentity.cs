using Newtonsoft.Json;

using System;
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
        public string? PrincipalId { get; set; } = null;

        /// <summary>
        /// ID of the tenant that contains the principal (as defined by PrincipalId)
        /// </summary>
        [JsonProperty("tenantId")]
        public string? TenantId { get; set; } = null;

        /// <summary>
        /// The types of identities assigned to this object
        /// </summary>
        public List<ResourceIdentityTypeEnum> AssignedIdentities
        {
            get
            {
                if ((assignedIdentities.Count == 0) && (!string.IsNullOrWhiteSpace(type)))
                {
                    foreach (string id in type.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if ((id == "UserAssigned") && (!assignedIdentities.Contains(ResourceIdentityTypeEnum.UserAssigned)))
                        {
                            assignedIdentities.Add(ResourceIdentityTypeEnum.UserAssigned);
                        }

                        if ((id == "SystemAssigned") && (!assignedIdentities.Contains(ResourceIdentityTypeEnum.SystemAssigned)))
                        {
                            assignedIdentities.Add(ResourceIdentityTypeEnum.SystemAssigned);
                        }
                    }
                }

                return assignedIdentities;
            }
            set
            {
                assignedIdentities.Clear();
                type = string.Empty;

                foreach (ResourceIdentityTypeEnum id in value)
                {
                    if ((id == ResourceIdentityTypeEnum.UserAssigned) && (!assignedIdentities.Contains(ResourceIdentityTypeEnum.UserAssigned)))
                    {
                        assignedIdentities.Add(ResourceIdentityTypeEnum.UserAssigned);
                        if (type.Length > 0)
                        {
                            type += ",";
                        }
                        type += "UserAssigned";
                    }

                    if ((id == ResourceIdentityTypeEnum.SystemAssigned) && (!assignedIdentities.Contains(ResourceIdentityTypeEnum.SystemAssigned)))
                    {
                        assignedIdentities.Add(ResourceIdentityTypeEnum.SystemAssigned);
                        if (type.Length > 0)
                        {
                            type += ",";
                        }
                        type += "SystemAssigned";
                    }
                }
            }
        }

        /// <summary>
        /// Type of identity. One/both of: SystemAssigned, UserAssigned
        /// </summary>
        [JsonProperty("type")]
        private string? type { get; set; } = null;

        private readonly List<ResourceIdentityTypeEnum> assignedIdentities = new List<ResourceIdentityTypeEnum>();

        /// <summary>
        /// If the identity is user assigned, then the list of them
        /// </summary>
        [JsonProperty("userAssignedIdentities")]
        public Dictionary<string, UserAssignedResourceIdentity>? UserAssignedIdentities { get; set; } = null;

        /// <summary>
        /// Returns if the VM has a user assigned identity
        /// </summary>
        public bool HasUserAssignedIdentity => AssignedIdentities.Contains(ResourceIdentityTypeEnum.UserAssigned);

        /// <summary>
        /// Returns if the VM has a system assigned identity
        /// </summary>
        public bool HasSystemAssignedIdentity => AssignedIdentities.Contains(ResourceIdentityTypeEnum.SystemAssigned);

        /// <summary>
        /// Create a new resource identity
        /// </summary>
        public ResourceIdentity() { }

        /// <summary>
        /// Create a system identity
        /// </summary>
        /// <param name="tenantId">Guid of the tenant where the system principal is homed</param>
        /// <param name="systemPrincipalId">Guid of the system principal</param>
        /// <returns>ResourceIdentity with the appropriate properties set</returns>
        public static ResourceIdentity CreateSystemIdentity(Guid tenantId, Guid systemPrincipalId)
            => new ResourceIdentity()
            {
                TenantId = tenantId.ToString("d"),
                PrincipalId = systemPrincipalId.ToString("d"),
                AssignedIdentities = new List<ResourceIdentityTypeEnum>() { ResourceIdentityTypeEnum.SystemAssigned },
                UserAssignedIdentities = null
            };

        /// <summary>
        /// Create a user-assigned identity
        /// </summary>
        /// <param name="identityName">The name assigned to the user-assigned identity</param>
        /// <param name="clientId">The guid of the application client as registered in the Azure AD</param>
        /// <param name="clientPrincipalId">The guid of the Principal obtained from the application client in the Azure AD</param>
        /// <returns>ResourceIdentity with the appropriate properties set</returns>
        public static ResourceIdentity CreateUserAssignedIdentity(string identityName, Guid clientId, Guid clientPrincipalId)
        {
            if (string.IsNullOrWhiteSpace(identityName)) { throw new ArgumentNullException(nameof(identityName)); }

            return new ResourceIdentity()
            {
                TenantId = null,
                PrincipalId = null,
                AssignedIdentities = new List<ResourceIdentityTypeEnum>() { ResourceIdentityTypeEnum.UserAssigned },
                UserAssignedIdentities = new Dictionary<string, UserAssignedResourceIdentity>()
                {
                    { $"Microsoft.ManagedIdentity/userAssignedIdentities/{identityName}",  new UserAssignedResourceIdentity(clientId, clientPrincipalId) }
                }
            };
        }

        /// <summary>
        /// Add a system identity. A resource may have only ONE system identity at a time, so this will throw if 
        /// there is already a system identity assigned.
        /// </summary>
        /// <param name="tenantId">Guid of the tenant where the system principal is homed</param>
        /// <param name="systemPrincipalId">Guid of the system principal</param>
        public void AddSystemIdentity(Guid tenantId, Guid systemPrincipalId)
        {
            if (HasSystemAssignedIdentity)
            {
                throw new InvalidOperationException("Resource already contains a system assigned identity.");
            }

            TenantId = tenantId.ToString("d");
            PrincipalId = systemPrincipalId.ToString("d");
            assignedIdentities.Add(ResourceIdentityTypeEnum.SystemAssigned);
        }

        /// <summary>
        /// Attach a user-assigned identity
        /// </summary>
        /// <param name="identityName">The name assigned to the user-assigned identity</param>
        /// <param name="clientId">The guid of the application client as registered in the Azure AD</param>
        /// <param name="clientPrincipalId">The guid of the Principal obtained from the application client in the Azure AD</param>
        public void AddUserAssignedIdentity(string identityName, Guid clientId, Guid clientPrincipalId)
        {
            if (string.IsNullOrWhiteSpace(identityName)) { throw new ArgumentNullException(nameof(identityName)); }

            if (UserAssignedIdentities != null)
            {
                // identity name must be unique in collection
                foreach (string idName in UserAssignedIdentities.Keys)
                {
                    if (idName.Equals(identityName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new ArgumentException($"Collection already contains identity with the name {identityName}");
                    }
                }
            }

            if (!assignedIdentities.Contains(ResourceIdentityTypeEnum.UserAssigned))
            {
                assignedIdentities.Add(ResourceIdentityTypeEnum.UserAssigned);
            }

            if (UserAssignedIdentities == null)
            {
                UserAssignedIdentities = new Dictionary<string, UserAssignedResourceIdentity>();
            }

            UserAssignedIdentities.Add($"Microsoft.ManagedIdentity/userAssignedIdentities/{identityName}", new UserAssignedResourceIdentity(clientId, clientPrincipalId));
        }

        /// <summary>
        /// Remove the system identity
        /// </summary>
        public void ClearSystemIdentity()
        {
            if (HasSystemAssignedIdentity)
            {
                TenantId = null;
                PrincipalId = null;
                assignedIdentities.Remove(ResourceIdentityTypeEnum.SystemAssigned);
            }
        }

        /// <summary>
        /// Remove ALL user assigned identities
        /// </summary>
        public void ClearUserAssignedIdentities()
        {
            if (HasUserAssignedIdentity)
            {
                assignedIdentities.Remove(ResourceIdentityTypeEnum.UserAssigned);
                UserAssignedIdentities = null;
            }
        }

        /// <summary>
        /// Remove a single user assigned identity
        /// </summary>
        /// <param name="identityName">Name of the identity</param>
        public void ClearUserAssignedIdentity(string identityName)
        {
            if (string.IsNullOrWhiteSpace(identityName)) { throw new ArgumentNullException(nameof(identityName)); }

            if (HasUserAssignedIdentity && (UserAssignedIdentities != null))
            {
                string idUri = $"Microsoft.ManagedIdentity/userAssignedIdentities/{identityName}";
                UserAssignedIdentities.Remove(idUri);

                if (UserAssignedIdentities.Count == 0)
                {
                    // no more user assigned identities left
                    UserAssignedIdentities = null;
                    assignedIdentities.Remove(ResourceIdentityTypeEnum.UserAssigned);
                }
            }
        }
    }
}
