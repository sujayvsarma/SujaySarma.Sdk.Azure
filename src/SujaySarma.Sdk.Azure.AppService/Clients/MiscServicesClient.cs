using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    /// <summary>
    /// Client to encapsulate calls to services/endpoints that do not 
    /// require their own client class
    /// </summary>
    public static class MiscServicesClient
    {

        /// <summary>
        /// Check if the provided name is available for creating a new resource
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="nameToCheck">The name to check for availabilit</param>
        /// <param name="type">Type of resource that <paramref name="nameToCheck"/> is for</param>
        /// <param name="isNameFQDN">Set TRUE if the <paramref name="nameToCheck"/> is a fully qualified domain name. FALSE if it is only the 'hostname' (ARM will append the 
        /// appropriate domain)</param>
        /// <returns>NULL: Invalid or other problem, TRUE: Available, FALSE: Not available (used)</returns>
        public static async Task<bool?> IsNameAvailable(string bearerToken, Guid subscription, string nameToCheck, AppServiceResourceTypesEnum type, bool isNameFQDN = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(nameToCheck)) { throw new ArgumentNullException(nameof(nameToCheck)); }
            if (!Enum.IsDefined(typeof(AppServiceResourceTypesEnum), type)) { throw new ArgumentOutOfRangeException(nameof(type)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.Web/checknameavailability",
                    "2019-08-01",
                    null,
                    new ResourceNameAvailabilityRequest()
                    {
                        Name = nameToCheck,
                        IsFullyQualifiedDomainName = isNameFQDN,
                        Type = Enum.GetName(typeof(AppServiceResourceTypesEnum), type)?.Replace("__", "/")?.Replace("_", ".") ?? "Site"
                    },
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            ResourceNameAvailabilityResponse results = JsonConvert.DeserializeObject<ResourceNameAvailabilityResponse>(response.Body);
            return results.Result switch
            {
                true => true,
                false => results.ResultReason switch
                {
                    ResourceNameUnavailabilityReason.AlreadyExists => false,
                    _ => null
                }
            };
        }


        /// <summary>
        /// List all available SKUs for App Services
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <returns>List of SKUs or empty list</returns>
        public static async Task<IList<object>> GetAvailableSkus(string bearerToken, Guid subscription)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            //TODO !!!

            return await Task.FromResult(new List<object>());
        }

    }
}
