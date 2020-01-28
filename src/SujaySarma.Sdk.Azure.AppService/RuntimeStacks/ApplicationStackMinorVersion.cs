using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.RuntimeStacks
{
    /// <summary>
    /// Minor version of an application stack
    /// </summary>
    public class ApplicationStackMinorVersion : ApplicationStackVersion
    {

        /// <summary>
        /// True if supports remote debugging
        /// </summary>
        [JsonProperty("isRemoteDebuggingEnabled")]
        public bool IsRemoteDebuggingSupported { get; set; } = false;


        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationStackMinorVersion() : base() { }
    }
}
