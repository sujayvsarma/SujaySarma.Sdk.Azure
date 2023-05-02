using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.DeletedWebApps;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    /// <summary>
    /// Lists and manages deleted web applications
    /// </summary>
    public static class DeletedWebAppsClient
    {

        /// <summary>
        /// Get a list of web apps that have been deleted
        /// </summary>
        /// <param name="bearerToken">Azure Bearer Token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="locationCode">(Optional) The location code (eg: "westus") where the app was hosted</param>
        /// <returns>List of deleted applications or empty list</returns>
        public static async Task<IList<DeletedWebApp>> GetDeletedWebApps(string bearerToken, Guid subscription, string? locationCode = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            StringBuilder requestUri = new StringBuilder();
            requestUri.Append($"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.Web");
            if (!string.IsNullOrWhiteSpace(locationCode))
            {
                requestUri.Append($"/locations/{locationCode}");
            }
            requestUri.Append("/deletedSites");

            RestApiResponse response = await RestApiClient.GETWithContinuations<DeletedWebAppProperties>(
                    bearerToken,
                    requestUri.ToString(),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<DeletedWebApp>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<DeletedWebApp>>(response.Body).Values;
        }

        /// <summary>
        /// Get metadata about a single web app that has been deleted
        /// </summary>
        /// <param name="bearerToken">Azure Bearer Token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="locationCode">(Optional) The location code (eg: "westus") where the app was hosted</param>
        /// <param name="siteId">Numeric Id of the web app</param>
        /// <returns>Metadata about the deleted web app or NULL</returns>
        public static async Task<DeletedWebApp?> GetDeletedWebApp(string bearerToken, Guid subscription, string locationCode, int siteId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(locationCode)) { throw new ArgumentNullException(nameof(locationCode)); }
            if (siteId < 0) { throw new ArgumentOutOfRangeException(nameof(siteId)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.Web/locations/{locationCode}/deletedSites/{siteId}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<DeletedWebApp>(response.Body);
        }


        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
