using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.Certificates;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    public static partial class AppServiceCertificateClient
    {

        /// <summary>
        /// Get certificates associated with the given certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>List of certificates or empty list</returns>
        public static async Task<IList<IssuedCertificate>> GetCertificates(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<IssuedCertificate>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/certificates",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<IssuedCertificate>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<IssuedCertificate>>(response.Body).Values;
        }

        /// <summary>
        /// Gets a single certificate associated with the given certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <param name="certificateName">Name of the certificate (usually the same as the order nickname)</param>
        /// <returns>Certificates or NULL</returns>
        public static async Task<IssuedCertificate?> GetCertificate(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname, string certificateName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }
            if (string.IsNullOrWhiteSpace(certificateName)) { throw new ArgumentNullException(nameof(certificateName)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<IssuedCertificate>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/certificates/{certificateName}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IssuedCertificate>(response.Body);
        }

        /// <summary>
        /// Issue a certificate
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <param name="certificateName">A name of the certificate to identify it later</param>
        /// <param name="keyVaultId">Resource Uri to the KeyVault to hold the private key for the certificate</param>
        /// <param name="privateKeySecretName">Name of the private key for this certificate in the Azure KeyVault</param>
        /// <returns>The issued certificate or NULL</returns>
        public static async Task<IssuedCertificate?> Issue(string bearerToken, Guid subscription, string resourceGroupName,
            string orderNickname, string certificateName, ResourceUri keyVaultId, string privateKeySecretName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }
            if (string.IsNullOrWhiteSpace(certificateName)) { throw new ArgumentNullException(nameof(certificateName)); }
            if ((keyVaultId == null) || (!keyVaultId.IsValid) || (!keyVaultId.Is(ResourceUriCompareLevel.Provider, "Microsoft.KeyVault")))
            {
                throw new ArgumentException(nameof(keyVaultId));
            }

            CertificateIssueRequest request = new CertificateIssueRequest()
            {
                Kind = "certificates",
                Location = "global",
                Properties = new CertificateIssueRequestProperties()
                {
                    KeyVaultId = keyVaultId.ToString(),
                    KeyVaultSecretName = privateKeySecretName
                }
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/certificates/{certificateName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IssuedCertificate>(response.Body);
        }

        /// <summary>
        /// Reissue the previous certificate
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> Reissue(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/reissue",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 204 }
                );

            if (response.WasException)
            {
                return null;
            }

            return response.IsExpectedSuccess;
        }

        /// <summary>
        /// Renew the currently issued certificate associated with the given order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> Renew(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/renew",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 204 }
                );

            if (response.WasException)
            {
                return null;
            }

            return response.IsExpectedSuccess;
        }

        /// <summary>
        /// Delete a certificate associated with the given certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <param name="certificateName">Name of the certificate (usually the same as the order nickname)</param>
        /// <returns>Certificates or NULL</returns>
        public static async Task<bool?> DeleteCertificate(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname, string certificateName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }
            if (string.IsNullOrWhiteSpace(certificateName)) { throw new ArgumentNullException(nameof(certificateName)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/certificates/{certificateName}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 204 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }
    }
}
