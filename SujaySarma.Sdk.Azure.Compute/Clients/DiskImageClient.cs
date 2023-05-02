using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.DiskImages;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    /// <summary>
    /// Client to interact with the disk images endpoint of the Azure RM API
    /// </summary>
    public static class DiskImageClient
    {
        /// <summary>
        /// Get a list of all disk images in the subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">(Optional) Set only if you wish to filter by resource group</param>
        /// <returns>List of disk images. Empty list if there are none or there was a problem</returns>
        public static async Task<List<DiskImage>> List(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<DiskImage>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}{(string.IsNullOrWhiteSpace(resourceGroupName) ? "" : "/resourceGroups/" + resourceGroupName)}/providers/Microsoft.Compute/images",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<DiskImage>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<DiskImage>>(response.Body).Values;
        }

        /// <summary>
        /// Get a single disk image
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk</param>
        /// <param name="imageName">Name of the disk image to retrieve</param>
        /// <returns>Disk image or NULL</returns>
        public static async Task<DiskImage?> Get(string bearerToken, Guid subscription, string resourceGroupName, string imageName)
        {
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(imageName)) { throw new ArgumentNullException(nameof(imageName)); }

            return await Get(bearerToken, $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/images/{imageName}");
        }

        /// <summary>
        /// Get a single disk image
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="imageResourceId">Absolute resource Id of the disk image</param>
        /// <returns>Disk image or NULL</returns>
        public static async Task<DiskImage?> Get(string bearerToken, string imageResourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(imageResourceId) || ((!imageResourceId.StartsWith("/subscriptions/")) && (!imageResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("imageResourceId must be an absolute resource Id.");
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/{(imageResourceId.StartsWith("/") ? imageResourceId[1..] : imageResourceId)}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<DiskImage>(response.Body);
        }

        /// <summary>
        /// Delete a disk image. Operation is completed asynchronously.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk</param>
        /// <param name="imageName">Name of the disk image to retrieve</param>
        /// <returns>True if the job was accepted. False if not</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string resourceGroupName, string imageName)
        {
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(imageName)) { throw new ArgumentNullException(nameof(imageName)); }

            return await Delete(bearerToken, $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/images/{imageName}");
        }

        /// <summary>
        /// Delete a disk image. Operation is completed asynchronously.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="imageResourceId">Absolute resource Id of the disk image</param>
        /// <returns>True if the job was accepted. False if not</returns>
        public static async Task<bool> Delete(string bearerToken, string imageResourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(imageResourceId) || ((!imageResourceId.StartsWith("/subscriptions/")) && (!imageResourceId.StartsWith("subscriptions/"))))
            {
                throw new ArgumentException("imageResourceId must be an absolute resource Id.");
            }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{(imageResourceId.StartsWith("/") ? imageResourceId[1..] : imageResourceId)}",
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
        /// Create a new disk image. Operation is asynchronous.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group containing the disk image</param>
        /// <param name="imageName">Name of the disk image to create</param>
        /// <param name="properties">Properties of the disk image</param>
        /// <returns>True if job was accepted, False if there was a problem</returns>
        public static async Task<bool> Create(string bearerToken, Guid subscription, string resourceGroupName, string imageName, DiskImageProperties properties,
            string locationName, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(imageName)) { throw new ArgumentNullException(nameof(imageName)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }

            string imageResourceId = $"subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/disks/{imageName}";
            DiskImage diskImage = new DiskImage()
            {
                Location = locationName,
                Name = imageName,
                Properties = properties,
                ResourceId = imageResourceId,
                Tags = tags,
                Type = "Microsoft.Compute/images"
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{imageResourceId}",
                    CLIENT_API_VERSION,
                    null,
                    diskImage,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update a disk image. Operation is asynchronous.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="image">Disk object containing all the values to update</param>
        /// <returns>True if job was accepted, False if there was a problem</returns>
        public static async Task<bool> Update(string bearerToken, DiskImage image)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (image == null) { throw new ArgumentNullException(nameof(image)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{image.ResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    image,
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
