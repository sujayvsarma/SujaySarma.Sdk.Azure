using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.AvailabilitySets;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    /// <summary>
    /// Client to interact with the virtual machines availability sets endpoint
    /// </summary>
    public static class AvailabilitySetClient
    {

        /// <summary>
        /// Create a virtual machine availability set. 
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the availability set resides in</param>
        /// <param name="availabilitySetName">Name of the virtual machine</param>
        /// <param name="availabilitySettings">Settings of the VM availability set to create or update. Note that not all settings work!</param>
        /// <returns>Virtual Machine availability set. NULL if there was a problem</returns>
        public static async Task<AvailabilitySet?> Create(string bearerToken, Guid subscription, string resourceGroupName, string availabilitySetName,
            AvailabilitySet availabilitySettings)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(availabilitySetName)) { throw new ArgumentNullException(nameof(availabilitySetName)); }
            if (availabilitySettings == null) { throw new ArgumentNullException(nameof(availabilitySettings)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/availabilitySets/{availabilitySetName}",
                    CLIENT_API_VERSION,
                    null, availabilitySettings,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AvailabilitySet>(response.Body);
        }

        /// <summary>
        /// Create a virtual machine availability set. 
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="availabilitySettings">Settings of the VM availability set to create or update. Note that not all settings work!</param>
        /// <returns>Virtual Machine availability set. NULL if there was a problem</returns>
        public static async Task<AvailabilitySet?> Update(string bearerToken, AvailabilitySet availabilitySettings)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (availabilitySettings == null) { throw new ArgumentNullException(nameof(availabilitySettings)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{availabilitySettings.ResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null, availabilitySettings,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AvailabilitySet>(response.Body);
        }

        /// <summary>
        /// Delete the virtual machine availability set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the availability set resides in</param>
        /// <param name="availabilitySetName">Name of the virtual machine</param>
        /// <returns>True if the delete job was accepted, False if not</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string resourceGroupName, string availabilitySetName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(availabilitySetName)) { throw new ArgumentNullException(nameof(availabilitySetName)); }

            return await Delete(bearerToken, $"/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/availabilitySets/{availabilitySetName}?");
        }

        /// <summary>
        /// Delete the virtual machine availability set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="availabilitySetUri">Resource Uri to the availability set</param>
        /// <returns>True if the delete job was accepted, False if not</returns>
        public static async Task<bool> Delete(string bearerToken, string availabilitySetUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(availabilitySetUri)) { throw new ArgumentNullException(nameof(availabilitySetUri)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{availabilitySetUri[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete the virtual machine availability set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the availability set resides in</param>
        /// <param name="availabilitySetName">Name of the virtual machine</param>
        /// <returns>AvailabilitySet or NULL</returns>
        public static async Task<AvailabilitySet?> Get(string bearerToken, Guid subscription, string resourceGroupName, string availabilitySetName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(availabilitySetName)) { throw new ArgumentNullException(nameof(availabilitySetName)); }

            return await Get(bearerToken, $"/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/availabilitySets/{availabilitySetName}?");
        }

        /// <summary>
        /// Delete the virtual machine availability set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="availabilitySetUri">Resource Uri to the availability set</param>
        /// <returns>AvailabilitySet or NULL</returns>
        public static async Task<AvailabilitySet?> Get(string bearerToken, string availabilitySetUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(availabilitySetUri)) { throw new ArgumentNullException(nameof(availabilitySetUri)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/{availabilitySetUri[1..]}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AvailabilitySet>(response.Body);
        }

        /// <summary>
        /// Get a list of all availability sets
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">(Optional) Resource group the availability sets reside in</param>
        /// <returns>List of AvailabilitySets or empty list</returns>
        public static async Task<List<AvailabilitySet>> List(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            StringBuilder queryUri = new StringBuilder();
            queryUri.Append($"https://management.azure.com/subscriptions/{subscription.ToString("d")}");
            if (!string.IsNullOrWhiteSpace(resourceGroupName))
            {
                queryUri.Append($"/resourceGroups/{resourceGroupName}");
            }
            queryUri.Append("/providers/Microsoft.Compute/availabilitySets");

            RestApiResponse response = await RestApiClient.GETWithContinuations<AvailabilitySet>(
                    bearerToken,
                    queryUri.ToString(),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<AvailabilitySet>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<AvailabilitySet>>(response.Body).Values;
        }


        public static string CLIENT_API_VERSION = "2019-03-01";
    }
}
