using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Deployments.Clients
{
    public static class DeploymentClient
    {

        /// <summary>
        /// Create a new deployment
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization (NULL for Tenant scope)</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in (NULL for Subscription scope)</param>
        /// <param name="deploymentName">A name for the deployment</param>
        /// <param name="request">Properties of the request</param>
        /// <param name="dataStorageLocation">If the deployment happens at tenant or subscription level, the geolocation where the deployment data is to stored</param>
        /// <returns>Response. NULL if there was a failure</returns>
        public static async Task<DeploymentResponse?> CreateDeployment(string bearerToken, Guid? subscription, string? resourceGroupName, string deploymentName,
            DeploymentRequestProperties request, string? dataStorageLocation = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(deploymentName)) { throw new ArgumentNullException(nameof(deploymentName)); }
            if (request == null) { throw new ArgumentNullException(nameof(request)); }

            if ((subscription == null) || (subscription == Guid.Empty))
            {
                // tenant scope
                resourceGroupName = null;
            }

            if (string.IsNullOrWhiteSpace(resourceGroupName) && string.IsNullOrWhiteSpace(dataStorageLocation))
            {
                throw new ArgumentNullException($"When {nameof(resourceGroupName)} is null/empty, {nameof(dataStorageLocation)} must be provided.");
            }

            StringBuilder uri = new StringBuilder();
            uri.Append("https://management.azure.com");

            if (subscription.HasValue)
            {
                uri.Append("/subscriptions/").Append(subscription.Value.ToString("d"));
                if (resourceGroupName != null)
                {
                    uri.Append("/resourceGroups/").Append(resourceGroupName);
                }
            }

            uri.Append("/providers/Microsoft.Resources/deployments/").Append(deploymentName);

            DeploymentRequest deploymentRequest = new DeploymentRequest()
            {
                Properties = request
            };

            if (string.IsNullOrWhiteSpace(resourceGroupName))
            {
                deploymentRequest.Location = dataStorageLocation;
            }

            tryAgain:
            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    uri.ToString(),
                    CLIENT_API_VERSION,
                    (string.IsNullOrWhiteSpace(resourceGroupName)
                        ? new Dictionary<string, string>()
                        {
                            { "location", dataStorageLocation! }
                        }
                        : null),
                    JsonConvert.SerializeObject(deploymentRequest, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }),
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || string.IsNullOrWhiteSpace(response.Body))
            {
                if ((response.HttpStatus == 400) && (!string.IsNullOrWhiteSpace(response.Body)) && (response.Body!.Contains("InvalidOnErrorDeployment")))
                {
                    // caller specified an OnError deployment but one does not exist, 
                    // so retry the request without an OnError
                    deploymentRequest.Properties.OnError = null;
                    goto tryAgain;
                }

                if (response.WasException || string.IsNullOrWhiteSpace(response.Body))
                {
                    return null;
                }
            }

            return JsonConvert.DeserializeObject<DeploymentResponse>(response.Body);
        }

        /// <summary>
        /// Get the status of a previous deployment
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization (NULL for Tenant scope)</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in (NULL for Subscription scope)</param>
        /// <param name="deploymentName">A name for the deployment</param>
        /// <returns>Deployment status. NULL if there was a failure</returns>
        public static async Task<DeploymentStatusResponse?> GetStatus(string bearerToken, Guid? subscription, string? resourceGroupName, string deploymentName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(deploymentName)) { throw new ArgumentNullException(nameof(deploymentName)); }

            if ((subscription == null) || (subscription == Guid.Empty))
            {
                // tenant scope
                resourceGroupName = null;
            }

            StringBuilder uri = new StringBuilder();
            uri.Append("https://management.azure.com");

            if (subscription.HasValue)
            {
                uri.Append("/subscriptions/").Append(subscription.Value.ToString("d"));
                if (resourceGroupName != null)
                {
                    uri.Append("/resourceGroups/").Append(resourceGroupName);
                }
            }

            uri.Append("/providers/Microsoft.Resources/deployments/").Append(deploymentName);

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    uri.ToString(),
                    CLIENT_API_VERSION, null,
                    null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<DeploymentStatusResponse>(response.Body);
        }


        /// <summary>
        /// Get the status of a all previous deployments
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization (NULL for Tenant scope)</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in (NULL for Subscription scope)</param>
        /// <param name="filter">Filters to apply (NULL for no filter)</param>
        /// <param name="top">Top number of items to retrieve (NULL for everything)</param>
        /// <returns>Deployment status. NULL if there was a failure</returns>
        public static async Task<List<DeploymentStatusResponse>?> GetStatusList(string bearerToken, Guid? subscription, string? resourceGroupName, string? filter = null, int? top = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }

            if ((subscription == null) || (subscription == Guid.Empty))
            {
                // tenant scope
                resourceGroupName = null;
            }

            StringBuilder uri = new StringBuilder();
            uri.Append("https://management.azure.com");

            if (subscription.HasValue)
            {
                uri.Append("/subscriptions/").Append(subscription.Value.ToString("d"));
                if (resourceGroupName != null)
                {
                    uri.Append("/resourceGroups/").Append(resourceGroupName);
                }
            }

            uri.Append("/providers/Microsoft.Resources/deployments/");
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "$filter", (string.IsNullOrWhiteSpace(filter) ? "null" : filter) },
                { "$top", (top.HasValue ? top.Value.ToString() : "null") }
            };

            RestApiResponse response = await RestApiClient.GETWithContinuations<DeploymentStatusResponse>(
                    bearerToken,
                    uri.ToString(),
                    CLIENT_API_VERSION, null,
                    null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<DeploymentStatusResponse>>(response.Body).Values;
        }

        /// <summary>
        /// Export the template used for deployment
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the plan is homed in</param>
        /// <param name="deploymentName">A name for the deployment</param>
        /// <returns>AzureDeploymentTemplate instance or NULL</returns>
        public static async Task<AzureDeploymentTemplate?> ExportTemplate(string bearerToken, Guid? subscription, string? resourceGroupName, string deploymentName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(deploymentName)) { throw new ArgumentNullException(nameof(deploymentName)); }

            if ((subscription == null) || (subscription == Guid.Empty))
            {
                // tenant scope
                resourceGroupName = null;
            }

            StringBuilder uri = new StringBuilder();
            uri.Append("https://management.azure.com");

            if (subscription.HasValue)
            {
                uri.Append("/subscriptions/").Append(subscription.Value.ToString("d"));
                if (resourceGroupName != null)
                {
                    uri.Append("/resourceGroups/").Append(resourceGroupName);
                }
            }

            uri.Append("/providers/Microsoft.Resources/deployments/").Append(deploymentName).Append("/exportTemplate");

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    uri.ToString(),
                    CLIENT_API_VERSION, null,
                    null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AzureDeploymentTemplate>(response.Body);
        }



        public static string CLIENT_API_VERSION = "2019-10-01";
    }
}
