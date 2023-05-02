using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Internal;
using SujaySarma.Sdk.Azure.Obscure.Backfill;

using System;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Obscure.Clients
{
    /// <summary>
    /// Provides methods to start a tenant/subscription backfill for an Azure tenant
    /// </summary>
    public static class TenantBackfillClient
    {

        /// <summary>
        /// Start backfilling subscriptions for the tenant authorized for the bearer token
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <returns>Status of the request</returns>
        public static async Task<TenantBackFillStatus> StartBackfillingSubscriptions(string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    "https://management.azure.com/providers/Microsoft.Management/startTenantBackfill",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new TenantBackFillStatus()
                {
                    Status = TenantBackfillStatusEnum.NotStarted,
                    TenantId = Guid.Empty
                };
            }

            return JsonConvert.DeserializeObject<TenantBackFillStatus>(response.Body);
        }

        /// <summary>
        /// Get the status of a previously started subscription-backfilling operation for the tenant authorized for the bearer token
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <returns>Status of the request</returns>
        public static async Task<TenantBackFillStatus> GetPreviousBackfillingOperationStatus(string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    "https://management.azure.com/providers/Microsoft.Management/tenantBackfillStatus",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new TenantBackFillStatus()
                {
                    Status = TenantBackfillStatusEnum.NotStarted,
                    TenantId = Guid.Empty
                };
            }

            return JsonConvert.DeserializeObject<TenantBackFillStatus>(response.Body);
        }



        public static string CLIENT_API_VERSION = "2018-03-01-preview";
    }
}
