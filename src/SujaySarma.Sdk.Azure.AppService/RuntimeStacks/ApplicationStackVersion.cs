using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.RuntimeStacks
{
    /// <summary>
    /// This is a base class used to implement both 
    /// ApplicationStackMajorVersion and ApplicationStackMinorVersion classes
    /// </summary>
    public class ApplicationStackVersion
    {
        /// <summary>
        /// Version number displayed on the UI
        /// </summary>
        [JsonProperty("displayVersion")]
        public string DisplayVersion { get; set; } = string.Empty;

        /// <summary>
        /// Internal version code used for the API
        /// </summary>
        [JsonProperty("runtimeVersion")]
        public string? InternalVersion { get; set; }

        /// <summary>
        /// Is the default version used for this stack if one is not specified?
        /// </summary>
        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; } = false;


        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationStackVersion() { }
    }
}
