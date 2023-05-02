using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Compute.Common;

using System;

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
        [JsonProperty("access"), JsonConverter(typeof(StringEnumConverter))]
        public AccessLevelEnum AccessLevelRequired { get; set; } = AccessLevelEnum.None;

        /// <summary>
        /// Duration in seconds until the granted SAS token expires
        /// </summary>
        [JsonProperty("durationInSeconds")]
        public int AccessExpireInSeconds { get; set; } = 0;


        public DiskAccessRequest() { }

        /// <summary>
        /// Create a new request
        /// </summary>
        /// <param name="required">Level of access required</param>
        /// <param name="expireIn">Number of seconds to expire the SAS token</param>
        public DiskAccessRequest(AccessLevelEnum required, int expireIn)
        {
            if (!Enum.IsDefined(typeof(AccessLevelEnum), required)) { throw new ArgumentOutOfRangeException(nameof(required)); }
            if ((expireIn < 1) || (expireIn > 86400)) { throw new ArgumentOutOfRangeException(nameof(expireIn)); }

            AccessLevelRequired = required;
            AccessExpireInSeconds = expireIn;
        }

    }
}
