using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.RateCard.Clients
{
    public static class RateCardClient
    {

        /// <summary>
        /// Get the rate card. This method will take a LONG time to run! It runs with a upstream timeout of 1 min, so 
        /// consuming method should be set to a larger timeout.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="azureSubscriptionOfferId">The Offer Id of the Azure subscription</param>
        /// <param name="isoCurrencyCode">3-letter ISO currency code (eg: USD)</param>
        /// <param name="isoLocaleCode">4-letter ISO locale code (eg: en-US)</param>
        /// <param name="isoRegionCode">2-letter ISO region name (eg: US)</param>
        /// <returns>The ratecard response, or NULL if there was a problem</returns>
        public static async Task<RateCardResponse?> GetRates(string bearerToken, Guid subscription, string azureSubscriptionOfferId, string isoCurrencyCode, string isoLocaleCode, string isoRegionCode)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if ((subscription == Guid.Empty) || (subscription == default)) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(azureSubscriptionOfferId)) { throw new ArgumentNullException(nameof(azureSubscriptionOfferId)); }
            if (string.IsNullOrWhiteSpace(isoCurrencyCode)) { throw new ArgumentNullException(nameof(isoCurrencyCode)); }
            if (string.IsNullOrWhiteSpace(isoLocaleCode)) { throw new ArgumentNullException(nameof(isoLocaleCode)); }
            if (string.IsNullOrWhiteSpace(isoRegionCode)) { throw new ArgumentNullException(nameof(isoRegionCode)); }

            RestApiResponse originalResponse = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription:d}/providers/Microsoft.Commerce/RateCard",
                    CLIENT_API_VERSION,
                    new Dictionary<string, string>()
                    {
                        { "$filter", $"OfferDurableId eq '{azureSubscriptionOfferId}' and Currency eq '{isoCurrencyCode}' and Locale eq '{isoLocaleCode}' and RegionInfo eq '{isoRegionCode}'" }
                    },
                    null,
                    new int[] { 302 }
                );

            if ((!originalResponse.IsExpectedSuccess) || originalResponse.WasException || (originalResponse.Headers == null) || (originalResponse.Headers!.Location == null))
            {
                return null;
            }

            RestApiResponse response = await RestApiClient.GETWithoutAuthentication(
                    originalResponse.Headers!.Location.AbsoluteUri,
                    null, null, null,
                    new int[] { 200 },
                    60                  // set timeout to 1 min
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<RateCardResponse>(response.Body);
        }


        public static string CLIENT_API_VERSION = "2016-08-31-preview";
    }
}
