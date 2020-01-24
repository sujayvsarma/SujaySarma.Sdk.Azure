using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.CertificateOrders;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    /// <summary>
    /// Handles requests to the App Service Certificate API endpoints
    /// </summary>
    public static partial class AppServiceCertificateClient
    {

        /// <summary>
        /// Create a certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group to place the new certificate in</param>
        /// <param name="orderNickname">A nickname for the order</param>
        /// <param name="certificateRequestText">Domain name to transfer. Note that the Azure App Service Domain only supports 
        /// purchase of selected domain TLDs.</param>
        /// <param name="autoRenew">Set to TRUE to autorenew the certificate at the end of the specified validity period</param>
        /// <param name="type">Type of certificate (WARNING: selection has cost implications!)</param>
        /// <param name="validityPeriod">Period in years, that the issued certificate will be valid for</param>
        /// <returns>Details of the placed order</returns>
        public static async Task<CertificateOrderDetail?> Order(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname,
            string domainName, bool autoRenew = true, CertificateTypesEnum type = CertificateTypesEnum.StandardDomainValidatedSsl,
                CertificateValidityPeriod validityPeriod = CertificateValidityPeriod.OneYear)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            AppServiceCertificateOrderRequest request = new AppServiceCertificateOrderRequest()
            {
                Location = "global",
                Name = orderNickname,
                Properties = new AppServiceCertificateOrderRequestProperties()
                {
                    AppServiceDomainName = domainName,
                    AutoRenew = autoRenew,
                    KeySize = CertificateKeySizesEnum.Default,
                    LastGeneratedCertificateRequestText = null,
                    Type = type,
                    Validity = validityPeriod
                }
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}",
                    CLIENT_API_VERSION,
                    null,
                    request,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CertificateOrderDetail>(response.Body);
        }

        /// <summary>
        /// Create a certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group to place the new certificate in</param>
        /// <param name="orderNickname">A nickname for the order</param>
        /// <param name="certificateRequestText">The text content of a certificate request (CSR)</param>
        /// <param name="autoRenew">Set to TRUE to autorenew the certificate at the end of the specified validity period</param>
        /// <param name="type">Type of certificate (WARNING: selection has cost implications!)</param>
        /// <param name="validityPeriod">Period in years, that the issued certificate will be valid for</param>
        /// <returns>Details of the placed order</returns>
        public static async Task<CertificateOrderDetail?> OrderUsingCSR(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname,
            string certificateRequestText, bool autoRenew = true, CertificateTypesEnum type = CertificateTypesEnum.StandardDomainValidatedSsl,
                CertificateValidityPeriod validityPeriod = CertificateValidityPeriod.OneYear)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }
            if (string.IsNullOrWhiteSpace(certificateRequestText)) { throw new ArgumentNullException(nameof(certificateRequestText)); }

            AppServiceCertificateOrderRequest request = new AppServiceCertificateOrderRequest()
            {
                Location = "global",
                Name = orderNickname,
                Properties = new AppServiceCertificateOrderRequestProperties()
                {
                    AppServiceDomainName = null,
                    AutoRenew = autoRenew,
                    KeySize = CertificateKeySizesEnum.Default,
                    LastGeneratedCertificateRequestText = certificateRequestText,
                    Type = type,
                    Validity = validityPeriod
                }
            };

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}",
                    CLIENT_API_VERSION,
                    null,
                    request,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CertificateOrderDetail>(response.Body);
        }

        /// <summary>
        /// Delete a certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> DeleteOrder(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 201 }
                );

            if (response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return response.IsExpectedSuccess;
        }

        /// <summary>
        /// Get a previously created certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>The order detail if found, NULL otherwise</returns>
        public static async Task<CertificateOrderDetail?> GetPreviousOrder(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CertificateOrderDetail>(response.Body);
        }

        /// <summary>
        /// Get a previously created certificate order
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>The list of order details if found, empty list otherwise</returns>
        public static async Task<IList<CertificateOrderDetail>> GetPreviousOrders(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            StringBuilder requestUri = new StringBuilder();
            requestUri.Append($"https://management.azure.com/subscriptions/{subscription:d}");
            if (string.IsNullOrWhiteSpace(resourceGroupName))
            {
                requestUri.Append($"/resourceGroups/{resourceGroupName}");
            }
            requestUri.Append("/providers/Microsoft.CertificateRegistration/certificateOrders");

            RestApiResponse response = await RestApiClient.GETWithContinuations<CertificateOrderDetail>(
                    bearerToken,
                    requestUri.ToString(),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<CertificateOrderDetail>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<CertificateOrderDetail>>(response.Body).Values;
        }

        /// <summary>
        /// Reissue a certificate
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> ReissueCertificate(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
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
        /// Renew a certificate
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> RenewCertificate(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
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
        /// Resend the certificate email
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> ResendCertificateEmail(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/resendEmail",
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
        /// Resend the certificate request email
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> ResendCertificateRequestEmail(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/resendRequestEmails",
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
        /// Verify domain ownership for the domain the certificate order was placed for
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the resource group the certificate exists in</param>
        /// <param name="orderNickname">A nickname for the order, specified during order creation</param>
        /// <returns>True if task was accepted, False if rejected, NULL if there was an error</returns>
        public static async Task<bool?> VerifyDomainOwnership(string bearerToken, Guid subscription, string resourceGroupName, string orderNickname)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(orderNickname)) { throw new ArgumentNullException(nameof(orderNickname)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.CertificateRegistration/certificateOrders/{orderNickname}/verifyDomainOwnership",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 204, 400 }
                );

            if (response.WasException)
            {
                return null;
            }

            if ((response.HttpStatus == 400) && (! string.IsNullOrWhiteSpace(response.Body)) 
                && response.Body.Contains("This request is no longer Pending Validation"))
            {
                return true;
            }

            return response.IsExpectedSuccess;
        }


        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
