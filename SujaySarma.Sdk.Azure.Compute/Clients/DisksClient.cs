using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;
using SujaySarma.Sdk.Azure.Compute.Disks;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    /// <summary>
    /// Client to interact with the disks endpoint of the Azure RM API
    /// </summary>
    public static class DisksClient
    {

        /// <summary>
        /// Get a list of all disks in the subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">(Optional) Set only if you wish to filter by resource group</param>
        /// <returns>List of disks. Empty list if there are none or there was a problem</returns>
        public static async Task<List<Disk>> List(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<Disk>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}{(string.IsNullOrWhiteSpace(resourceGroupName) ? "" : "/resourceGroups/" + resourceGroupName)}/providers/Microsoft.Compute/disks",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<Disk>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<Disk>>(response.Body).Values;
        }

        /// <summary>
        /// Get a single disk
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk</param>
        /// <param name="diskName">Name of the disk to retrieve</param>
        /// <returns>Disk or NULL</returns>
        public static async Task<Disk?> Get(string bearerToken, Guid subscription, string resourceGroupName, string diskName)
        {
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(diskName)) { throw new ArgumentNullException(nameof(diskName)); }

            return await Get(bearerToken, $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/disks/{diskName}");
        }

        /// <summary>
        /// Get a single disk
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="diskResourceId">Absolute resource Id of the disk</param>
        /// <returns>Disk or NULL</returns>
        public static async Task<Disk?> Get(string bearerToken, string diskResourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(diskResourceId) || ((!diskResourceId.StartsWith("/subscriptions/")) && (!diskResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("diskResourceId must be an absolute resource Id.");
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/{(diskResourceId.StartsWith("/") ? diskResourceId[1..] : diskResourceId)}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Disk>(response.Body);
        }

        /// <summary>
        /// Delete a disk. Operation is completed asynchronously.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="diskResourceId">Absolute resource Id of the disk</param>
        /// <returns>True if the job was accepted. False if not</returns>
        public static async Task<bool> Delete(string bearerToken, string diskResourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(diskResourceId) || ((!diskResourceId.StartsWith("/subscriptions/")) && (!diskResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("diskResourceId must be an absolute resource Id.");
            }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{(diskResourceId.StartsWith("/") ? diskResourceId[1..] : diskResourceId)}",
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
        /// Create a new disk. Operation is asynchronous.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk</param>
        /// <param name="diskName">Name of the disk to create</param>
        /// <param name="properties">Properties of the disk</param>
        /// <returns>True if job was accepted, False if there was a problem</returns>
        public static async Task<bool> Create(string bearerToken, Guid subscription, string resourceGroupName, string diskName, DiskProperties properties,
            string locationName, DiskSkuNamesEnum sku = DiskSkuNamesEnum.Standard_LRS, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(diskName)) { throw new ArgumentNullException(nameof(diskName)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }
            if (properties.CreationMetadata == null) { throw new ArgumentException("properties must contain CreationMetadata."); }
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }

            string diskResourceId = $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/disks/{diskName}";
            Disk disk = new Disk()
            {
                Location = locationName,
                ManagedBy = null,
                Name = diskName,
                Properties = properties,
                ResourceId = diskResourceId,
                Sku = new DiskSku()
                {
                    Name = sku
                },
                Tags = tags,
                Type = "Microsoft.Compute/disks"
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{diskResourceId}",
                    CLIENT_API_VERSION,
                    null,
                    disk,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update a disk. Operation is asynchronous.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="disk">Disk object containing all the values to update</param>
        /// <returns>True if job was accepted, False if there was a problem</returns>
        public static async Task<bool> Update(string bearerToken, Disk disk)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (disk == null) { throw new ArgumentNullException(nameof(disk)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{disk.ResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    disk,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get a SAS token to access a disk
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="diskResourceId">Absolute resource Uri to the disk</param>
        /// <param name="durationSeconds">Required duration of access in seconds</param>
        /// <param name="accessLevel">Level of access</param>
        /// <returns>The SAS token to access the disk. NULL if we did not get one back</returns>
        public static async Task<string?> GetAccess(string bearerToken, string diskResourceId, int durationSeconds, AccessLevelEnum accessLevel = AccessLevelEnum.None)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(diskResourceId) || ((!diskResourceId.StartsWith("/subscriptions/")) && (!diskResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("diskResourceId must be an absolute resource Id.");
            }

            DiskAccessRequest request = new DiskAccessRequest()
            {
                AccessExpireInSeconds = durationSeconds,
                AccessLevelRequired = accessLevel
            };

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{diskResourceId[1..]}/beginGetAccess",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<DiskAccessResponse>(response.Body).SasUri;
        }

        /// <summary>
        /// Get a SAS token to access a disk
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="diskResourceId">Absolute resource Uri to the disk</param>
        /// <returns>The SAS token to access the disk. NULL if we did not get one back</returns>
        public static async Task<bool> RevokeAccess(string bearerToken, string diskResourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(diskResourceId) || ((!diskResourceId.StartsWith("/subscriptions/")) && (!diskResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("diskResourceId must be an absolute resource Id.");
            }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{diskResourceId[1..]}/endGetAccess",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }


        public static string CLIENT_API_VERSION = "2019-07-01";
    }
}
