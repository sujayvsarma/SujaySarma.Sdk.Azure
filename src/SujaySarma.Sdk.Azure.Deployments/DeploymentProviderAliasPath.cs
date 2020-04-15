using System.Collections.Generic;

using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Path of an alias
    /// </summary>
    public class DeploymentProviderAliasPath
    {
        /// <summary>
        /// API versions supported
        /// </summary>
        [JsonProperty("apiVersions")]
        public List<string> ApiVersions { get; set; } = new List<string>();

        /// <summary>
        /// Path
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; } = string.Empty;
    }
}
