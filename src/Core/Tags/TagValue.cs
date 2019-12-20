using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Core.Tags
{
    /// <summary>
    /// A value of a tag
    /// </summary>
    public class TagValue
    {
        /// <summary>
        /// The number of instances of this tag value
        /// </summary>
        [JsonProperty("count")]
        public Count Count { get; set; }

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


        public TagValue()
        {
            Count = new Count();
            Id = string.Empty;
            Value = string.Empty;
        }
    }
}
