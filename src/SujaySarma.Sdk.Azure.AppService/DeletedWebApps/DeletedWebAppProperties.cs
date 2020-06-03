using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.AppService.DeletedWebApps
{
    /// <summary>
    /// Properties of a deleted web app
    /// </summary>
    public class DeletedWebAppProperties
    {

        /// <summary>
        /// Numeric (IIS/Apache) Site Id
        /// </summary>
        [JsonProperty("deletedSiteId")]
        public int SiteId { get; set; }

        /// <summary>
        /// Name of the website
        /// </summary>
        [JsonProperty("deletedSiteName")]
        public string SiteName { get; set; } = string.Empty;

        /// <summary>
        /// Date/time when website was deleted
        /// </summary>
        [JsonProperty("deletedTimestamp")]
        public DateTime DeletedAt { get; set; }

        /// <summary>
        /// Full name (eg: "West US") of the geographic region where the site was hosted
        /// </summary>
        [JsonProperty("geoRegionName")]
        public string GeoRegionName { get; set; } = string.Empty;

        /// <summary>
        /// Kind of site that was deleted. Example: "app" or "api" or "functionapp"
        /// </summary>
        [JsonProperty("kind")]
        public string SiteKind { get; set; } = string.Empty;

        /// <summary>
        /// Name of the resource group that contained this website
        /// </summary>
        [JsonProperty("resourceGroup")]
        public string ResourceGroupName { get; set; } = string.Empty;

        /// <summary>
        /// Name of the slot that held this website
        /// </summary>
        [JsonProperty("slot")]
        public string SlotName { get; set; } = "Production";

        /// <summary>
        /// Host subscription Guid
        /// </summary>
        [JsonProperty("subscription")]
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DeletedWebAppProperties() { }
    }
}
