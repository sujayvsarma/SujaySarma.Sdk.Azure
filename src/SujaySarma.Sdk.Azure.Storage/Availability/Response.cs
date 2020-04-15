using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage.Availability
{
    public class Response
    {
        /// <summary>
        /// Availability result
        /// </summary>
        [JsonProperty("nameAvailable")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Response() { }
    }
}
