
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// An Azure Storage Account
    /// </summary>
    public class StorageAccount : AzureObjectBase
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
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public StorageAccountProperties Properties { get; set; } = new StorageAccountProperties();

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccount() : base() => Type = "Microsoft.Storage/storageAccounts";
    }
}
