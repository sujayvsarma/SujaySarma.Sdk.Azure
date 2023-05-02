using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Internal;
using SujaySarma.Sdk.Azure.Tenants;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Core.Clients
{
    /// <summary>
    /// Interact with the tenant endpoints
    /// </summary>
    public static class TenantClient
    {
        /// <summary>
        /// Get a list of tenants.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <returns>List of tenants. Will be an empty list if there was a problem.</returns>
        public static async Task<List<Tenant>> List(string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            RestApiResponse response = await RestApiClient.GETWithContinuations<Tenant>(
                    bearerToken,
                    "https://management.azure.com/tenants",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<Tenant>();
            }

            return JsonConvert.DeserializeObject<List<Tenant>>(response.Body);
        }

        public static string CLIENT_API_VERSION = "2019-11-01";
    }
}
