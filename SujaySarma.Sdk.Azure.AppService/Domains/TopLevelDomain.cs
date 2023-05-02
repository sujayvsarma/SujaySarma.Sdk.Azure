using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Describes a top level domain (TLD) that is supported by Azure App Service Domain 
    /// purchasing system.
    /// </summary>
    public class TopLevelDomain : AzureObjectBase
    {

        /// <summary>
        /// Properties of the TLD
        /// </summary>
        [JsonProperty("properties")]
        public TopLevelDomainProperties Properties { get; set; } = new TopLevelDomainProperties();

        /// <summary>
        /// Default constructor
        /// </summary>
        public TopLevelDomain() { }
    }
}
