using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Storage
{
    /// <summary>
    /// Properties of the AD
    /// </summary>
    public class AzureFilesIdentityBasedAuthentication
    {
        [JsonProperty("activeDirectoryProperties")]
        public ActiveDirectoryProperties Properties { get; set; } = new ActiveDirectoryProperties();

        /// <summary>
        /// Type of directory services used
        /// </summary>
        [JsonProperty("directoryServiceOptions"), JsonConverter(typeof(StringEnumConverter))]
        public ActiveDirectoryServiceOptions Options { get; set; } = ActiveDirectoryServiceOptions.None;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AzureFilesIdentityBasedAuthentication() { }
    }



}
