using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.RuntimeStacks
{
    /// <summary>
    /// Properties of an application stack
    /// </summary>
    public class ApplicationStackProperties
    {
        /// <summary>
        /// Dependency on another stack or pre-requisite (usually NULL)
        /// </summary>
        [JsonProperty("dependency")]
        public string? Dependency { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        [JsonProperty("display")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Internal name -- to be used in provisioning config, etc
        /// </summary>
        [JsonProperty("name")]
        public string InternalName { get; set; } = string.Empty;

        /// <summary>
        /// Major versions of the stack
        /// </summary>
        [JsonProperty("majorVersions")]
        public List<ApplicationStackMajorVersion> MajorVersions { get; set; } = new List<ApplicationStackMajorVersion>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationStackProperties() { }
    }
}
