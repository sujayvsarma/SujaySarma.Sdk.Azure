using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.RuntimeStacks
{
    /// <summary>
    /// Major version of an application stack
    /// </summary>
    public class ApplicationStackMajorVersion : ApplicationStackVersion
    {
        /// <summary>
        /// True if this version supports app insights
        /// </summary>
        [JsonProperty("applicationInsights")]
        public bool IsApplicationInsightsSupported { get; set; } = false;

        /// <summary>
        /// True if this version has been deprecated
        /// </summary>
        [JsonProperty("isDeprecated")]
        public bool IsDeprecated { get; set; } = false;

        /// <summary>
        /// True if this version should be hidden from the UI
        /// </summary>
        [JsonProperty("isHidden")]
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// True if this is a preview version
        /// </summary>
        [JsonProperty("isPreview")]
        public bool IsPreview { get; set; } = false;

        /// <summary>
        /// Minor versions of the stack
        /// </summary>
        [JsonProperty("minorVersions")]
        public List<ApplicationStackMinorVersion> MinorVersions { get; set; } = new List<ApplicationStackMinorVersion>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationStackMajorVersion() : base() { }
    }
}
