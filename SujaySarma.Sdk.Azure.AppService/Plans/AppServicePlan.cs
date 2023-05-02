using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.Plans
{
    /// <summary>
    /// An app service plan
    /// </summary>
    public class AppServicePlan : AzureObjectBase
    {

        /// <summary>
        /// Properties of the plan
        /// </summary>
        [JsonProperty("properties")]
        public AppServicePlanProperties Properties { get; set; } = new AppServicePlanProperties();

        /// <summary>
        /// Information about the SKU used to provision this plan
        /// </summary>
        [JsonProperty("sku")]
        public ResourceSku Sku { get; set; } = new ResourceSku();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServicePlan() : base() { }
    }
}
