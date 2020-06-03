using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.ResourceSkus;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    public static class ResourceSkusClient
    {

        /// <summary>
        /// Get all SKUs available to a subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <returns>List of resource skus available. Empty list if there are none or there was a problem</returns>
        public static async Task<List<ComputeResourceSku>> GetAllAvailableSizes(string bearerToken, Guid subscription)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<ComputeResourceSku>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/providers/Microsoft.Compute/skus",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<ComputeResourceSku>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<ComputeResourceSku>>(response.Body).Values;
        }

        public static string CLIENT_API_VERSION = "2017-09-01";
    }
}
