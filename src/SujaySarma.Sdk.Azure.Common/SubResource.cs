using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    public class SubResource
    {
        /// <summary>
        /// ResourceId URI of the subresource
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; } = string.Empty;

        public SubResource() { }

    }
}
