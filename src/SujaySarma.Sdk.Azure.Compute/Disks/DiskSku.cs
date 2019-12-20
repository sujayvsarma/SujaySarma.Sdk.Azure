
using Newtonsoft.Json;
using SujaySarma.Sdk.Azure.Compute.Common;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    public class DiskSku
    {
        /// <summary>
        /// Type of sku
        /// </summary>
        [JsonProperty("name", ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DiskSkuNamesEnum Name { get; set; } = DiskSkuNamesEnum.Standard_LRS;

        /// <summary>
        /// Tier the sku belongs to
        /// </summary>
        [JsonProperty("tier")]
        public string? Tier { get; set; } = null;


        public DiskSku() { }
    }
}
