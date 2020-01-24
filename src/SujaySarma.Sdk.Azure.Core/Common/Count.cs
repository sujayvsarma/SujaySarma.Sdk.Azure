using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// A count value
    /// </summary>
    public class Count
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


        public Count()
        {
            Type = "Total";
            Value = 0;
        }
    }
}
