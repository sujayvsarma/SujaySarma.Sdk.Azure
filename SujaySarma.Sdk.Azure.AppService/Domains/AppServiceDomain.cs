using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Existing App Service Domain as retrieved from Azure
    /// </summary>
    public class AppServiceDomain : AzureObjectBase
    {

        /// <summary>
        /// Properties of the domain
        /// </summary>
        [JsonProperty("properties")]
        public AppServiceDomainProperties Properties { get; set; } = new AppServiceDomainProperties();


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceDomain() : base() { }
    }
}
