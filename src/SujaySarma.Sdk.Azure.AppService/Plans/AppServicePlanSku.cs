using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.Plans
{
    /// <summary>
    /// A SKU for an App Service Plan
    /// </summary>
    public class AppServicePlanSku
    {

        /// <summary>
        /// Type of resource. Always "Microsoft.Web/serverfarms"
        /// </summary>
        [JsonProperty("resourceType")]
        public string Type { get; set; } = "Microsoft.Web/serverfarms";

        /// <summary>
        /// The SKU information. Only Name and Tier properties will be populated!
        /// </summary>
        [JsonProperty("sku")]
        public ResourceSku Sku { get; set; } = new ResourceSku();

        /// <summary>
        /// Capacity and constraints of this SKU
        /// </summary>
        [JsonProperty("capacity")]
        public AppServicePlanSkuCapacity Capacity { get; set; } = new AppServicePlanSkuCapacity();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServicePlanSku() { }

    }
}
