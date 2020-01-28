using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Virtual directory mapped into an App Service web app's 
    /// virtual application
    /// </summary>
    public class AppServiceWebAppVirtualAppDirectory
    {
        /// <summary>
        /// The absolute physical path (eg: "C:\wwwroot")
        /// </summary>
        [JsonProperty("physicalPath")]
        public string PhysicalPath { get; set; } = string.Empty;

        /// <summary>
        /// The virtual path (eg: "/")
        /// </summary>
        [JsonProperty("virtualPath")]
        public string VirtualPath { get; set; } = string.Empty;
    }

}
