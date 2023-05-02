using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// The plan in a <see cref="IndexItem"/>
    /// </summary>
    public class MarketplaceItemPlan
    {
        /// <summary>
        /// Id of this plan
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Another Id for this plan. 
        /// </summary>
        [JsonProperty("skuId")]
        public string SkuId { get; set; } = string.Empty;

        /// <summary>
        /// Another Id for this plan
        /// </summary>
        [JsonProperty("planId")]
        public string PlanId { get; set; } = string.Empty;

        /// <summary>
        /// Legacy Id of this plan
        /// </summary>
        [JsonProperty("legacyPlanId")]
        public string LegacyPlanId { get; set; } = string.Empty;

        /// <summary>
        /// Version of plan (not the version of the product in the plan!!!)
        /// </summary>
        [JsonProperty("version")]
        public string PlanVersion { get; set; } = string.Empty;

        /// <summary>
        /// User-friendly display name of plan
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Another internal name for this item
        /// </summary>
        [JsonProperty("itemName")]
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// A shorter form of the description, typically plain-text
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Description. Usually long and could contain HTML.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Type of item
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Absolute path to a Json file that **may** contain useful information for the UI
        /// </summary>
        [JsonProperty("uiDefinitionUri")]
        public string UIDefinitionUri { get; set; } = string.Empty;

        /// <summary>
        /// Alternate reference Id. This value **may** also be present within the <see cref="Metadata"/> dictionary.
        /// </summary>
        [JsonProperty("altStackReference")]
        public string AltStackReference { get; set; } = string.Empty;

        /// <summary>
        /// Keywords to match specific plan
        /// </summary>
        [JsonProperty("keywords")]
        public List<string>? Keywords { get; set; }

        /// <summary>
        /// List of category Ids this item appears under
        /// </summary>
        [JsonProperty("categoryIds")]
        public List<string>? Categories { get; set; }

        /// <summary>
        /// Metadata. Of particular use is the one with the key "altStackReference" -- value 
        /// is the Id of an alternate plan that the user may prefer.
        /// </summary>
        [JsonProperty("metadata")]
        public Dictionary<string, object>? Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Json templates.
        /// </summary>
        [JsonProperty("artifacts")]
        public List<SkuArtifact>? Artifacts { get; set; } = new List<SkuArtifact>();

        /// <summary>
        /// The software stack or version?
        /// </summary>
        [JsonProperty("stackType")]
        public string? Stack { get; set; }

        /// <summary>
        /// The behavior to exhibit for CSP SKUs
        /// </summary>
        [JsonProperty("cspState")]
        public string? CloudServiceProviderState { get; set; }

        /// <summary>
        /// If set, this plan is private to the subscriber
        /// </summary>
        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// If set, this plan is hidden (from search?)
        /// </summary>
        [JsonProperty("isHidden")]
        public bool IsHidden { get; set; }

        /// <summary>
        /// If set, the SKU offers free-trial plans
        /// </summary>
        [JsonProperty("hasFreeTrials")]
        public bool HasFreeTrials { get; set; }

        /// <summary>
        /// If set, this plan is free and will not incur charges.
        /// </summary>
        [JsonProperty("isFree")]
        public bool IsFree { get; set; }

        /// <summary>
        /// If set, do not sell this item
        /// </summary>
        [JsonProperty("isStopSell")]
        public bool IsStopSell { get; set; }

        /// <summary>
        /// If set, the SKU is unlicensed and requires to be licensed with something 
        /// the user has acquired seperately (like an EA license for a Windows Server)
        /// </summary>
        [JsonProperty("isByol")]
        public bool IsBringYourOwnLicense { get; set; }

        /// <summary>
        /// If set, sets this as a PAYG plan
        /// </summary>
        [JsonProperty("isPayg")]
        public bool IsPayAsYouGo { get; set; }

        /// <summary>
        /// If set, is quantifiable -- meaning unclear!
        /// </summary>
        [JsonProperty("isQuantifiable")]
        public bool IsQuantifiable { get; set; }

        /// <summary>
        /// Instantiate the item
        /// </summary>
        public MarketplaceItemPlan() { }
    }
}
