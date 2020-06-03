using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Compute.Common;

using System;

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

        /// <summary>
        /// Build a Disk SKU
        /// </summary>
        /// <param name="sku">Name of the SKU</param>
        /// <param name="tier">Tier of the SKU</param>
        public DiskSku(DiskSkuNamesEnum sku, string? tier = null)
        {
            if (!Enum.IsDefined(typeof(DiskSkuNamesEnum), sku)) { throw new ArgumentOutOfRangeException(nameof(sku)); }

            Name = sku;
            Tier = tier;
        }
    }
}
