using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Obscure.Backfill
{

    /// <summary>
    /// Metadata about backfill status of a tenant. This is purely a return structure.
    /// </summary>
    public class TenantBackFillStatus
    {

        /// <summary>
        /// The Guid of the tenant
        /// </summary>
        [JsonProperty("tenantId")]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Status of the backfill operation
        /// </summary>
        [JsonProperty("status", ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public TenantBackfillStatusEnum Status { get; set; }


        public TenantBackFillStatus()
        {
            TenantId = Guid.Empty;
            Status = TenantBackfillStatusEnum.NotStarted;
        }

    }
}
