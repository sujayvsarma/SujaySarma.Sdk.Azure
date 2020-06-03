
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Properties of a storage account
    /// </summary>
    public class StorageAccountProperties
    {
        /// <summary>
        /// Access tier
        /// </summary>
        [JsonProperty("accessTier"), JsonConverter(typeof(StringEnumConverter))]
        public StorageAccessTier AccessTier { get; set; }

        /// <summary>
        /// Azure identity based authentication for File service
        /// </summary>
        [JsonProperty("azureFilesIdentityBasedAuthentication")]
        public AzureFilesIdentityBasedAuthentication? AzureFilesIdentityBasedAuthentication { get; set; }

        /// <summary>
        /// Blob restore status
        /// </summary>
        [JsonProperty("blobRestoreStatus")]
        public BlobRestoreStatus? BlobRestoreStatus { get; set; }

        /// <summary>
        /// Date/time when account was created
        /// </summary>
        [JsonProperty("creationTime")]
        public DateTime CreatedAt { get; set; }

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
        /// If failover is in progress
        /// </summary>
        [JsonProperty("failoverInProgress")]
        public bool IsFailoverInProgress { get; set; }

        /// <summary>
        /// Geo replication statistics
        /// </summary>
        [JsonProperty("geoReplicationStats")]
        public GeoReplicationStatistics? GeoReplicationStatistics { get; set; }

        /// <summary>
        /// Flag indicating if Hierarchical Namespace is enabled
        /// </summary>
        [JsonProperty("isHnsEnabled")]
        public bool IsHierarchicalNamespaceEnabled { get; set; }

        /// <summary>
        /// State of large file shares
        /// </summary>
        [JsonProperty("largeFileSharesState"), JsonConverter(typeof(StringEnumConverter))]
        public LargeFilesShareState LargeFilesShareState { get; set; }

        /// <summary>
        /// Timestamp of the most recent geo-replication failover
        /// </summary>
        [JsonProperty("lastGeoFailoverTime")]
        public DateTime? LastGeoFailoverTime { get; set; }

        /// <summary>
        /// Network ACL rulesets
        /// </summary>
        [JsonProperty("networkAcls")]
        public NetworkRuleSet? NetworkACL { get; set; }

        /// <summary>
        /// Primary endpoints
        /// </summary>
        [JsonProperty("primaryEndpoints")]
        public StorageAccountEndpoints PrimaryEndpoints { get; set; } = new StorageAccountEndpoints();

        /// <summary>
        /// Location of the account's primary storage
        /// </summary>
        [JsonProperty("primaryLocation")]
        public string PrimaryLocation { get; set; } = string.Empty;

        /// <summary>
        /// Private endpoint connections
        /// </summary>
        [JsonProperty("privateEndpointConnections")]
        public List<PrivateEndpointConnection>? PrivateEndpointConnections { get; set; }

        /// <summary>
        /// Provisioning state
        /// </summary>
        [JsonProperty("provisioningState"), JsonConverter(typeof(StringEnumConverter))]
        public ProvisioningState ProvisioningState { get; set; }

        /// <summary>
        /// Routing preference
        /// </summary>
        [JsonProperty("routingPreference")]
        public RoutingPreference RoutingPreference { get; set; } = new RoutingPreference();

        /// <summary>
        /// Secondary endpoints
        /// </summary>
        [JsonProperty("secondaryEndpoints")]
        public StorageAccountEndpoints SecondaryEndpoints { get; set; } = new StorageAccountEndpoints();

        /// <summary>
        /// Location of the account's secondary storage
        /// </summary>
        [JsonProperty("secondaryLocation")]
        public string SecondaryLocation { get; set; } = string.Empty;

        /// <summary>
        /// Status of the primary account
        /// </summary>
        [JsonProperty("statusOfPrimary"), JsonConverter(typeof(StringEnumConverter))]
        public AccountStatus PrimaryStatus { get; set; }

        /// <summary>
        /// Status of the secondary account
        /// </summary>
        [JsonProperty("statusOfSecondary"), JsonConverter(typeof(StringEnumConverter))]
        public AccountStatus SecondaryStatus { get; set; }

        /// <summary>
        /// Flag indicating if the storage should allow only HTTPS traffic
        /// </summary>
        [JsonProperty("supportsHttpsTrafficOnly")]
        public bool IsSupportsHttpsTrafficOnly { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountProperties() { }
    }
}
