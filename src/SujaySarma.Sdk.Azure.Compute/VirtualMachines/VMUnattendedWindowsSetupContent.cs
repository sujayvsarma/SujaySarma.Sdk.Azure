using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Additional setup content for Windows
    /// </summary>
    public class VMUnattendedWindowsSetupContent
    {
        /// <summary>
        /// Name of the target component. Only one value is allowed "Microsoft-Windows-Shell-Setup"
        /// </summary>
        [JsonProperty("componentName")]
        public string ComponentName { get; set; } = "Microsoft-Windows-Shell-Setup";

        /// <summary>
        /// Xml-formatted content, max size is 4KB, including the root element for the setting/feature.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Name of the setup pass that should execute the content. Only one is allowed: "OobeSystem".
        /// </summary>
        [JsonProperty("passName")]
        public string PassName { get; set; } = "OobeSystem";

        /// <summary>
        /// When are these commands to be run?
        /// </summary>
        [JsonProperty("settingName"), JsonConverter(typeof(StringEnumConverter))]
        public VMUnattendedWindowsSetupRunAtEnum RunWhen { get; set; } = VMUnattendedWindowsSetupRunAtEnum.FirstLogonCommands;

        public VMUnattendedWindowsSetupContent() { }
    }
}
