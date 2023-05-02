using System;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// This is the object returned to the caller for any Usage related API call. 
    /// The original is the UsageAPIResponseItem that is structured VERY strangely!
    /// </summary>
    public class UsageSdkResponseItem
    {

        /// <summary>
        /// The display name
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// The internal counter name
        /// </summary>
        public string CounterName { get; private set; }

        /// <summary>
        /// Maximum limit for this metric. (-1) if not limited
        /// </summary>
        public int MaximumLimit { get; private set; }

        /// <summary>
        /// Current counter value
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Date/time of next reset
        /// </summary>
        public DateTime NextResetAt { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiResponse">The structure received from the Azure API</param>
        public UsageSdkResponseItem(UsageAPIResponseItem apiResponse)
        {
            DisplayName = apiResponse.Name.Localized!;
            CounterName = apiResponse.Name.Value;
            MaximumLimit = apiResponse.Limit;
            Value = apiResponse.Value;
            NextResetAt = apiResponse.NextResetAt;
        }
    }
}
