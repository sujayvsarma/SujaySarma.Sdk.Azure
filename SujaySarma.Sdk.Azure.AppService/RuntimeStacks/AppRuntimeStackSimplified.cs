namespace SujaySarma.Sdk.Azure.AppService.RuntimeStacks
{
    /// <summary>
    /// This is a simplified version of the ApplicationStack class, to make it easier 
    /// to pass in as arguments to various methods
    /// </summary>
    public class AppRuntimeStackSimplified
    {

        /// <summary>
        /// The internal name of the stack
        /// </summary>
        public string InternalName { get; set; } = string.Empty;

        /// <summary>
        /// Major version number
        /// </summary>
        public string MajorVersion { get; set; } = string.Empty;

        /// <summary>
        /// Minor version number
        /// </summary>
        public string MinorVersion { get; set; } = string.Empty;

        /// <summary>
        /// If remote debugging enabled
        /// </summary>
        public bool IsRemoteDebuggingEnabled { get; set; } = false;

        /// <summary>
        /// If app insights is supported
        /// </summary>
        public bool IsAppInsightsSupported { get; set; } = false;


        public bool IsDotNet => (InternalName == "aspnet");


        /// <summary>
        /// Create an instance
        /// </summary>
        /// <param name="internalName">Internal name (ApplicationStack.Properties.InternalName)</param>
        /// <param name="majorVersion">Selected major version instance</param>
        /// <param name="minorVersion">Selected minor version instance</param>
        public AppRuntimeStackSimplified(string internalName, ApplicationStackMajorVersion majorVersion, ApplicationStackMinorVersion minorVersion)
        {
            InternalName = internalName;
            MajorVersion = majorVersion.InternalVersion ?? string.Empty;
            MinorVersion = minorVersion.InternalVersion ?? string.Empty;
            IsRemoteDebuggingEnabled = minorVersion.IsRemoteDebuggingSupported;
            IsAppInsightsSupported = majorVersion.IsApplicationInsightsSupported;
        }

    }
}
