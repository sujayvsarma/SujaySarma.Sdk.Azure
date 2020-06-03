using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Internal;
using SujaySarma.Sdk.Azure.Subscriptions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Core.Clients
{
    /// <summary>
    /// Interact with the subscriptions endpoints
    /// </summary>
    public static class SubscriptionClient
    {
        /// <summary>
        /// Get a list of subscriptions.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <returns>List of subscriptions. Will be an empty list if there was a problem or there are no subscriptions.</returns>
        public static async Task<List<Subscription>> List(string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            RestApiResponse response = await RestApiClient.GETWithContinuations<Subscription>(
                    bearerToken,
                    "https://management.azure.com/subscriptions",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<Subscription>();
            }

            return JsonConvert.DeserializeObject<List<Subscription>>(response.Body);
        }

        /// <summary>
        /// Get a single subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="id">Guid of the subscription</param>
        /// <returns>The subscription. NULL if the item was not found or there was a problem.</returns>
        public static async Task<Subscription?> Get(string bearerToken, Guid id)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((id == Guid.Empty) || (id == default)) { throw new ArgumentNullException(nameof(id)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{id:d}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Subscription>(response.Body);
        }

        /// <summary>
        /// Get all geographic locations (Azure Data Centers) that are accessible to a particular subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="id">Guid of the subscription</param>
        /// <returns>List of locations. Empty list if there was a problem</returns>
        public static async Task<List<SubscriptionLocation>> ListLocations(string bearerToken, Guid id)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((id == Guid.Empty) || (id == default)) { throw new ArgumentNullException(nameof(id)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<SubscriptionLocation>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{id:d}/locations",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<SubscriptionLocation>();
            }

            return JsonConvert.DeserializeObject<List<SubscriptionLocation>>(response.Body);
        }


        /// <summary>
        /// Cancel a subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="id">Guid of the subscription</param>
        public static async Task Cancel(string bearerToken, Guid id)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((id == Guid.Empty) || (id == default)) { throw new ArgumentNullException(nameof(id)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{id:d}/providers/Microsoft.Subscription/cancel",
                    "2019-03-01-preview",
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                await Task.FromException(new Exception(response.ExceptionMessage));
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Enable a subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="id">Guid of the subscription</param>
        public static async Task Enable(string bearerToken, Guid id)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((id == Guid.Empty) || (id == default)) { throw new ArgumentNullException(nameof(id)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{id:d}/providers/Microsoft.Subscription/enable",
                    "2019-03-01-preview",
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                await Task.FromException(new Exception(response.ExceptionMessage));
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Rename a subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="id">Guid of the subscription</param>
        /// <param name="newName">New name for the subscription</param>
        public static async Task Rename(string bearerToken, Guid id, string newName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((id == Guid.Empty) || (id == default)) { throw new ArgumentNullException(nameof(id)); }
            if (string.IsNullOrWhiteSpace(newName)) { throw new ArgumentNullException(nameof(newName)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{id:d}/providers/Microsoft.Subscription/rename",
                    "2019-03-01-preview",
                    null,
                    new SubscriptionRenameStructure()
                    {
                        SubscriptionName = newName
                    },
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                await Task.FromException(new Exception(response.ExceptionMessage));
            }

            await Task.CompletedTask;
        }


        public static string CLIENT_API_VERSION = "2019-11-01";
    }


    public class SubscriptionRenameStructure
    {
        [JsonProperty("SubscriptionName")]
        public string SubscriptionName { get; set; } = string.Empty;
    }

}