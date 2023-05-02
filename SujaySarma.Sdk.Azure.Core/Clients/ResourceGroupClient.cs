using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Internal;
using SujaySarma.Sdk.Azure.ResourceGroups;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Core.Clients
{
    /// <summary>
    /// Interact with the subscriptions endpoints
    /// </summary>
    public static class ResourceGroupClient
    {
        /// <summary>
        /// Get a list of resource groups.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <returns>List of resource groups. Will be an empty list if there was a problem or there are no resource groups.</returns>
        public static async Task<List<ResourceGroup>> List(string bearerToken, Guid subscription)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<ResourceGroup>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourcegroups",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<ResourceGroup>();
            }

            return JsonConvert.DeserializeObject<List<ResourceGroup>>(response.Body);
        }

        /// <summary>
        /// Get a specific resource group by its name
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group. Not case-sensitive.</param>
        /// <returns>The resource group if found, else NULL</returns>
        public static async Task<ResourceGroup?> Get(string bearerToken, Guid subscription, string resourceGroupName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourcegroups/{resourceGroupName}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResourceGroup>(response.Body);
        }

        /// <summary>
        /// Check if a specific resource group exists, by its name
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group. Not case-sensitive.</param>
        /// <returns>TRUE if it exists, FALSE if not, NULL if there was an error</returns>
        public static async Task<bool?> Exists(string bearerToken, Guid subscription, string resourceGroupName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }

            RestApiResponse response = await RestApiClient.HEAD(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourcegroups/{resourceGroupName}",
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
        /// Create a new resource group. This method does not check for existance of the group or perform any other validation!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group</param>
        /// <param name="location">Azure data center location name (the short name eg: "westus")</param>
        /// <param name="tags">Tags to set</param>
        /// <returns>The newly created resource group. NULL if there was a problem</returns>
        public static async Task<ResourceGroup?> Create(string bearerToken, Guid subscription, string resourceGroupName, string location, Dictionary<string, string>? tags)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }

            var body = new
            {
                location,
                tags = (((tags != null) && (tags.Count > 0)) ? tags : null)
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourcegroups/{resourceGroupName}",
                    CLIENT_API_VERSION,
                    null, body,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResourceGroup>(response.Body);
        }

        /// <summary>
        /// Update the tags for a resource group. This method does not check for existence of the group or perform any other validation!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group</param>
        /// <param name="tags">Tags to set</param>
        /// <returns>The updated properties of the resource group. NULL if there was a problem.</returns>
        public static async Task<ResourceGroup?> UpdateTags(string bearerToken, Guid subscription, string resourceGroupName, Dictionary<string, string>? tags)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if ((tags == null) || (tags.Count == 0)) { throw new ArgumentNullException(nameof(tags)); }

            var body = new
            {
                tags
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourcegroups/{resourceGroupName}",
                    CLIENT_API_VERSION,
                    null, body,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ResourceGroup>(response.Body);
        }


        /// <summary>
        /// Deletes a resource group
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group. Not case-sensitive.</param>
        /// <returns>The operation is queued for asynchronous processing, so TRUE means accepted for deletion, FALSE means there was some problem.</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string resourceGroupName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourcegroups/{resourceGroupName}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Exports the entire resource group as an AzureRM template
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group. Not case-sensitive.</param>
        /// <returns>String value containing the AzureRM template's JSON text. NULL if there was a problem</returns>
        public static async Task<string?> ExportTemplate(string bearerToken, Guid subscription, string resourceGroupName, ExportTemplateOptions options)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (options == null)
            {
                // will set default options
                options = new ExportTemplateOptions();
            }

            StringBuilder optOptions = new StringBuilder();
            if (options.IncludeDefaultValuesForParameters)
            {
                optOptions.Append("IncludeParameterDefaultValue");
            }

            if (options.IncludeComments)
            {
                if (optOptions.Length > 0)
                {
                    optOptions.Append(",");
                }
                optOptions.Append("IncludeComments");
            }

            if (options.DoNotParameterize)
            {
                if (optOptions.Length > 0)
                {
                    optOptions.Append(",");
                }
                optOptions.Append("SkipAllParameterization");
            }
            else if (options.RetainExistingResourceNames)
            {
                if (optOptions.Length > 0)
                {
                    optOptions.Append(",");
                }
                optOptions.Append("SkipResourceNameParameterization");
            }

            if (options.IncludeAllResources)
            {
                options.ResourceNames = new List<string>() { "*" };
            }
            else
            {
                if ((options.ResourceNames == null) || (options.ResourceNames.Count == 0))
                {
                    throw new ArgumentException($"{nameof(options)}: ResourceNames must be specified if IncludeAllResources is FALSE.");
                }
            }

            // we dont want to create a class for this, NSJ will handle anon-types :-)
            var opt = new
            {
                options = optOptions.ToString(),
                resources = options.ResourceNames
            };


            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourcegroups/{resourceGroupName}/exportTemplate",
                    CLIENT_API_VERSION,
                    null, opt,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return response.Body;
        }


        public static string CLIENT_API_VERSION = "2019-06-01";
    }
}
