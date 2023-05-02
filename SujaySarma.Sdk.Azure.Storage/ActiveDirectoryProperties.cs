using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Properties of the AD
    /// </summary>
    public class ActiveDirectoryProperties
    {
        /// <summary>
        /// SID for the Azure storage
        /// </summary>
        [JsonProperty("azureStorageSid")]
        public string? StorageSid { get; set; }

        /// <summary>
        /// Guid of the domain
        /// </summary>
        [JsonProperty("domainGuid")]
        public string? DomainGuid { get; set; }

        /// <summary>
        /// Name of the AD domain
        /// </summary>
        [JsonProperty("domainName")]
        public string? DomainName { get; set; }

        /// <summary>
        /// SID of the AD domain
        /// </summary>
        [JsonProperty("domainSid")]
        public string? DomainSid { get; set; }

        /// <summary>
        /// Name of the AD forest
        /// </summary>
        [JsonProperty("forestName")]
        public string? ADForest { get; set; }

        /// <summary>
        /// NetBIOS domain name
        /// </summary>
        [JsonProperty("netBiosDomainName")]
        public string? NetBIOSName { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ActiveDirectoryProperties() { }
    }



}
