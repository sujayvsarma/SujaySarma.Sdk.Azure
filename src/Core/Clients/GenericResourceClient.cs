using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common.Common;
using SujaySarma.Sdk.Azure.Core.GenericResources;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Core.Clients
{
    /// <summary>
    /// Client interacts with the generic resources endpoint
    /// </summary>
    public static class GenericResourceClient
    {

        /// <summary>
        /// Check if a resource exists
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the containing resource group</param>
        /// <param name="resourceProviderNamespace">Namespace name of the resource provider (eg: "Microsoft.Web")</param>
        /// <param name="parentResourcePath">Path of the containing resource (if any, else set NULL or empty string)</param>
        /// <param name="type">Type of resource</param>
        /// <param name="resourceName">Name of the resource itself</param>
        /// <returns>True if exists, False if not, NULL if there was a problem</returns>
        public static async Task<bool?> Exists(string bearerToken, Guid subscription, string resourceGroupName, string resourceProviderNamespace,
            string? parentResourcePath, string type, string resourceName)
        {
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceProviderNamespace)) { throw new ArgumentNullException(nameof(resourceProviderNamespace)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(type)) { throw new ArgumentNullException(nameof(type)); }
            if (string.IsNullOrWhiteSpace(resourceName)) { throw new ArgumentNullException(nameof(resourceName)); }
            if (string.IsNullOrWhiteSpace(parentResourcePath)) { parentResourcePath = null; }

            string providerName = $"{resourceProviderNamespace}{((parentResourcePath == null) ? "" : "/" + parentResourcePath)}";
            return await Exists(bearerToken, $"https://management.azure.com{ResourceUri.CreateResourceUri(subscription, resourceGroupName, providerName, type, resourceName)}");
        }

        /// <summary>
        /// Check if a resource exists
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="resourceId">Absolute resource Id</param>
        /// <returns>True if exists, False if not, NULL if there was a problem</returns>
        public static async Task<bool?> Exists(string bearerToken, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceId) || (!resourceId.StartsWith("/subscriptions/"))) { throw new ArgumentNullException(nameof(resourceId)); }

            RestApiResponse response = await RestApiClient.HEAD(
                    bearerToken,
                    resourceId,
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 204, 404 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return null;
            }

            return (response.HttpStatus == 204);
        }


        /// <summary>
        /// Delete a resource. Resources are queued for async deletion and status will be available only later.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="resourceId">Absolute resource Id</param>
        /// <returns>True if accepted for deletion, false if not or there was a problem</returns>
        public static async Task<bool> Delete(string bearerToken, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceId)) { throw new ArgumentNullException(nameof(resourceId)); }
            if (resourceId.StartsWith("/"))
            {
                resourceId = resourceId[1..];
            }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{resourceId}",
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
        /// Create or update a resource. Resources are queued for async creation/updation and status will be available only later.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="resourceDefinition">Definition of the resource</param>
        /// <returns>The same/updated definition. If there was a problem, will return the original object</returns>
        public static async Task<GenericResource> CreateOrUpdate(string bearerToken, GenericResource resourceDefinition)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (resourceDefinition == null) { throw new ArgumentNullException(nameof(resourceDefinition)); }
            if (string.IsNullOrWhiteSpace(resourceDefinition.ResourceId)) { throw new ArgumentException($"ResourceId of the {nameof(resourceDefinition)} must be populated."); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{resourceDefinition.ResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null, resourceDefinition,
                    new int[] { 200, 201, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return resourceDefinition;
            }

            return JsonConvert.DeserializeObject<GenericResource>(response.Body);
        }

        /// <summary>
        /// Get a resource.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="resourceId">Absolute resource Id</param>
        /// <returns>Resource. NULL if there was a problem</returns>
        public static async Task<GenericResource?> Get(string bearerToken, string resourceId)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceId)) { throw new ArgumentNullException(nameof(resourceId)); }
            if (resourceId.StartsWith("/"))
            {
                resourceId = resourceId[1..];
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/{resourceId}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<GenericResource>(response.Body);
        }


        /// <summary>
        /// List all resources, optionally within a resource group
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">To filter by resource group, provide name of resource group here. Else, NULL.</param>
        /// <returns>List of matching resources. Empty list, but never NULL.</returns>
        public static async Task<List<GenericResource>> List(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { resourceGroupName = null; }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com{ResourceUri.CreateResourceUri(subscription, resourceGroupName, null, null, "resources")}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<GenericResource>();
            }

            return JsonConvert.DeserializeObject<List<GenericResource>>(response.Body);
        }

        /// <summary>
        /// (Try to) move the given set of resources to another resource group.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="resources">Group of resources to move. Must all be in the same resource group!</param>
        /// <param name="targetResourceGroup">The target resource group. Must already exist.</param>
        /// <param name="onlyValidate">If true, then only a validation is performed. When false, the actual move operation is queued.</param>
        /// <returns>This is an async operation. Will return TRUE if accepted for movement (or validation passes) or FALSE.</returns>
        public static async Task<bool> Move(string bearerToken, IEnumerable<GenericResource> resources, ResourceGroups.ResourceGroup targetResourceGroup, bool onlyValidate = true)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((resources == null) || (resources.Count() == 0)) { throw new ArgumentNullException(nameof(resources)); }
            if ((targetResourceGroup == null) || string.IsNullOrWhiteSpace(targetResourceGroup.ResourceId)) { throw new ArgumentException("targetResourceGroup is not valid."); }

            // validate that the source resource groups are all the same
            GenericResource firstResource = resources.First();
            if (string.IsNullOrWhiteSpace(firstResource.ResourceId) || (firstResource.ResourceId.Length < 68)) { throw new ArgumentException("ResourceId of resources, Element 1 is not valid."); }

            string sourceResourceGroupId = firstResource.ResourceId.Substring(0, firstResource.ResourceId.IndexOf("/", 67));

            // validate that target is not the same as the source!
            if (sourceResourceGroupId.Equals(targetResourceGroup.ResourceId, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Target resource group cannot be the same as the source group.");
            }

            ResourceMoveRequest moveRequest = new ResourceMoveRequest()
            {
                TargetResourceGroupId = targetResourceGroup.ResourceId
            };

            foreach (GenericResource res in resources)
            {
                if (string.IsNullOrWhiteSpace(res.ResourceId) || (!res.ResourceId.StartsWith(sourceResourceGroupId)))
                {
                    throw new ArgumentException($"All resources in {nameof(resources)} must belong to the same resource group.");
                }

                moveRequest.Resources.Add(res.ResourceId);
            }

            string endpointName = (onlyValidate ? "validateMoveResources" : "moveResources");
            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{sourceResourceGroupId[1..]}/{endpointName}",
                    CLIENT_API_VERSION,
                    null, 
                    moveRequest,
                    new int[] { 202, 204 }
                );

            if ((! response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }


        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
