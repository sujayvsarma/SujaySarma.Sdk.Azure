using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SujaySarma.Sdk.Azure.Compute.Common;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    /// <summary>
    /// Structure to request a SAS token for a disk
    /// </summary>
    public class DiskAccessRequest
    {
        /// <summary>
        /// Access level required
        /// </summary>
        [JsonProperty("access", ItemConverterType = typeof(StringEnumConverter))]
        public AccessLevelEnum AccessLevelRequired { get; set; } = AccessLevelEnum.None;

        /// <summary>
        /// Duration in seconds until the granted SAS token expires
        /// </summary>
        [JsonProperty("durationInSeconds")]
        public int AccessExpireInSeconds { get; set; } = 0;


        public DiskAccessRequest() { }

    }
}
