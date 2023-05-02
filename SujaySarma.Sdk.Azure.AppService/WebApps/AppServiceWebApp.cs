using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// An Azure App Service Web App
    /// </summary>
    public class AppServiceWebApp : AzureObjectBase
    {

        /// <summary>
        /// Identity of the web app
        /// </summary>
        [JsonProperty("identity")]
        public ResourceIdentity? Identity { get; set; } = null;

        /// <summary>
        /// Properties of a web app
        /// </summary>
        [JsonProperty("properties")]
        public AppServiceWebAppProperties Properties { get; set; } = new AppServiceWebAppProperties();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceWebApp() { }


        public static AppServiceWebApp New(string name, string resourceGroupName, string regionLocationCode)
        {
            return new AppServiceWebApp();
        }
    }

}
