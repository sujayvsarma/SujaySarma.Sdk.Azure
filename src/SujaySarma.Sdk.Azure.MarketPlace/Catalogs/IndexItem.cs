using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// An item in a catalog's category (for example, the types of VMs that could be created in the "Compute" catalog). The 
    /// original data has a lot of properties, we only return those that make sense.
    /// </summary>
    public class IndexItem
    {

        /// <summary>
        /// Id of this item.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Type of item
        /// </summary>
        [JsonProperty("itemType")]
        public string ItemType { get; set; } = string.Empty;

        /// <summary>
        /// The version of this _definition_. This is not the version of the SKU being sold (eg: it is not the version of Windows!)
        /// </summary>
        [JsonProperty("version")]
        public string ItemVersion { get; set; } = string.Empty;

        /// <summary>
        /// Language of this entry -- not necessarily the language of the SKU!
        /// </summary>
        [JsonProperty("language")]
        public string LanguageCode { get; set; } = string.Empty;

        /// <summary>
        /// User-displayable name
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Id of the publisher of this SKU
        /// </summary>
        [JsonProperty("publisherId")]
        public string? PublisherId { get; set; }

        /// <summary>
        /// User-displayable name of the publisher of this SKU
        /// </summary>
        [JsonProperty("publisherDisplayName")]
        public string? PublisherName { get; set; }

        /// <summary>
        /// Id of the offer
        /// </summary>
        [JsonProperty("offerId")]
        public string OfferId { get; set; } = string.Empty;

        /// <summary>
        /// Previous Id of this offer (legacy portal system). Some places may still reference this item using this Id.
        /// </summary>
        [JsonProperty("legacyId")]
        public string? LegacyOfferId { get; set; }

        /// <summary>
        /// Yet another Id for this item. Usually matches <see cref="LegacyOfferId"/>.
        /// </summary>
        [JsonProperty("bigId")]
        public string? BigId { get; set; }

        /// <summary>
        /// This will have a value if <see cref="HasAmendmentsToStandardContract"/> is TRUE. Typical format is a Guid, but who knows!
        /// </summary>
        [JsonProperty("standardContractAmendmentsRevisionId")]
        public string? ContractAmendmentRevisionId { get; set; }

        /// <summary>
        /// Type of offer
        /// </summary>
        [JsonProperty("offerType")]
        public string? OfferType { get; set; }

        /// <summary>
        /// The shortest of the three forms of description.
        /// </summary>
        [JsonProperty("summary")]
        public string? ShortSummary { get; set; }

        /// <summary>
        /// This is longer than <see cref="ShortSummary"/>, but shorter than <see cref="Description"/>. Sometimes HTML, sometimes plain-text.
        /// </summary>
        [JsonProperty("longSummary")]
        public string? LongSummary { get; set; }

        /// <summary>
        /// Description about this item. This is typically very long and contains (sometimes improperly formatted) HTML.
        /// </summary>
        [JsonProperty("description")]
        public string? HtmlDescription { get; set; }

        /// <summary>
        /// If set, there are changes to the standard Azure Services Agreement effective for this SKU.
        /// </summary>
        [JsonProperty("hasStandardContractAmendments")]
        public bool HasAmendmentsToStandardContract { get; set; } = false;

        /// <summary>
        /// If set, is private to the subscriber.
        /// </summary>
        [JsonProperty("isPrivate")]
        public bool IsPrivateItem { get; set; } = false;

        /// <summary>
        /// If set, this is a preview version of the SKU
        /// </summary>
        [JsonProperty("isPreview")]
        public bool IsPreviewRelease { get; set; } = false;

        /// <summary>
        /// If set, the SKU offers free-trial plans
        /// </summary>
        [JsonProperty("hasFreeTrials")]
        public bool HasFreeTrials { get; set; } = false;

        /// <summary>
        /// If set, the SKU is unlicensed and requires to be licensed with something 
        /// the user has acquired seperately (like an EA license for a Windows Server)
        /// </summary>
        [JsonProperty("isByol")]
        public bool IsBringYourOwnLicense { get; set; } = false;

        /// <summary>
        /// If set, offers PAYG plans
        /// </summary>
        [JsonProperty("hasPaygPlans")]
        public bool HasPayAsYouGoPlans { get; set; } = false;

        /// <summary>
        /// If set, do not sell this item
        /// </summary>
        [JsonProperty("isStopSell")]
        public bool IsStopSell { get; set; } = false;

        /// <summary>
        /// If set, order must be fulfilled before charges start to accumalate.
        /// </summary>
        /// <remarks>Never seen this set to TRUE. Is this CSP specific?</remarks>
        [JsonProperty("fulfillBeforeChargeEligible")]
        public bool MustFulfillBeforeEligibleForCharges { get; set; } = false;

        /// <summary>
        /// If set, this is a third-party offering (True when publishers other than Microsoft have created SKUs)
        /// </summary>
        [JsonProperty("isThirdParty")]
        public bool IsThirdParty { get; set; } = false;

        /// <summary>
        /// Is this quantifiable? -- meaning unknown.
        /// </summary>
        [JsonProperty("isQuantifiable")]
        public bool IsQuantifiable { get; set; } = false;

        /// <summary>
        /// If set, means that Microsoft is reselling this (most likely <seealso cref="IsThirdParty"/> is set to True).
        /// </summary>
        [JsonProperty("isReseller")]
        public bool IsReseller { get; set; } = false;

        /// <summary>
        /// Marketing/learning material attached to this item
        /// </summary>
        [JsonProperty("marketingMaterial")]
        public MarketingMaterial? MarketingMaterial { get; set; }

        /// <summary>
        /// Ids of (other?) categories this item belongs under
        /// </summary>
        [JsonProperty("categoryIds")]
        public List<string>? Categories { get; set; }

        /// <summary>
        /// List of keywords this item should match during a search
        /// </summary>
        [JsonProperty("keywords")]
        public List<string>? Keywords { get; set; }

        /// <summary>
        /// Links to informational pages
        /// </summary>
        [JsonProperty("links")]
        public List<Link>? DocumentationLinks { get; set; } = new List<Link>();

        /// <summary>
        /// Links to icon files. Key is a name (small, medium, large, wide). Value is the corresponding image (usually PNG).
        /// </summary>
        [JsonProperty("iconFileUris")]
        public Dictionary<string, string>? IconFiles { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Artifacts, usually json-templates
        /// </summary>
        [JsonProperty("artifacts")]
        public List<SkuArtifact>? Artifacts { get; set; } = new List<SkuArtifact>();

        /// <summary>
        /// Usually contains fields like leadGeneration and testDrive (both typically NULL)
        /// </summary>
        [JsonProperty("metadata")]
        public Dictionary<string, object>? Metadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Icon/images for display - This is usually a structured reptition of what is already there in <seealso cref="IconFiles"/>.
        /// </summary>
        [JsonProperty("images")]
        public List<ImageContextItem>? Images { get; set; } = new List<ImageContextItem>();

        /// <summary>
        /// The definition template (more often than not, this is NULL !!!)
        /// </summary>
        [JsonProperty("definitionTemplates")]
        public DefinitionTemplate? DefinitionTemplate { get; set; }

        /// <summary>
        /// Plans offered by this SKU
        /// </summary>
        [JsonProperty("plans")]
        public List<MarketplaceItemPlan>? Plans { get; set; } = new List<MarketplaceItemPlan>();

        /// <summary>
        /// Uri to legal terms and conditions
        /// </summary>
        [JsonProperty("legalTermsUri")]
        public string LegalTermsUri { get; set; } = string.Empty;

        /// <summary>
        /// Type of legal terms. Typically set to "None".
        /// </summary>
        [JsonProperty("legalTermsType")]
        public string? TypeOfLegalTerms { get; set; } = string.Empty;

        /// <summary>
        /// Uri to the privacy policy
        /// </summary>
        [JsonProperty("privacyPolicyUri")]
        public string? PrivacyPolicyUri { get; set; } = string.Empty;

        /// <summary>
        /// Uri the user should visit to get support for this item.
        /// </summary>
        [JsonProperty("supportUri")]
        public string? SupportUri { get; set; } = string.Empty;

        /// <summary>
        /// Absolute URI to a WEB page that provides detailed pricing information.
        /// </summary>
        [JsonProperty("pricingDetailsUri")]
        public string? PricingDetailsUri { get; set; }

        /// <summary>
        /// Popularity index. Can be used to sort the list.
        /// </summary>
        [JsonProperty("popularity")]
        public decimal Popularity { get; set; } = 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        public IndexItem() { }

    }
}
