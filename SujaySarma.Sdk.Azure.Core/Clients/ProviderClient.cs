using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Internal;
using SujaySarma.Sdk.Azure.Providers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Core.Clients
{
    /// <summary>
    /// Interact with the resource provider endpoints
    /// </summary>
    public static class ProviderClient
    {
        /// <summary>
        /// Get a list of providers at tenant scope.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="fetchMetadata">If true, adds additional metadata (mutually exclusive with <paramref name="fetchAliases"/>)</param>
        /// <param name="fetchAliases">If true, adds type aliases to the metdata (mutually exclusive with <paramref name="fetchMetadata"/>)</param>
        /// <returns>List of resource providers. Will be an empty list if there was a problem or there are no resource providers.</returns>
        public static async Task<List<ResourceProvider>> List(string bearerToken, bool fetchMetadata = false, bool fetchAliases = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }

            Dictionary<string, string> query = new Dictionary<string, string>();
            if (fetchMetadata)
            {
                query.Add("$expand", "metadata");
            }
            else if (fetchAliases)
            {
                query.Add("$expand", "resourceTypes/aliases");
            }

            RestApiResponse response = await RestApiClient.GETWithContinuations<ResourceProvider>(
                    bearerToken,
                    "https://management.azure.com/providers",
                    CLIENT_API_VERSION,
                    query, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<ResourceProvider>();
            }

            return JsonConvert.DeserializeObject<List<ResourceProvider>>(response.Body);
        }

        /// <summary>
        /// Get a list of providers at subscription scope.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="fetchMetadata">If true, adds additional metadata (mutually exclusive with <paramref name="fetchAliases"/>)</param>
        /// <param name="fetchAliases">If true, adds type aliases to the metdata (mutually exclusive with <paramref name="fetchMetadata"/>)</param>
        /// <returns>List of resource providers. Will be an empty list if there was a problem or there are no resource providers.</returns>
        public static async Task<List<ResourceProvider>> List(string bearerToken, Guid subscription, bool fetchMetadata = false, bool fetchAliases = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }

            Dictionary<string, string> query = new Dictionary<string, string>();
            if (fetchMetadata)
            {
                query.Add("$expand", "metadata");
            }
            else if (fetchAliases)
            {
                query.Add("$expand", "resourceTypes/aliases");
            }

            RestApiResponse response = await RestApiClient.GETWithContinuations<ResourceProvider>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers",
                    CLIENT_API_VERSION,
                    query, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<ResourceProvider>();
            }

            return JsonConvert.DeserializeObject<List<ResourceProvider>>(response.Body);
        }

        /// <summary>
        /// Get a resource provider at tenant scope
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="resourceProviderNamespace">Namespace name of the resource provider (eg: "Microsoft.Web")</param>
        /// <param name="fetchMetadata">If true, adds additional metadata (mutually exclusive with <paramref name="fetchAliases"/>)</param>
        /// <param name="fetchAliases">If true, adds type aliases to the metdata (mutually exclusive with <paramref name="fetchMetadata"/>)</param>
        /// <returns>Resource Provider or NULL</returns>
        public static async Task<ResourceProvider?> Get(string bearerToken, string resourceProviderNamespace, bool fetchMetadata = false, bool fetchAliases = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceProviderNamespace)) { throw new ArgumentNullException(nameof(resourceProviderNamespace)); }

            Dictionary<string, string> query = new Dictionary<string, string>();
            if (fetchMetadata)
            {
                query.Add("$expand", "metadata");
            }
            else if (fetchAliases)
            {
                query.Add("$expand", "resourceTypes/aliases");
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/providers/{resourceProviderNamespace}",
                    CLIENT_API_VERSION,
                    query, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResourceProvider>(response.Body);
        }

        /// <summary>
        /// Get a resource provider at subscription scope
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceProviderNamespace">Namespace name of the resource provider (eg: "Microsoft.Web")</param>
        /// <param name="fetchMetadata">If true, adds additional metadata (mutually exclusive with <paramref name="fetchAliases"/>)</param>
        /// <param name="fetchAliases">If true, adds type aliases to the metdata (mutually exclusive with <paramref name="fetchMetadata"/>)</param>
        /// <returns>Resource Provider or NULL</returns>
        public static async Task<ResourceProvider?> Get(string bearerToken, Guid subscription, string resourceProviderNamespace, bool fetchMetadata = false, bool fetchAliases = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceProviderNamespace)) { throw new ArgumentNullException(nameof(resourceProviderNamespace)); }

            Dictionary<string, string> query = new Dictionary<string, string>();
            if (fetchMetadata)
            {
                query.Add("$expand", "metadata");
            }
            else if (fetchAliases)
            {
                query.Add("$expand", "resourceTypes/aliases");
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/{resourceProviderNamespace}",
                    CLIENT_API_VERSION,
                    query, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResourceProvider>(response.Body);
        }

        /// <summary>
        /// Register a subscription with a resource provider
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceProviderNamespace">Namespace name of the resource provider (eg: "Microsoft.Web")</param>
        /// <returns>True if registered successfully, False if not. TRUE may also mean an asynchronous registration that completes later!</returns>
        public static async Task<bool> Register(string bearerToken, Guid subscription, string resourceProviderNamespace)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceProviderNamespace)) { throw new ArgumentNullException(nameof(resourceProviderNamespace)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/{resourceProviderNamespace}/register",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            return response.IsExpectedSuccess;
        }

        /// <summary>
        /// Unregister a subscription from a resource provider
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceProviderNamespace">Namespace name of the resource provider (eg: "Microsoft.Web")</param>
        /// <returns>True if registered successfully, False if not. TRUE may also mean an asynchronous registration that completes later!</returns>
        public static async Task<bool> Unregister(string bearerToken, Guid subscription, string resourceProviderNamespace)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceProviderNamespace)) { throw new ArgumentNullException(nameof(resourceProviderNamespace)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/{resourceProviderNamespace}/unregister",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            return response.IsExpectedSuccess;
        }

        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
