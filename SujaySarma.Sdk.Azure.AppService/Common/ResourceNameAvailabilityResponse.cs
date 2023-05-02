
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Common
{
    /// <summary>
    /// Response to a check for a resource name's availability
    /// </summary>
    public class ResourceNameAvailabilityResponse
    {

        /// <summary>
        /// If Result==false & ResultReason==Invalid, contains the validation error data
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        /// True if name is valid, available and a new resource can be created with it. If False, it means do not use.
        /// </summary>
        [JsonProperty("nameAvailable")]
        public bool Result { get; set; }

        /// <summary>
        /// If Result==false, provides the reason why: Invalid or AlreadyExists.
        /// </summary>
        [JsonProperty("reason", ItemConverterType = typeof(ResourceNameUnavailabilityReason))]
        public ResourceNameUnavailabilityReason ResultReason { get; set; }


        public ResourceNameAvailabilityResponse() { }
    }
}
