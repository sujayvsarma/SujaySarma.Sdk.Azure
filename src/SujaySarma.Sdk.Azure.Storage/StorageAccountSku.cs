
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// The SKU of a storage account
    /// </summary>
    public class StorageAccountSku
    {

        /// <summary>
        /// Sku of the storage account
        /// </summary>
        [JsonProperty("name"), JsonConverter(typeof(StringEnumConverter))]
        public StorageAccountSkuNames Name { get; set; } = StorageAccountSkuNames.Standard_LRS;

        /// <summary>
        /// Account tier
        /// </summary>
        [JsonProperty("tier"), JsonConverter(typeof(StringEnumConverter))]
        public StorageAccountSkuTiers Tier { get; set; } = StorageAccountSkuTiers.Standard;

        /// <summary>
        /// Default constructor
        /// </summary>
        public StorageAccountSku() { }

    }
}
