using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Alias of a deployment provider
    /// </summary>
    public class DeploymentProviderAlias
    {
        /// <summary>
        /// Aliased name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Paths of the alias
        /// </summary>
        [JsonProperty("paths")]
        public List<DeploymentProviderAliasPath> Paths { get; set; } = new List<DeploymentProviderAliasPath>();
    }
}
