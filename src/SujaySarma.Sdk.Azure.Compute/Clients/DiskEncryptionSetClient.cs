using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.DiskEncryptionSets
{
    /// <summary>
    /// Client to interact with the disk encryption sets endpoint of the Azure RM API
    /// </summary>
    public static class DiskEncryptionSetClient
    {

        /// <summary>
        /// Get a single disk encryption set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk</param>
        /// <param name="diskEncryptionSetName">Name of the disk encryption set to retrieve</param>
        /// <returns>Disk encryption set or NULL</returns>
        public static async Task<DiskEncryptionSet?> Get(string bearerToken, Guid subscription, string resourceGroupName, string diskEncryptionSetName)
        {
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(diskEncryptionSetName)) { throw new ArgumentNullException(nameof(diskEncryptionSetName)); }

            return await Get(bearerToken, $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/diskEncryptionSets/{diskEncryptionSetName}");
        }

        /// <summary>
        /// Get a single disk encryption set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="diskEncryptionSetResourceId">Absolute resource Id of the disk</param>
        /// <returns>Disk encryption set or NULL</returns>
        public static async Task<DiskEncryptionSet?> Get(string bearerToken, string diskEncryptionSetResourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(diskEncryptionSetResourceId) || ((!diskEncryptionSetResourceId.StartsWith("/subscriptions/")) && (!diskEncryptionSetResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("diskResourceId must be an absolute resource Id.");
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/{(diskEncryptionSetResourceId.StartsWith("/") ? diskEncryptionSetResourceId[1..] : diskEncryptionSetResourceId)}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<DiskEncryptionSet>(response.Body);
        }

        /// <summary>
        /// List all disk encryption sets in the subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">(Optional) Set only if you want to filter by resource group</param>
        /// <returns>List of sets, or empty list</returns>
        public static async Task<List<DiskEncryptionSet>> List(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<DiskEncryptionSet>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}{(string.IsNullOrWhiteSpace(resourceGroupName) ? "" : "/resourceGroups/" + resourceGroupName)}/providers/Microsoft.Compute/diskEncryptionSets",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<DiskEncryptionSet>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<DiskEncryptionSet>>(response.Body).Values;
        }

        /// <summary>
        /// Delete a single disk encryption set
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk</param>
        /// <param name="diskEncryptionSetName">Name of the disk encryption set to retrieve</param>
        /// <returns>True if deletion job was accepted. False otherwise</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string resourceGroupName, string diskEncryptionSetName)
        {
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(diskEncryptionSetName)) { throw new ArgumentNullException(nameof(diskEncryptionSetName)); }

            return await Delete(bearerToken, $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/diskEncryptionSets/{diskEncryptionSetName}");
        }


        /// <summary>
        /// Delete a disk encryption set. This is an async job and the returned value only indicates acceptance of the job
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="diskEncryptionSetResourceId">Absolute resource Id of the disk</param>
        /// <returns>True if deletion job was accepted. False otherwise</returns>
        public static async Task<bool> Delete(string bearerToken, string diskEncryptionSetResourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(diskEncryptionSetResourceId) || ((!diskEncryptionSetResourceId.StartsWith("/subscriptions/")) && (!diskEncryptionSetResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("diskResourceId must be an absolute resource Id.");
            }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{diskEncryptionSetResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 202, 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a disk encryption set. The process will complete async.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk</param>
        /// <param name="diskEncryptionSetName">Name of the disk encryption set to retrieve</param>
        /// <returns>Disk encryption set or NULL if there was a problem</returns>
        public static async Task<DiskEncryptionSet?> CreateOrUpdate(string bearerToken, Guid subscription, string resourceGroupName, string diskEncryptionSetName,
            DiskEncryptionSetProperties properties, string location, Dictionary<string, string>? tags = null)
        {
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(diskEncryptionSetName)) { throw new ArgumentNullException(nameof(diskEncryptionSetName)); }
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }

            properties ??= new DiskEncryptionSetProperties();

            string resourceId = $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/diskEncryptionSets/{diskEncryptionSetName}";
            DiskEncryptionSet request = new DiskEncryptionSet()
            {
                ResourceId = resourceId,
                Properties = properties,
                Location = location,
                Identity = new DiskEncryptionSetIdentity()
                {
                    Type = DiskEncryptionSetIdentityTypeNamesEnum.SystemAssigned
                },
                Name = diskEncryptionSetName,
                Tags = tags,
                Type = ""
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{resourceId}",
                    CLIENT_API_VERSION,
                    null,
                    request,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<DiskEncryptionSet>(response.Body);
        }


        public static string CLIENT_API_VERSION = "2019-07-01";
    }
}
