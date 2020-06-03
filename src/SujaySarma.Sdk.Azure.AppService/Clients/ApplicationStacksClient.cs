using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.RuntimeStacks;
using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AppService.Clients
{
    /// <summary>
    /// Interacts with the Application Stacks endpoint of the App Service system
    /// </summary>
    public static class ApplicationStacksClient
    {

        /// <summary>
        /// Get a list of available application stacks
        /// </summary>
        /// <param name="bearerToken">Azure Bearer Token</param>
        /// <param name="operatingSystem">Operating system to fetch the stacks for</param>
        /// <returns>List of application stacks for the operating system specified, or an empty list</returns>
        public static async Task<IList<ApplicationStack>> GetApplicationStacks(string bearerToken, OSTypeNamesEnum operatingSystem)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (!Enum.IsDefined(typeof(OSTypeNamesEnum), operatingSystem)) { throw new ArgumentOutOfRangeException(nameof(operatingSystem)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<ApplicationStack>(
                    bearerToken,
                    $"https://management.azure.com/providers/Microsoft.Web/availableStacks?osTypeSelected={Enum.GetName(typeof(OSTypeNamesEnum), operatingSystem)}",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<ApplicationStack>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<ApplicationStack>>(response.Body).Values;
        }



        public static string CLIENT_API_VERSION = "2019-08-01";
    }
}
