using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Core.Tags;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Core.Clients
{
    /// <summary>
    /// Interact with the tags endpoints
    /// </summary>
    public static class TagsClient
    {


        /// <summary>
        /// Create a tag. This method does not check for existence of the tag.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="tagName">Name of the tag</param>
        /// <returns>The created tag. NULL if there was a problem</returns>
        public static async Task<Tag?> Create(string bearerToken, Guid subscription, string tagName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(tagName)) { throw new ArgumentNullException(nameof(tagName)); }
            if ((tagName.Length > 512) || tagName.StartsWith("microsoft", StringComparison.InvariantCultureIgnoreCase)
                || tagName.StartsWith("azure", StringComparison.InvariantCultureIgnoreCase) || tagName.StartsWith("windows", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("tagName must be <= 512 characters and not start with 'microsoft', 'azure' or 'windows'.");
            }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/tagNames/{tagName}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Tag>(response.Body);
        }

        /// <summary>
        /// Sets the value of a tag. Tag must already exist (this method does not check for it).
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="tagName">Name of the tag</param>
        /// <param name="value">Value to set</param>
        /// <returns>The updated tag. NULL if there was a problem</returns>
        public static async Task<Tag?> SetValue(string bearerToken, Guid subscription, string tagName, string value)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(tagName)) { throw new ArgumentNullException(nameof(tagName)); }
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException(nameof(value)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/tagNames/{tagName}/{value}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Tag>(response.Body);
        }

        /// <summary>
        /// Delete tag. This method does not check for existence of the tag.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="tagName">Name of the tag</param>
        /// <returns>True if deleted, False if not.</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string tagName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(tagName)) { throw new ArgumentNullException(nameof(tagName)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/tagNames/{tagName}",
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
        /// Get a list of all tags defined at the subscription level
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <returns>List of tags. Empty list if there was none or there was a problem.</returns>
        public static async Task<List<Tag>> List(string bearerToken, Guid subscription)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<Tag>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/tagNames",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<Tag>();
            }

            return JsonConvert.DeserializeObject<List<Tag>>(response.Body);
        }


        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
