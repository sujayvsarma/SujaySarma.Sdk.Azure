using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Core.Tags
{
    /// <summary>
    /// A value of a tag. This is purely a result-data structure and there are no code flows 
    /// that require the caller to generate this data.
    /// </summary>
    public class TagValue
    {
        /// <summary>
        /// The number of instances of this tag value
        /// </summary>
        [JsonProperty("count")]
        public ResultValueAggregate Count { get; set; }

        /// <summary>
        /// Unique ID of this instance of the tag
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Specific value of the tag for this instance
        /// </summary>
        [JsonProperty("tagValue")]
        public string Value { get; set; }

        /// <summary>
        /// Create a new value element for a tag
        /// </summary>
        public TagValue()
        {
            Count = new ResultValueAggregate();
            Id = string.Empty;
            Value = string.Empty;
        }
    }
}
