using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Virtual application configured on an Azure App Service app
    /// </summary>
    public class AppServiceWebAppVirtualApplication
    {
        /// <summary>
        /// Absolute physical path (eg: "C:\wwwroot")
        /// </summary>
        [JsonProperty("physicalPath")]
        public string PhysicalPath { get; set; } = string.Empty;

        /// <summary>
        /// Is preloading the app (during app start) enabled
        /// </summary>
        [JsonProperty("preloadEnabled")]
        public bool IsPreloadingEnabled { get; set; } = false;

        /// <summary>
        /// Virtual path ("/")
        /// </summary>
        [JsonProperty("virtualPath")]
        public string VirtualPath { get; set; } = string.Empty;

        /// <summary>
        /// Virtual directories for virtual application
        /// </summary>
        [JsonProperty("virtualDirectories")]
        public List<AppServiceWebAppVirtualAppDirectory> VirtualDirectories { get; set; } = new List<AppServiceWebAppVirtualAppDirectory>();

    }

}
