using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.WebApps;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    /// <summary>
    /// This class helps provision and manage ONLY WEBSITE apps via the Azure App Service. 
    /// Use the other specific clients to manage functions and other types of supported apps.
    /// </summary>
    public static class AppServiceWebAppClient
    {
        /// <summary>
        /// Create a new website app
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription Guid for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the app is to be created in</param>
        /// <param name="app">The app</param>
        /// <returns>Metadata about the app in creation, or NULL</returns>
        /// <remarks>
        ///     Web Apps have **so many** ifs and buts in their configuration that we don't want to have a thousand overloaded 
        ///     methods. Instead, let the caller pass in a preconfigured App instance and we just pass it on to the API.
        /// </remarks>
        public static async Task<AppServiceWebApp?> CreateOrUpdate(string bearerToken, Guid subscription, string resourceGroupName, AppServiceWebApp app)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (app == null) { throw new ArgumentNullException(nameof(app)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{app.Name}",
                    CLIENT_API_VERSION,
                    null, app,
                    new int[] { 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServiceWebApp>(response.Body);
        }

        /// <summary>
        /// Delete the web app
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="appUri">ResourceUri to the web app</param>
        /// <param name="alsoDeleteMetrics">Set TRUE to also delete the web app metrics</param>
        /// <param name="alsoDeleteAppServicePlanIfEmpty">Set TRUE to also delete the App Service Plan if there are no more apps left in the plan</param>
        /// <returns>True if the app was accepted for deletion, false if it failed validation/etc, NULL if there was an exception</returns>
        public static async Task<bool?> Delete(string bearerToken, ResourceUri appUri, bool alsoDeleteMetrics = false, bool alsoDeleteAppServicePlanIfEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{appUri.ToAbsoluteAzureRMEndpointUri()}",
                    CLIENT_API_VERSION,
                    new Dictionary<string, string>()
                    {
                        { "deleteMetrics", (alsoDeleteMetrics ? "true" : "false") },
                        { "deleteEmptyServerFarm", (alsoDeleteAppServicePlanIfEmpty ? "true" : "false") }
                    },
                    new int[] { 200, 204, 404 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return null;
            }

            return response.IsExpectedSuccess;
        }

        /// <summary>
        /// Get an instance of a web app
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="appServiceUri">Resource Uri to the app service</param>
        /// <param name="slotName">(Optional) Name of the slot to fetch</param>
        /// <returns>App service or NULL</returns>
        public static async Task<AppServiceWebApp?> Get(string bearerToken, ResourceUri appServiceUri, string? slotName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((!appServiceUri.IsValid) || (!appServiceUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Web")) || (!appServiceUri.Is(ResourceUriCompareLevel.Type, "sites")))
            {
                throw new ArgumentException(nameof(appServiceUri));
            }

            string endpoint = string.Empty;
            if (!string.IsNullOrWhiteSpace(slotName))
            {
                endpoint = $"slots/{slotName}";
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    appServiceUri.ToAbsoluteAzureRMEndpointUri(endpoint),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 404 }
                );

            if ((!response.IsExpectedSuccess) || (response.HttpStatus == 404) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServiceWebApp>(response.Body);
        }

        /// <summary>
        /// Get all web apps in the subscription or resource group
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription guid</param>
        /// <param name="resourceGroup">(optional) Name or Guid of the resource group containing the app services</param>
        /// <returns>List of App services or empty list</returns>
        public static async Task<IList<AppServiceWebApp>> List(string bearerToken, Guid subscription, string? resourceGroup = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }

            ResourceUri uri = new ResourceUri(subscription, resourceGroup, "Microsoft.Web", "sites");

            RestApiResponse response = await RestApiClient.GETWithContinuations<AppServiceWebApp>(
                    bearerToken,
                    uri.ToAbsoluteAzureRMEndpointUri(),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<AppServiceWebApp>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<AppServiceWebApp>>(response.Body).Values;
        }

        /// <summary>
        /// Restart a web app, or optionally a specific slot of the app
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="appServiceUri">Resource Uri to the app service</param>
        /// <param name="slotName">(Optional) Name of the slot to restart -- if unspecified, the entire app is restarted</param>
        /// <param name="isSoftRestart">If true, performs a soft-restart, otherwise app is reprovisioned</param>
        /// <param name="blockTillRestarted">If true, call returns only after the app has been restarted</param>
        /// <returns>True if the operation was accepted, FALSE if not, NULL if there was a problem</returns>
        public static async Task<bool?> Restart(string bearerToken, ResourceUri appServiceUri, string? slotName = null, bool isSoftRestart = true, bool blockTillRestarted = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((!appServiceUri.IsValid) || (!appServiceUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Web")) || (!appServiceUri.Is(ResourceUriCompareLevel.Type, "sites")))
            {
                throw new ArgumentException(nameof(appServiceUri));
            }

            string endpoint = "restart";
            if (!string.IsNullOrWhiteSpace(slotName))
            {
                endpoint = $"slots/{slotName}/restart";
            }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    appServiceUri.ToAbsoluteAzureRMEndpointUri(endpoint),
                    CLIENT_API_VERSION,
                    new Dictionary<string, string>()
                    {
                        { "softRestart", (isSoftRestart ? "true" : "false" ) },
                        { "synchronous", (blockTillRestarted ? "true" : "false" ) }
                    },
                    null,
                    new int[] { 200 }
                );

            if (response.WasException)
            {
                return null;
            }

            return response.IsExpectedSuccess;
        }

        /// <summary>
        /// Starts a web app, or optionally a specific slot of the app
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="appServiceUri">Resource Uri to the app service</param>
        /// <param name="slotName">(Optional) Name of the slot to start -- if unspecified, the entire app is started</param>
        /// <returns>True if the operation was accepted, FALSE if not, NULL if there was a problem</returns>
        public static async Task<bool?> Start(string bearerToken, ResourceUri appServiceUri, string? slotName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((!appServiceUri.IsValid) || (!appServiceUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Web")) || (!appServiceUri.Is(ResourceUriCompareLevel.Type, "sites")))
            {
                throw new ArgumentException(nameof(appServiceUri));
            }

            string endpoint = "start";
            if (!string.IsNullOrWhiteSpace(slotName))
            {
                endpoint = $"slots/{slotName}/start";
            }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    appServiceUri.ToAbsoluteAzureRMEndpointUri(endpoint),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if (response.WasException)
            {
                return null;
            }

            return response.IsExpectedSuccess;
        }

        /// <summary>
        /// Stop a web app, or optionally a specific slot of the app
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="appServiceUri">Resource Uri to the app service</param>
        /// <param name="slotName">(Optional) Name of the slot to stop -- if unspecified, the entire app is stopped</param>
        /// <returns>True if the operation was accepted, FALSE if not, NULL if there was a problem</returns>
        public static async Task<bool?> Stop(string bearerToken, ResourceUri appServiceUri, string? slotName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((!appServiceUri.IsValid) || (!appServiceUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Web")) || (!appServiceUri.Is(ResourceUriCompareLevel.Type, "sites")))
            {
                throw new ArgumentException(nameof(appServiceUri));
            }

            string endpoint = "stop";
            if (!string.IsNullOrWhiteSpace(slotName))
            {
                endpoint = $"slots/{slotName}/stop";
            }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    appServiceUri.ToAbsoluteAzureRMEndpointUri(endpoint),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if (response.WasException)
            {
                return null;
            }

            return response.IsExpectedSuccess;
        }





        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
