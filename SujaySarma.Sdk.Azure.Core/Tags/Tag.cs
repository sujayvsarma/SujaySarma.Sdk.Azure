using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Core.Tags
{
    /// <summary>
    /// Data about a tag. This is purely a result-data structure and there are no code flows 
    /// that require the caller to generate this data.
    /// </summary>
    public class Tag
    {

        /// <summary>
        /// The ID URI of a tag
        /// </summary>
        [JsonProperty("id")]
        public string TagId { get; set; }

        /// <summary>
        /// Name of the tag
        /// </summary>
        [JsonProperty("tagName")]
        public string Name { get; set; }

        /// <summary>
        /// The number of instances of this tag across all resources in this subscription
        /// </summary>
        [JsonProperty("count")]
        public ResultValueAggregate Count { get; set; }

        /// <summary>
        /// Values in each instance
        /// </summary>
        [JsonProperty("values")]
        public List<TagValue> Values { get; set; }

        /// <summary>
        /// Create a new tag
        /// </summary>
        public Tag()
        {
            TagId = string.Empty;
            Name = string.Empty;
            Count = new ResultValueAggregate();
            Values = new List<TagValue>();
        }
    }
}
