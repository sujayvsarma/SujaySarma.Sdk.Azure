
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Request to create a storage account
    /// </summary>
    public class StorageAccountCreateRequest
    {
        /// <summary>
        /// Identity of the resource
        /// </summary>
        [JsonProperty("identity")]
        public ResourceIdentity? Identity { get; set; }

        /// <summary>
        /// Kind of account
        /// </summary>
        [JsonProperty("kind"), JsonConverter(typeof(StringEnumConverter))]
        public StorageAccountKind Kind { get; set; }

        /// <summary>
        /// Sku of the storage account
        /// </summary>
        [JsonProperty("sku")]
        public StorageAccountSku Sku { get; set; } = new StorageAccountSku();

        /// <summary>
        /// Azure data center location where this is located
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, string>? Tags { get; set; } = null;

        /// <summary>
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public StorageAccountCreateRequestProperties? Properties { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountCreateRequest() { }
    }
}
