using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.LogAnalytics;
using SujaySarma.Sdk.Azure.Compute.Usages;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    /// <summary>
    /// Class to encapsulate calls to miscellaneous Compute methods that do not 
    /// justify their own class to have them.
    /// </summary>
    public static class MiscServicesClient
    {

        /// <summary>
        /// Get a list of usage information
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="locationName">Internal/short name of the location (eg: "westus")</param>
        /// <returns>List of usages. Empty list if there was a problem.</returns>
        public static async Task<List<Usage>> GetUsages(string bearerToken, Guid subscription, string locationName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<Usage>(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/providers/Microsoft.Compute/locations/{locationName}/usages",
                    "2019-03-01",
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<Usage>();
            }


            return JsonConvert.DeserializeObject<ListResultWithContinuations<Usage>>(response.Body).Values;
        }


        /// <summary>
        /// Request AzureRM API to export the log analytics of a compute resources to the provided SAS-enabled blob
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="locationName">Internal/short name of the location (eg: "westus") -- logs for all Compute resources in this region will be exported</param>
        /// <param name="type">Type of log to export</param>
        /// <param name="startTimeUtc">Start date/time (in UTC) window to fetch the logs from</param>
        /// <param name="endTimeUtc">End date/time (in UTC) window to fetch the logs upto</param>
        /// <param name="sasEnabledBlobUri">The SAS URL to a blob that the Azure API can WRITE to</param>
        /// <param name="intervals">Intervals of time to place in the log data (eg: if set to ThreeMins, log analytics data for every 3 minutes from <paramref name="startTimeUtc"/> will be placed on the blob)</param>
        /// <param name="grouping">Type of grouping to perform on the exported data</param>
        /// <returns>Full path to the blob URI where the log file was written. NOTE THAT this will LOSE its SAS data!!!</returns>
        public static async Task<string?> ExportLogAnalytics(string bearerToken, Guid subscription, string locationName, ComputeAnalyticsLogType type,
            DateTime startTimeUtc, DateTime endTimeUtc, string sasEnabledBlobUri,
                ComputeLogAnalyticsRequestIntervals intervals = ComputeLogAnalyticsRequestIntervals.ThreeMins, ComputeAnalyticsLogGrouping grouping = ComputeAnalyticsLogGrouping.None)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }

            string apiEndpointName = type switch
            {
                ComputeAnalyticsLogType.RequestRate => "getRequestRateByInterval",
                ComputeAnalyticsLogType.ThrottleRate => "getThrottledRequests",
                _ => throw new ArgumentOutOfRangeException(nameof(apiEndpointName))
            };

            ComputeLogAnalyticsRequest request = new ComputeLogAnalyticsRequest()
            {
                TimeWindowStart = startTimeUtc,
                TimeWindowEnd = endTimeUtc,
                BlobContainerSasUri = sasEnabledBlobUri,
                Length = intervals,
                GroupByOperationName = grouping.HasFlag(ComputeAnalyticsLogGrouping.Operation),
                GroupByResourceName = grouping.HasFlag(ComputeAnalyticsLogGrouping.Resource),
                GroupByThrottlePolicy = grouping.HasFlag(ComputeAnalyticsLogGrouping.ThrottlePolicy)
            };

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/providers/Microsoft.Compute/locations/{locationName}/logAnalytics/apiAccess/{apiEndpointName}",
                    "2019-03-01",
                    null,
                    request,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ComputeLogAnalyticsResponse>(response.Body).Properties?.OutputFileUri;
        }


    }

    /// <summary>
    /// Type of compute analytics log
    /// </summary>
    public enum ComputeAnalyticsLogType
    {
        /// <summary>
        /// Request rate over time
        /// </summary>
        RequestRate,

        /// <summary>
        /// Throttling rate over time
        /// </summary>
        ThrottleRate
    }

    /// <summary>
    /// Type of grouping required in the log results
    /// </summary>
    [Flags]
    public enum ComputeAnalyticsLogGrouping
    {
        /// <summary>
        /// No grouping
        /// </summary>
        None = 0,

        /// <summary>
        /// Grouped by operation name
        /// </summary>
        Operation = 1,

        /// <summary>
        /// Grouped by resource name
        /// </summary>
        Resource = 2,

        /// <summary>
        /// Grouped by throttling policy
        /// </summary>
        ThrottlePolicy = 4
    }
}
