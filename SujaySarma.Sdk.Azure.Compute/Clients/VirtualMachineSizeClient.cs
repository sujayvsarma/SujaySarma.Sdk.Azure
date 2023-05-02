using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Compute.VirtualMachines;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    public static class VirtualMachineSizeClient
    {


        /// <summary>
        /// Get the VM sizes available by name of the location
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="availabilitySetName">Internal/short name of the location (eg: "westus")</param>
        /// <returns>List of available sizes or empty list</returns>
        public static async Task<List<VirtualMachineSize>> GetAvailableSizesByLocation(string bearerToken, Guid subscription, string locationName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/providers/Microsoft.Compute/locations/{locationName}/vmSizes",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<VirtualMachineSize>();
            }

            return JsonConvert.DeserializeObject<List<VirtualMachineSize>>(response.Body);
        }

        /// <summary>
        /// Get the VM sizes available by name of the virtual machine. These are the sizes that particular VM may be resized to.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>List of available sizes or empty list</returns>
        public static async Task<List<VirtualMachineSize>> GetAvailableSizesByVM(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/vmSizes",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<VirtualMachineSize>();
            }

            return JsonConvert.DeserializeObject<List<VirtualMachineSize>>(response.Body);
        }

        /// <summary>
        /// Get the VM sizes available by name of the availability set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM availability set resides in</param>
        /// <param name="availabilitySetName">Name of the availability set</param>
        /// <returns>List of available sizes or empty list</returns>
        public static async Task<List<VirtualMachineSize>> GetAvailableSizesByAvailabilitySet(string bearerToken, Guid subscription, string resourceGroupName, string availabilitySetName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(availabilitySetName)) { throw new ArgumentNullException(nameof(availabilitySetName)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/availabilitySets/{availabilitySetName}/vmSizes",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<VirtualMachineSize>();
            }

            return JsonConvert.DeserializeObject<List<VirtualMachineSize>>(response.Body);
        }


        public static string CLIENT_API_VERSION = "2019-03-01";
    }
}
