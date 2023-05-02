using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;
using SujaySarma.Sdk.Azure.Storage.Availability;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Storage.Clients
{
    /// <summary>
    /// Client that performs actions agains the Azure Storage Services API
    /// </summary>
    public static class StorageServicesClient
    {

        /// <summary>
        /// Check if name is available for creation
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is to be placed in</param>
        /// <param name="nameToCheck">Name to check for</param>
        /// <returns>True if name is available</returns>
        public static async Task<bool> IsNameAvailable(string bearerToken, Guid subscription, string resourceGroupName, string nameToCheck)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(nameToCheck)) { throw new ArgumentNullException(nameof(nameToCheck)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.Storage/checkNameAvailability",
                    CLIENT_API_VERSION,
                    null,
                    new Request(nameToCheck),
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return false;
            }

            return JsonConvert.DeserializeObject<Response>(response.Body).IsAvailable;
        }

        /// <summary>
        /// Create a new storage account
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is to be placed in</param>
        /// <param name="storageAccountName">Name of the storage account to create</param>
        /// <param name="location">Geographic location where account is to be placed (eg: "uswest")</param>
        /// <param name="kind">Kind of storage account to create</param>
        /// <param name="sku">SKU of storage account</param>
        /// <param name="properties">Additional properties (optional)</param>
        /// <returns>Storage Account</returns>
        public static async Task<StorageAccount?> Create(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName, string location,
            StorageAccountKind kind = StorageAccountKind.StorageV2, StorageAccountSkuNames sku = StorageAccountSkuNames.Standard_LRS,
                StorageAccountCreateRequestProperties? properties = null, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }
            if (!Enum.IsDefined(typeof(StorageAccountKind), kind)) { throw new ArgumentOutOfRangeException(nameof(kind)); }
            if (!Enum.IsDefined(typeof(StorageAccountSkuNames), sku)) { throw new ArgumentOutOfRangeException(nameof(sku)); }

            StorageAccountCreateRequest request = new StorageAccountCreateRequest()
            {
                Kind = kind,
                Sku = new StorageAccountSku()
                {
                    Name = sku,
                    Tier = StorageAccountSkuTiers.Standard
                },
                Location = location,
                Tags = tags,

                Properties = properties ?? new StorageAccountCreateRequestProperties()
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}",
                    CLIENT_API_VERSION,
                    null,
                    request,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return null;
            }

            if ((response.HttpStatus == 202) && (!string.IsNullOrWhiteSpace(response.Headers?.Location.ToString())))
            {
                string pollUrl = response.Headers!.Location.ToString();
                bool complete = false;

                while (!complete)
                {
                    response = await RestApiClient.GET(
                            bearerToken,
                            pollUrl,
                            string.Empty, null, null,
                            new int[] { 200, 202 }
                        );

                    if ((!response.IsExpectedSuccess) || response.WasException)
                    {
                        return null;
                    }

                    if ((response.HttpStatus == 200) && (!string.IsNullOrWhiteSpace(response.Body)))
                    {
                        complete = true;
                    }
                }
            }


            return JsonConvert.DeserializeObject<StorageAccount>(response.Body!);
        }

        /// <summary>
        /// Delete a storage account
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account to delete</param>
        /// <returns>True if the task was accepted. False if not. NULL if there is a problem</returns>
        public static async Task<bool?> Delete(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return null;
            }

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Failover a storage account. Failback is the same call because after failover the primary is marked as the secondary and vice-versa.
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account</param>
        /// <returns>True if the task was accepted. False if not. NULL if there is a problem</returns>
        public static async Task<bool?> Failover(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}/failover",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return null;
            }

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Get metadata about a storage account
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account</param>
        /// <returns>StorageAccount. NULL if there is a problem</returns>
        public static async Task<StorageAccount?> Get(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<StorageAccount>(response.Body);
        }

        /// <summary>
        /// List storage accounts
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in (optional)</param>
        /// <returns>List of storage accounts or empty list</returns>
        public static async Task<List<StorageAccount>> List(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }

            ResourceUri uri = new ResourceUri(subscription, resourceGroupName, "Microsoft.Storage", "storageAccounts");
            RestApiResponse response = await RestApiClient.GETWithContinuations<StorageAccount>(
                    bearerToken,
                    uri.ToAbsoluteAzureRMEndpointUri(),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<StorageAccount>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<StorageAccount>>(response.Body).Values;
        }

        /// <summary>
        /// Get an SAS token for the Storage Account
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account</param>
        /// <param name="properties">Other properties for the request</param>
        /// <returns>SAS token value (or NULL)</returns>
        public static async Task<string?> GetSASToken(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName, SASTokenRequest properties)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}/ListAccountSas",
                    CLIENT_API_VERSION,
                    null,
                    properties,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<SASTokenResponse>(response.Body).Token;
        }

        /// <summary>
        /// Get an SAS token for the service account
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account</param>
        /// <param name="properties">Other properties for the request</param>
        /// <returns>SAS token value (or NULL)</returns>
        public static async Task<string?> GetSASServiceToken(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName, SASServiceTokenRequest properties)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}/ListServiceSas",
                    CLIENT_API_VERSION,
                    null,
                    properties,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<SASServiceTokenResponse>(response.Body).Token;
        }

        /// <summary>
        /// Get both account keys for the storage account
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account</param>
        /// <returns>List of keys or empty list</returns>
        public static async Task<List<StorageAccountKey>> ListKeys(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}/listKeys",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<StorageAccountKey>();
            }

            return JsonConvert.DeserializeObject<StorageAccountKeyResponse>(response.Body).Keys;
        }


        /// <summary>
        /// Regenerates the provided key and returns the new value
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account</param>
        /// <param name="keyName">Name of the key to regenerate</param>
        /// <returns>New key or NULL</returns>
        public static async Task<string?> RegenerateKey(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName, string keyName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }
            if (string.IsNullOrWhiteSpace(keyName)) { throw new ArgumentNullException(nameof(keyName)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}/regenerateKey",
                    CLIENT_API_VERSION,
                    null,
                    $"{{ \"keyName\" : \"{keyName}\" }}",
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<StorageAccountKeyResponse>(response.Body).Keys.First(k => k.Name.Equals(keyName))?.Key;
        }

        /// <summary>
        /// Restore blobs to a point in time
        /// </summary>
        /// <param name="bearerToken">Azure bearer token</param>
        /// <param name="subscription">Subscription for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the account is placed in</param>
        /// <param name="storageAccountName">Name of the storage account</param>
        /// <param name="restoreTo">Timestamp of point in time to restore up to</param>
        /// <param name="blobRanges">Ranges of blobs to restore</param>
        /// <returns>True if task has been accepted. False/NULL otherwise</returns>
        public static async Task<bool?> RestoreBlobs(string bearerToken, Guid subscription, string resourceGroupName, string storageAccountName,
                string restoreTo, IEnumerable<BlobRestoreRange> blobRanges)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(storageAccountName)) { throw new ArgumentNullException(nameof(storageAccountName)); }
            if (string.IsNullOrWhiteSpace(restoreTo)) { throw new ArgumentNullException(nameof(restoreTo)); }
            if (!blobRanges.Any()) { throw new ArgumentNullException(nameof(blobRanges)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.Storage/storageAccounts/{storageAccountName}/restoreBlobRanges",
                    CLIENT_API_VERSION,
                    null,
                    new BlobRestoreRequest()
                    {
                        PointInTime = restoreTo,
                        Ranges = new List<BlobRestoreRange>(blobRanges)
                    },
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return (JsonConvert.DeserializeObject<BlobRestoreStatus>(response.Body).Status != BlobRestoreProgress.Failed);
        }


        public static string CLIENT_API_VERSION = "2019-06-01";
    }
}
