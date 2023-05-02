using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.Domains;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    /// <summary>
    /// Client to find, purchase and manage App Service Domains
    /// </summary>
    public static class AppServiceDomainClient
    {

        /// <summary>
        /// Check if a domain name is available
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="domainName">Domain name to check for availability. Note that the Azure App Service Domain only supports 
        /// checking and purchase of selected domain TLDs.</param>
        /// <returns>True if available, False if not, NULL if there was an error.</returns>
        public static async Task<bool?> IsAvailable(string bearerToken, Guid subscription, string domainName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            // this will cause validation of the domainName and will throw!
            AvailabilityRequest request = new AvailabilityRequest(domainName);

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.DomainRegistration/checkDomainAvailability",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            AvailabilityResult result = JsonConvert.DeserializeObject<AvailabilityResult>(response.Body);
            return result.IsAvailable;
        }


        /// <summary>
        /// Create a request to register a new domain name
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="domainName">Domain name to purchase. Note that the Azure App Service Domain only supports 
        /// purchase of selected domain TLDs.</param>
        /// <param name="targetResourceGroupName">The Azure Resource Group to place the registered domain in</param>
        /// <param name="autoRenew">If set, Azure will auto-renew the domain every year</param>
        /// <param name="registrationContact">Contact information to use for registration</param>
        /// <param name="callerIpAddress">IP address of caller for purchase consent agreement</param>
        /// <param name="agreementKeyNames">Name of the keys of terms and conditions that have been agreed to</param>
        /// <param name="useCustomDns">If set, will not use Azure DNS for the domain (if Azure DNS is required later, it will need to be 
        /// configured seperately)</param>
        /// <param name="existingAzureZone">If an existing Azure DNS Zone is to be tied to this domain, the resource Uri to that zone</param>
        /// <returns>True if the domain registration task was accepted by Azure. False if not, NULL if there were errors</returns>
        public static async Task<bool?> Register(string bearerToken, Guid subscription, string domainName, string targetResourceGroupName, bool autoRenew,
            DomainRegistrationContact registrationContact, string callerIpAddress, string[] agreementKeyNames,
                bool useCustomDns = false, ResourceUri? existingAzureZone = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(targetResourceGroupName)) { throw new ArgumentNullException(nameof(targetResourceGroupName)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            // validate domain name :-)
            AvailabilityRequest _ = new AvailabilityRequest(domainName);

            RegistrationRequest request = new RegistrationRequest(autoRenew, registrationContact, callerIpAddress, agreementKeyNames);
            if (useCustomDns)
            {
                request.Properties.TypeOfDns = DomainRegistrationDnsTypeEnum.DefaultDomainRegistrarDns;
            }

            if (existingAzureZone != null)
            {
                if ((!existingAzureZone.IsValid)
                    || (!existingAzureZone.Is(ResourceUriCompareLevel.Provider, "Microsoft.Network"))
                        || (!existingAzureZone.Is(ResourceUriCompareLevel.Type, "dnszones")))
                {
                    throw new ArgumentException(nameof(existingAzureZone));
                }

                request.Properties.AzureDnsZoneId = existingAzureZone.ToString();
            }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{targetResourceGroupName}/providers/Microsoft.DomainRegistration/domains/{domainName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Create a request to register a new domain name. This overload automatically validates the domain name and picks up the 
        /// required consents to populate for the request.
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="domainName">Domain name to purchase. Note that the Azure App Service Domain only supports 
        /// purchase of selected domain TLDs.</param>
        /// <param name="targetResourceGroupName">The Azure Resource Group to place the registered domain in</param>
        /// <param name="autoRenew">If set, Azure will auto-renew the domain every year</param>
        /// <param name="registrationContact">Contact information to use for registration</param>
        /// <param name="callerIpAddress">IP address of caller for purchase consent agreement</param>
        /// <param name="useCustomDns">If set, will not use Azure DNS for the domain (if Azure DNS is required later, it will need to be 
        /// configured seperately)</param>
        /// <param name="existingAzureZone">If an existing Azure DNS Zone is to be tied to this domain, the resource Uri to that zone</param>
        /// <returns>True if the domain registration task was accepted by Azure. False if not, NULL if there were errors</returns>
        public static async Task<bool?> Register(string bearerToken, Guid subscription, string domainName, string targetResourceGroupName, bool autoRenew,
            DomainRegistrationContact registrationContact, string callerIpAddress, bool useCustomDns = false, ResourceUri? existingAzureZone = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(targetResourceGroupName)) { throw new ArgumentNullException(nameof(targetResourceGroupName)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            IList<TopLevelDomain> supportedTlds = await GetSupportedTopLevelDomains(bearerToken, subscription);
            string? tldName = Common.Extensions.GetPossibleTopLevelDomainName(domainName);
            if (string.IsNullOrWhiteSpace(tldName)) { throw new ArgumentNullException($"Could not retrieve TLD of '{domainName}'."); }

            TopLevelDomain? tld = supportedTlds.FirstOrDefault(t => t.Name == tldName);
            if (tld == null)
            {
                throw new ArgumentNullException($"The TLD '{tldName}' is not supported at this time.");
            }

            IList<TopLevelAgreement> agreements = await GetRequiredConsents(bearerToken, subscription, tld.Name, true, false);
            string[] agreementNames = agreements.Select(a => a.AgreementCode).ToArray();

            RegistrationRequest request = new RegistrationRequest(autoRenew, registrationContact, callerIpAddress, agreementNames);
            if (useCustomDns)
            {
                request.Properties.TypeOfDns = DomainRegistrationDnsTypeEnum.DefaultDomainRegistrarDns;
            }

            if (existingAzureZone != null)
            {
                if ((!existingAzureZone.IsValid)
                    || (!existingAzureZone.Is(ResourceUriCompareLevel.Provider, "Microsoft.Network"))
                        || (!existingAzureZone.Is(ResourceUriCompareLevel.Type, "dnszones")))
                {
                    throw new ArgumentException(nameof(existingAzureZone));
                }

                request.Properties.AzureDnsZoneId = existingAzureZone.ToString();
            }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{targetResourceGroupName}/providers/Microsoft.DomainRegistration/domains/{domainName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Delete a domain registration. The domain registration must be available in the specified Azure subscription.
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the Azure resource group the Azure App Service Domain is homed in</param>
        /// <param name="domainName">Domain registration to delete</param>
        /// <param name="forceDelete">If set, deletes immediately. Otherwise will delete after 24 hours</param>
        /// <returns>True if request was accepted, False if not, NULL if there was an exception</returns>
        public static async Task<bool?> Delete(string bearerToken, Guid subscription, string resourceGroupName, string domainName, bool forceDelete = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(domainName)) { throw new ArgumentNullException(nameof(domainName)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.DomainRegistration/domains/{domainName}",
                    CLIENT_API_VERSION,
                    new Dictionary<string, string>()
                    {
                        { "forceHardDeleteDomain", (forceDelete ? "true" : "false") }
                    }
                    , new int[] { 200, 204 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Forcibly renew a domain registration (eg: before it is due to be renewed)
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the Azure resource group the Azure App Service Domain is homed in</param>
        /// <param name="domainName">Domain registration to renew</param>
        /// <returns>True if request was accepted, False if not, NULL if there was an exception</returns>
        public static async Task<bool?> ForceRenew(string bearerToken, Guid subscription, string resourceGroupName, string domainName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(domainName)) { throw new ArgumentNullException(nameof(domainName)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.DomainRegistration/domains/{domainName}/renew",
                    CLIENT_API_VERSION, null, null,
                    new int[] { 200, 202, 204 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Get an existing App Service Domain
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">Name of the Azure resource group the Azure App Service Domain is homed in</param>
        /// <param name="domainName">App Service Domain name</param>
        /// <returns>App Service Domain metadata, or NULL if not found</returns>
        public static async Task<AppServiceDomain?> Get(string bearerToken, Guid subscription, string resourceGroupName, string domainName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(domainName)) { throw new ArgumentNullException(nameof(domainName)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{resourceGroupName}/providers/Microsoft.DomainRegistration/domains/{domainName}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AppServiceDomain>(response.Body);
        }

        /// <summary>
        /// List existing App Service Domains
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="resourceGroupName">(Optional) Name of the Azure resource group the Azure App Service Domains are homed in. 
        /// If not provided (NULL), lists all domains in the subscription</param>
        /// <returns>List of App Service Domain metadata, or empty list if not found</returns>
        public static async Task<IList<AppServiceDomain>> List(string bearerToken, Guid subscription, string? resourceGroupName = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            StringBuilder requestUri = new StringBuilder();
            requestUri.Append($"https://management.azure.com/subscriptions/{subscription:d}");
            if (!string.IsNullOrWhiteSpace(resourceGroupName))
            {
                requestUri.Append($"/resourceGroups/{resourceGroupName}");
            }
            requestUri.Append("/providers/Microsoft.DomainRegistration/domains");

            RestApiResponse response = await RestApiClient.GETWithContinuations<AppServiceDomain>(
                    bearerToken,
                    requestUri.ToString(),
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<AppServiceDomain>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<AppServiceDomain>>(response.Body).Values;
        }

        /// <summary>
        /// Get recommendations for available domains
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="keywords">Collection of keywords. Provide one keyword per element</param>
        /// <param name="maxRecommendations">Maximum number of recommendations to return. While Azure does not seem to have an upper limit, 
        /// this function enforces a limit of 25 names per call.</param>
        /// <returns>Collection of strings - recommendations for names. Empty list if there was a problem</returns>
        public static async Task<IList<string>> GetDomainNameRecommendations(string bearerToken, Guid subscription, IEnumerable<string> keywords, int maxRecommendations = 5)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if ((keywords == null) || (!keywords.Any())) { throw new ArgumentNullException(nameof(keywords)); }
            if ((maxRecommendations < 1) || (maxRecommendations > 25)) { throw new ArgumentOutOfRangeException(nameof(maxRecommendations)); }

            DomainNameRecommendationRqeuest request = new DomainNameRecommendationRqeuest()
            {
                Keywords = string.Join(",", keywords),
                MaximumRecommendations = maxRecommendations
            };

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.DomainRegistration/listDomainRecommendations",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<string>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<KeyValuePair<string, string>>>(response.Body).Values
                .Select(n => n.Value).ToList();
        }

        /// <summary>
        /// Create a request to transfer a new domain name
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="domainName">Domain name to transfer. Note that the Azure App Service Domain only supports 
        /// purchase of selected domain TLDs.</param>
        /// <param name="transferAuthorization">Authorization code received from current Domain Registrar to authorize this transfer request</param>
        /// <param name="targetResourceGroupName">The Azure Resource Group to place the registered domain in</param>
        /// <param name="autoRenew">If set, Azure will auto-renew the domain every year</param>
        /// <param name="registrationContact">Contact information to use for transfer</param>
        /// <param name="callerIpAddress">IP address of caller for purchase consent agreement</param>
        /// <param name="agreementKeyNames">Name of the keys of terms and conditions that have been agreed to</param>
        /// <param name="useCustomDns">If set, will not use Azure DNS for the domain (if Azure DNS is required later, it will need to be 
        /// configured seperately)</param>
        /// <param name="existingAzureZone">If an existing Azure DNS Zone is to be tied to this domain, the resource Uri to that zone</param>
        /// <returns>True if the domain registration task was accepted by Azure. False if not, NULL if there were errors</returns>
        public static async Task<bool?> TransferDomain(string bearerToken, Guid subscription, string domainName, string transferAuthorization,
            string targetResourceGroupName, bool autoRenew,
                DomainRegistrationContact registrationContact, string callerIpAddress, string[] agreementKeyNames,
                    bool useCustomDns = false, ResourceUri? existingAzureZone = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(targetResourceGroupName)) { throw new ArgumentNullException(nameof(targetResourceGroupName)); }
            if (string.IsNullOrWhiteSpace(transferAuthorization)) { throw new ArgumentNullException(nameof(transferAuthorization)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if ((agreementKeyNames == null) || (!agreementKeyNames.Any())) { throw new ArgumentNullException(nameof(agreementKeyNames)); }

            // validate domain name :-)
            AvailabilityRequest _ = new AvailabilityRequest(domainName);

            DomainTransferRequest request = new DomainTransferRequest(transferAuthorization, autoRenew, registrationContact, callerIpAddress, agreementKeyNames);
            if (useCustomDns)
            {
                request.Properties.TypeOfDns = DomainRegistrationDnsTypeEnum.DefaultDomainRegistrarDns;
            }

            if (existingAzureZone != null)
            {
                if ((!existingAzureZone.IsValid)
                    || (!existingAzureZone.Is(ResourceUriCompareLevel.Provider, "Microsoft.Network"))
                        || (!existingAzureZone.Is(ResourceUriCompareLevel.Type, "dnszones")))
                {
                    throw new ArgumentException(nameof(existingAzureZone));
                }

                request.Properties.AzureDnsZoneId = existingAzureZone.ToString();
            }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{targetResourceGroupName}/providers/Microsoft.DomainRegistration/domains/{domainName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Create a request to transfer a new domain name. This overload automatically validates the domain name and picks up the 
        /// required consents to populate for the request.
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="domainName">Domain name to transfer. Note that the Azure App Service Domain only supports 
        /// purchase of selected domain TLDs.</param>
        /// <param name="transferAuthorization">Authorization code received from current Domain Registrar to authorize this transfer request</param>
        /// <param name="targetResourceGroupName">The Azure Resource Group to place the registered domain in</param>
        /// <param name="autoRenew">If set, Azure will auto-renew the domain every year</param>
        /// <param name="registrationContact">Contact information to use for transfer</param>
        /// <param name="callerIpAddress">IP address of caller for purchase consent agreement</param>
        /// <param name="useCustomDns">If set, will not use Azure DNS for the domain (if Azure DNS is required later, it will need to be 
        /// configured seperately)</param>
        /// <param name="existingAzureZone">If an existing Azure DNS Zone is to be tied to this domain, the resource Uri to that zone</param>
        /// <returns>True if the domain registration task was accepted by Azure. False if not, NULL if there were errors</returns>
        public static async Task<bool?> TransferDomain(string bearerToken, Guid subscription, string domainName, string transferAuthorization,
            string targetResourceGroupName, bool autoRenew, DomainRegistrationContact registrationContact, string callerIpAddress,
                    bool useCustomDns = false, ResourceUri? existingAzureZone = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(targetResourceGroupName)) { throw new ArgumentNullException(nameof(targetResourceGroupName)); }
            if (string.IsNullOrWhiteSpace(transferAuthorization)) { throw new ArgumentNullException(nameof(transferAuthorization)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            IList<TopLevelDomain> supportedTlds = await GetSupportedTopLevelDomains(bearerToken, subscription);
            string? tldName = Common.Extensions.GetPossibleTopLevelDomainName(domainName);
            if (string.IsNullOrWhiteSpace(tldName)) { throw new ArgumentNullException($"Could not retrieve TLD of '{domainName}'."); }

            TopLevelDomain? tld = supportedTlds.FirstOrDefault(t => t.Name == tldName);
            if (tld == null)
            {
                throw new ArgumentNullException($"The TLD '{tldName}' is not supported at this time.");
            }

            IList<TopLevelAgreement> agreements = await GetRequiredConsents(bearerToken, subscription, tld.Name, true, true);
            string[] agreementNames = agreements.Select(a => a.AgreementCode).ToArray();

            DomainTransferRequest request = new DomainTransferRequest(transferAuthorization, autoRenew, registrationContact, callerIpAddress, agreementNames);
            if (useCustomDns)
            {
                request.Properties.TypeOfDns = DomainRegistrationDnsTypeEnum.DefaultDomainRegistrarDns;
            }

            if (existingAzureZone != null)
            {
                if ((!existingAzureZone.IsValid)
                    || (!existingAzureZone.Is(ResourceUriCompareLevel.Provider, "Microsoft.Network"))
                        || (!existingAzureZone.Is(ResourceUriCompareLevel.Type, "dnszones")))
                {
                    throw new ArgumentException(nameof(existingAzureZone));
                }

                request.Properties.AzureDnsZoneId = existingAzureZone.ToString();
            }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/resourceGroups/{targetResourceGroupName}/providers/Microsoft.DomainRegistration/domains/{domainName}",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200, 202 }
                );

            return (response.WasException ? (bool?)null : response.IsExpectedSuccess);
        }

        /// <summary>
        /// Get a list of all supported top level domains (TLDs)
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <returns>List of supported TLDs or empty list</returns>
        public static async Task<IList<TopLevelDomain>> GetSupportedTopLevelDomains(string bearerToken, Guid subscription)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.DomainRegistration/topLevelDomains",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<TopLevelDomain>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<TopLevelDomain>>(response.Body).Values;
        }

        /// <summary>
        /// Get metadata for a supported top level domains (TLDs)
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="tldName">Tld name, without a prefixed '.' (eg: "com" for ".com")</param>
        /// <returns>Metadata of supported TLDs or NULL</returns>
        public static async Task<TopLevelDomain?> GetSupportedTopLevelDomain(string bearerToken, Guid subscription, string tldName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(tldName)) { throw new ArgumentNullException(nameof(tldName)); }

            if (tldName[0] == '.')
            {
                tldName = tldName[1..];
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.DomainRegistration/topLevelDomains/{tldName}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<TopLevelDomain>(response.Body);
        }

        /// <summary>
        /// Get a list of all consents required for a particular TLD
        /// </summary>
        /// <param name="bearerToken">The Azure bearer token</param>
        /// <param name="subscription">Subscription Id for authorization</param>
        /// <param name="tldName">Tld name, without a prefixed '.' (eg: "com" for ".com")</param>
        /// <param name="includePrivacyConsents">If true retrieves consents required for privacy</param>
        /// <param name="includeTransferConsents">If true retrieves consents for domain transfer</param>
        /// <returns>List of supported TLDs or empty list</returns>
        public static async Task<IList<TopLevelAgreement>> GetRequiredConsents(string bearerToken, Guid subscription, string tldName,
            bool includePrivacyConsents = true, bool includeTransferConsents = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(tldName)) { throw new ArgumentNullException(nameof(tldName)); }

            TopLevelAgreementRequest request = new TopLevelAgreementRequest()
            {
                IncludeAgreementsForDomainTransfer = includeTransferConsents,
                IncludePrivacyAgreements = includePrivacyConsents
            };

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.DomainRegistration/topLevelDomains/{tldName}/listAgreements",
                    CLIENT_API_VERSION,
                    null, request,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<TopLevelAgreement>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<TopLevelAgreement>>(response.Body).Values;
        }




        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
