using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.Plans;
using SujaySarma.Sdk.Azure.AppService.WebApps;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    /// <summary>
    /// Interacts wtih the App Service Plans endpoint of the App Services module
    /// </summary>
    public static class AppServicePlanClient
    {


        /// <summary>
        /// Get metadata about a single App Service Plan
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <returns>AppServicePlan or NULL</returns>
        public static async Task<AppServicePlan?> GetAppServicePlan(string bearerToken, Guid subscription, string resourceGroupName, string planName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServicePlan>(response.Body);
        }

        /// <summary>
        /// Get metadata for all app service plans in the provided resource group
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="detailed">Set to TRUE to fetch additional metadata (sometimes may take longer)</param>
        /// <returns>List of plans or empty list</returns>
        public static async Task<IList<AppServicePlan>> GetAppServicePlans(string bearerToken, Guid subscription, string? resourceGroupName = null, bool detailed = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<AppServicePlan>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}{(string.IsNullOrWhiteSpace(resourceGroupName) ? "" : $"/resourceGroups/{resourceGroupName}")}/providers/Microsoft.Web/serverfarms",
                    CLIENT_API_VERSION,
                    new Dictionary<string, string>()
                    {
                        { "detailed", (detailed ? "true" : "false") }
                    }, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<AppServicePlan>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<AppServicePlan>>(response.Body).Values;
        }

        /// <summary>
        /// Get the capabilities of an App Service Plan.
        /// Operation will be completed asynchornously.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <returns>Dictionary of capabilities. Key is the name, and Value is the state of that (usually "Enabled")</returns>
        public static async Task<IDictionary<string, string>> GetAppServicePlanCapabilities(string bearerToken, Guid subscription, string resourceGroupName, string planName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<AppServicePlan>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}/capabilities",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new Dictionary<string, string>();
            }

            IList<ExtensibleAzureObject> objList = JsonConvert.DeserializeObject<ListResultWithContinuations<ExtensibleAzureObject>>(response.Body).Values;
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (ExtensibleAzureObject obj in objList)
            {
                result.Add((string)obj.Properties["name"]!, (string)obj.Properties["value"]!);
            }

            return result;
        }

        /// <summary>
        /// Get usages (metrics) for the App Service Plan
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <returns>List of metrics or empty list</returns>
        public static async Task<IList<UsageSdkResponseItem>> GetAppServicePlanUsage(string bearerToken, Guid subscription, string resourceGroupName, string planName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<AppServicePlan>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}/usages",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<UsageSdkResponseItem>();
            }

            IList<UsageAPIResponseItem> objList = JsonConvert.DeserializeObject<ListResultWithContinuations<UsageAPIResponseItem>>(response.Body).Values;
            List<UsageSdkResponseItem> results = new List<UsageSdkResponseItem>();
            foreach (UsageAPIResponseItem api in objList)
            {
                results.Add(new UsageSdkResponseItem(api));
            }

            return results;
        }

        /// <summary>
        /// Create a standard App Service Plan. This plan cannot make Spot reservations or make use of elastic scaling.
        /// Operation will be completed asynchornously.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <param name="locationCode">Geographic location code (eg: "uswest") of the location where this VM is to be created.</param>
        /// <param name="operatingSystem">Host operating system. Cannot be changed once the plan is created.</param>
        /// <param name="sku">The App Service plan SKU to use (use MiscServicesClient.GetAvailableSkus)</param>
        /// <param name="tags">Tags to attach</param>
        /// <returns>The created App Service Plan or NULL</returns>
        public static async Task<AppServicePlan?> CreateStandardPlan(string bearerToken, Guid subscription, string resourceGroupName, string planName,
            string locationCode, OSTypeNamesEnum operatingSystem, ResourceSku sku, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }
            if (string.IsNullOrWhiteSpace(locationCode)) { throw new ArgumentNullException(nameof(locationCode)); }
            if (!Enum.IsDefined(typeof(OSTypeNamesEnum), operatingSystem)) { throw new ArgumentOutOfRangeException(nameof(operatingSystem)); }
            if (sku == null) { throw new ArgumentNullException(nameof(sku)); }

            AppServicePlan request = new AppServicePlan()
            {
                Location = locationCode,
                Sku = sku,
                Tags = tags,

                Properties = new AppServicePlanProperties()
                {
                    IsHyperVContainerApp = false,
                    IsSpotAppsEnabled = false,
                    ApplyScalingPerSite = false,
                    IsLinuxAppPlan = (operatingSystem == OSTypeNamesEnum.Linux),
                }
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServicePlan>(response.Body);
        }

        /// <summary>
        /// Create an App Service Plan whose instances can scale elastically
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <param name="locationCode">Geographic location code (eg: "uswest") of the location where this VM is to be created.</param>
        /// <param name="operatingSystem">Host operating system. Cannot be changed once the plan is created.</param>
        /// <param name="sku">The App Service plan SKU to use (use MiscServicesClient.GetAvailableSkus)</param>
        /// <param name="maximumWorkerCount">Maximum number of elastic workers</param>
        /// <param name="enablePerSiteScaling">Enable per-site scaling. Otherwise, all sites will scale equally!</param>
        /// <param name="tags">Tags to attach</param>
        /// <returns>The created App Service Plan or NULL</returns>
        public static async Task<AppServicePlan?> CreateElasticScalingPlan(string bearerToken, Guid subscription, string resourceGroupName, string planName,
            string locationCode, OSTypeNamesEnum operatingSystem, ResourceSku sku, int maximumWorkerCount, bool enablePerSiteScaling, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }
            if (string.IsNullOrWhiteSpace(locationCode)) { throw new ArgumentNullException(nameof(locationCode)); }
            if (!Enum.IsDefined(typeof(OSTypeNamesEnum), operatingSystem)) { throw new ArgumentOutOfRangeException(nameof(operatingSystem)); }
            if (sku == null) { throw new ArgumentNullException(nameof(sku)); }
            if (maximumWorkerCount < 1) { throw new ArgumentNullException(nameof(maximumWorkerCount)); }

            AppServicePlan request = new AppServicePlan()
            {
                Location = locationCode,
                Sku = sku,
                Tags = tags,

                Properties = new AppServicePlanProperties()
                {
                    IsHyperVContainerApp = false,
                    IsSpotAppsEnabled = false,
                    ApplyScalingPerSite = enablePerSiteScaling,
                    MaximumElasticWorkers = maximumWorkerCount,
                    IsLinuxAppPlan = (operatingSystem == OSTypeNamesEnum.Linux),
                }
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServicePlan>(response.Body);
        }

        /// <summary>
        /// Create an App Service Plan that can bid for and create Spot instances.
        /// Operation will be completed asynchornously.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <param name="locationCode">Geographic location code (eg: "uswest") of the location where this VM is to be created.</param>
        /// <param name="operatingSystem">Host operating system. Cannot be changed once the plan is created.</param>
        /// <param name="sku">The App Service plan SKU to use (use MiscServicesClient.GetAvailableSkus)</param>
        /// <param name="maximumWorkerCount">Maximum number of elastic workers</param>
        /// <param name="enablePerSiteScaling">Enable per-site scaling. Otherwise, all sites will scale equally!</param>
        /// <param name="spotExpirationTime">Date/time when the spots will expire</param>
        /// <param name="tags">Tags to attach</param>
        /// <returns>The created App Service Plan or NULL</returns>
        public static async Task<AppServicePlan?> CreateSpotPlan(string bearerToken, Guid subscription, string resourceGroupName, string planName,
            string locationCode, OSTypeNamesEnum operatingSystem, ResourceSku sku, int maximumWorkerCount, bool enablePerSiteScaling,
                DateTime spotExpirationTime, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }
            if (string.IsNullOrWhiteSpace(locationCode)) { throw new ArgumentNullException(nameof(locationCode)); }
            if (!Enum.IsDefined(typeof(OSTypeNamesEnum), operatingSystem)) { throw new ArgumentOutOfRangeException(nameof(operatingSystem)); }
            if (sku == null) { throw new ArgumentNullException(nameof(sku)); }
            if (maximumWorkerCount < 1) { throw new ArgumentNullException(nameof(maximumWorkerCount)); }
            if (spotExpirationTime < DateTime.UtcNow) { throw new ArgumentOutOfRangeException(nameof(spotExpirationTime)); }

            AppServicePlan request = new AppServicePlan()
            {
                Location = locationCode,
                Sku = sku,
                Tags = tags,

                Properties = new AppServicePlanProperties()
                {
                    IsHyperVContainerApp = false,
                    IsSpotAppsEnabled = true,
                    SpotExpirationTime = spotExpirationTime,
                    ApplyScalingPerSite = enablePerSiteScaling,
                    MaximumElasticWorkers = maximumWorkerCount,
                    IsLinuxAppPlan = (operatingSystem == OSTypeNamesEnum.Linux),
                }
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServicePlan>(response.Body);
        }

        /// <summary>
        /// Update the App Service Plan
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <param name="appServicePlan">Structure containing updated metadata for the App Service Plan (function does NOT validate the data!)</param>
        /// <returns>The updated ASP or NULL</returns>
        public static async Task<AppServicePlan?> UpdateAppServicePlan(string bearerToken, Guid subscription, string resourceGroupName, string planName, AppServicePlan appServicePlan)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }
            if (appServicePlan == null) { throw new ArgumentNullException(nameof(appServicePlan)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}",
                    CLIENT_API_VERSION,
                    null, appServicePlan,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServicePlan>(response.Body);
        }

        /// <summary>
        /// Delete an App Service Plan.
        /// Operation will be completed asynchornously.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <returns>True if operation was accepted, false if rejected, NULL if there was another problem</returns>
        public static async Task<bool?> DeleteAppServicePlan(string bearerToken, Guid subscription, string resourceGroupName, string planName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 204 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Get all webapps provisioned under an App Service Plan
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <returns>List of Web apps, or empty list</returns>
        public static async Task<IList<AppServiceWebApp>> GetWebApps(string bearerToken, Guid subscription, string resourceGroupName, string planName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<AppServiceWebApp>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}/sites",
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
        /// Reboot a worker instance. This is only available when using a web app with an 
        /// App Service Environment configuration (worker roles are only surfaced in that scenario). 
        /// Operation will be completed asynchornously.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <param name="workerName">Name of the VM worker role (typically starts with "RD")</param>
        /// <returns>True if operation was accepted, False if not. NULL if there was an exception.</returns>
        public static async Task<bool?> RebootAppServiceEnvironmentWorker(string bearerToken, Guid subscription, string resourceGroupName, string planName, string workerName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }
            if (string.IsNullOrWhiteSpace(workerName)) { throw new ArgumentNullException(nameof(workerName)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}/workers/{workerName}/reboot",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 204 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Restarts all web apps under this App Service Plan. If the apps are scaled, all the instances of all the apps are restarted.
        /// Operation will be completed asynchornously.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <param name="forceRestart">If set, then the apps are terminated and reprovisioned (takes longer). Hint: The default portal "Restart" action is a 'soft' restart [i.e., this 
        /// value would be FALSE].</param>
        /// <returns>True if operation was accepted, False if not. NULL if there was an exception.</returns>
        public static async Task<bool?> RestartAllWebApps(string bearerToken, Guid subscription, string resourceGroupName, string planName, bool forceRestart = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}/restartSites",
                    CLIENT_API_VERSION,
                    new Dictionary<string, string>()
                    {
                        { "softRestart", (forceRestart ? "false" : "true") }
                    }, null,
                    new int[] { 204 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Get the list of SKUs that the given Service Plan may be upgraded or downgraded to. Will include current SKU as well.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="planName">Name of the plan</param>
        /// <returns>List of applicable SKUs or empty list</returns>
        public static async Task<IList<AppServicePlanSku>> GetServicePlanSkuList(string bearerToken, Guid subscription, string resourceGroupName, string planName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == default) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(planName)) { throw new ArgumentNullException(nameof(planName)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<AppServicePlanSku>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/serverfarms/{planName}/skus",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<AppServicePlanSku>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<AppServicePlanSku>>(response.Body).Values;
        }






        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
