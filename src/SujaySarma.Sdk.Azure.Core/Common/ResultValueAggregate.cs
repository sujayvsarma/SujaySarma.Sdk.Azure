using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// A count value. Returned by multiple Azure API
    /// </summary>
    public class ResultValueAggregate
    {
        /// <summary>
        /// Type of count
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Value of the count
        /// </summary>
        [JsonProperty("value")]
        public int Value { get; set; }


        public ResultValueAggregate()
        {
            Type = "Total";
            Value = 0;
        }
    }
}
