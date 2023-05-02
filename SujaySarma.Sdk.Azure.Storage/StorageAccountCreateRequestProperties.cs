
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Properties of a storage account creation request
    /// </summary>
    public class StorageAccountCreateRequestProperties
    {
        /// <summary>
        /// Access tier
        /// </summary>
        [JsonProperty("accessTier"), JsonConverter(typeof(StringEnumConverter))]
        public StorageAccessTier AccessTier { get; set; } = StorageAccessTier.Hot;

        /// <summary>
        /// Azure identity based authentication for File service
        /// </summary>
        [JsonProperty("azureFilesIdentityBasedAuthentication")]
        public AzureFilesIdentityBasedAuthentication? AzureFilesIdentityBasedAuthentication { get; set; }

        /// <summary>
        /// If configured, the custom domain attached to the Blob storage account
        /// </summary>
        [JsonProperty("customDomain")]
        public BlobStorageCustomDomain? BlobStorageCustomDomain { get; set; }

        /// <summary>
        /// Encryption properties
        /// </summary>
        [JsonProperty("encryption")]
        public StorageAccountEncryption? Encryption { get; set; }

        /// <summary>
        /// Flag indicating if Hierarchical Namespace is enabled
        /// </summary>
        [JsonProperty("isHnsEnabled")]
        public bool IsHierarchicalNamespaceEnabled { get; set; } = false;

        /// <summary>
        /// State of large file shares
        /// </summary>
        [JsonProperty("largeFileSharesState"), JsonConverter(typeof(StringEnumConverter))]
        public LargeFilesShareState LargeFilesShareState { get; set; } = LargeFilesShareState.Disabled;

        /// <summary>
        /// Network ACL rulesets
        /// </summary>
        [JsonProperty("networkAcls")]
        public NetworkRuleSet? NetworkACL { get; set; }

        /// <summary>
        /// Routing preference
        /// </summary>
        [JsonProperty("routingPreference")]
        public RoutingPreference? RoutingPreference { get; set; }

        /// <summary>
        /// Flag indicating if the storage should allow only HTTPS traffic
        /// </summary>
        [JsonProperty("supportsHttpsTrafficOnly")]
        public bool IsSupportsHttpsTrafficOnly { get; set; } = true;

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountCreateRequestProperties() { }
    }
}
