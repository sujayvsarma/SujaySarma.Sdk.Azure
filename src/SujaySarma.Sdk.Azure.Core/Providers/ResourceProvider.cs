using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Providers
{
    /// <summary>
    /// Metadata about a single resource provider. This is purely a return-data structure and there 
    /// are no flows where this is instantiated in the client's code.
    /// </summary>
    public class ResourceProvider
    {

        /// <summary>
        /// ID URI of the provider
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Namespace of the provider
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Status of registration in the tenant/subscription. One of: 
        /// NotRegistered, Registered
        /// </summary>
        [JsonProperty("registrationState")]
        public string RegistrationState { get; set; }

        /// <summary>
        /// Indicates if registration into the tenant is required before it can be used. One of:
        /// RegistrationRequired, RegistrationFree
        /// </summary>
        [JsonProperty("registrationPolicy")]
        public string RegistrationPolicy { get; set; }

        /// <summary>
        /// Types of resources supported by this provider
        /// </summary>
        [JsonProperty("resourceTypes")]
        public List<ProviderResourceType> ResourceTypes { get; set; }

        /// <summary>
        /// Other information
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> Metadata { get; set; }

        public ResourceProvider()
        {
            Id = "/subscriptions/00000000-0000-0000-0000-000000000000/providers/SujaySarma.Sdk.Azure";
            Namespace = "SujaySarma.Sdk.Azure";
            RegistrationState = "NotRegistered";
            RegistrationPolicy = "RegistrationFree";
            ResourceTypes = new List<ProviderResourceType>();
            Metadata = new Dictionary<string, object>();
        }
    }
}
