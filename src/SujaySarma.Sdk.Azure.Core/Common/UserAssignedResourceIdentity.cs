using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// An identity assigned to a resource by a user
    /// </summary>
    public class UserAssignedResourceIdentity
    {

        /// <summary>
        /// Guid of the service principal
        /// </summary>
        [JsonProperty("principalId")]
        public string PrincipalId { get; set; } = string.Empty;

        /// <summary>
        /// Guid of the application client the service principal belongs to
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Create a new user assigned identity
        /// </summary>
        public UserAssignedResourceIdentity() { }

        /// <summary>
        /// Create a new user assigned identity
        /// </summary>
        /// <param name="clientId">Guid of the application client (as registered in Azure AD's Applications blade)</param>
        /// <param name="principalId">The Principal's ID of the Client</param>
        public UserAssignedResourceIdentity(Guid clientId, Guid principalId)
        {
            ClientId = clientId.ToString("d");
            PrincipalId = principalId.ToString("d");
        }

    }
}
