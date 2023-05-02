using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Custom domain assigned to a blob storage account
    /// </summary>
    public class BlobStorageCustomDomain
    {
        /// <summary>
        /// Name of the custom domain, this is a CNAME in the DNS records
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if indirect domain validation is enabled. Should only be set during a record update process.
        /// </summary>
        [JsonProperty("useSubDomainName")]
        public bool IsIndirectDomainValidationEnabled { get; set; } = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlobStorageCustomDomain() { }
    }



}
