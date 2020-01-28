using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Azure App Service site limits
    /// </summary>
    public class AppServiceWebAppSiteLimits
    {
        /// <summary>
        /// Maximum allowed disk size in MB
        /// </summary>
        [JsonProperty("maxDiskSizeInMb")]
        public int MaximumDiskSizeInMB { get; set; }

        /// <summary>
        /// Maximum allowed memory size in MB
        /// </summary>
        [JsonProperty("maxMemoryInMb")]
        public int MaximumMemorySizeInMB { get; set; }

        /// <summary>
        /// Maximum allowed CPU percentage
        /// </summary>
        [JsonProperty("maxPercentageCpu")]
        public int MaximumCpuPercentage { get; set; }
    }
}
