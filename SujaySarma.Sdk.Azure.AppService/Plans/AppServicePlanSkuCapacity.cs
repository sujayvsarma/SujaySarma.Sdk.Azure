using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.AppService.Plans
{
    /// <summary>
    /// Capacity and constraints of a SKU
    /// </summary>
    public class AppServicePlanSkuCapacity
    {
        /// <summary>
        /// Minimum number of instances (can be zero!)
        /// </summary>
        [JsonProperty("minimum")]
        public int MinimumInstances { get; set; } = 0;

        /// <summary>
        /// Maximum number of instances (can be zero!)
        /// </summary>
        [JsonProperty("maximum")]
        public int MaximumInstances { get; set; } = 0;

        /// <summary>
        /// Maximum number of instances when elastic scaling is applied. 
        /// NULL for SKUs that don't support elastic scaling, for others will have a number >= zero.
        /// </summary>
        [JsonProperty("elasticMaximum")]
        public int? ElasticScalingMaximumInstances { get; set; } = null;

        /// <summary>
        /// Default number of instances (can be zero!)
        /// </summary>
        [JsonProperty("default")]
        public int DefaultNumberOfInstances { get; set; } = 0;

        /// <summary>
        /// Type of scaling that can be applied
        /// </summary>
        [JsonProperty("scaleType"), JsonConverter(typeof(StringEnumConverter))]
        public AppServicePlanScaleTypeEnum ScaleType { get; set; } = AppServicePlanScaleTypeEnum.Default;


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServicePlanSkuCapacity() { }
    }
}
