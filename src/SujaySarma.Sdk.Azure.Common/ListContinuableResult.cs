using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// A listing that provides for a continuation URL
    /// </summary>
    public class ListContinuableResult<T>
    {

        #region Properties

        /// <summary>
        /// List of result values
        /// </summary>
        [JsonProperty("value")]
        public List<T> Values { get; set; }

        /// <summary>
        /// The URL for the next page of results
        /// </summary>
        [JsonProperty("nextLink")]
        public string? NextPage { get; set; }

        #endregion

        /// <summary>
        /// Base initializer
        /// </summary>
        public ListContinuableResult()
        {
            Values = new List<T>();
            NextPage = null;
        }

    }
}
