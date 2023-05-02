using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.AppService.Environments;
using SujaySarma.Sdk.Azure.Common;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.Plans
{
    /// <summary>
    /// Properties of an app service plan
    /// </summary>
    public class AppServicePlanProperties
    {

        /// <summary>
        /// Date/time when the free offer (if the plan was created using some promotional campaign) 
        /// will expire
        /// </summary>
        [JsonProperty("freeOfferExpirationTime")]
        public DateTime? FreeOfferExpiresAt { get; set; } = null;

        /// <summary>
        /// Name of the geographic region (eg: "West US") where the plan is homed.
        /// </summary>
        [JsonProperty("geoRegion")]
        public string GeoRegionName { get; set; } = string.Empty;

        /// <summary>
        /// If the plan is homed in an app service environment, then the profile of the ASE
        /// </summary>
        [JsonProperty("hostingEnvironmentProfile")]
        public HostingEnvironmentProfile? AppServiceEnvironmentProfile { get; set; } = null;

        /// <summary>
        /// Reference to the hosting app service environment
        /// </summary>
        [JsonProperty("hostingEnvironment")]
        public AppServiceEnvironment? AppServiceEnvironment { get; set; } = null;

        /// <summary>
        /// True if plan is a HyperV container app service
        /// </summary>
        [JsonProperty("hyperV")]
        public bool IsHyperVContainerApp { get; set; } = false;

        /// <summary>
        /// If true, plan owns spot instances
        /// </summary>
        [JsonProperty("isSpot")]
        public bool IsSpotAppsEnabled { get; set; } = false;

        /// <summary>
        /// If elastic scaling has been applied to this ASP, then the maximum limit of workers allowed
        /// </summary>
        [JsonProperty("maximumElasticWorkerCount")]
        public int MaximumElasticWorkers { get; set; } = 0;

        /// <summary>
        /// The overall maximum number of workers that can be allocated for this ASP
        /// </summary>
        [JsonProperty("maximumNumberOfWorkers")]
        public int MaximumWorkersLimit { get; set; } = 0;

        /// <summary>
        /// Number of sites currently running under this ASP
        /// </summary>
        [JsonProperty("numberOfSites")]
        public int SitesCount { get; set; } = 0;

        /// <summary>
        /// If true, scaling options can be applied per site. Otherwise, scaling is applied 
        /// equally to all sites hosted under this ASP.
        /// </summary>
        [JsonProperty("perSiteScaling")]
        public bool ApplyScalingPerSite { get; set; } = true;

        /// <summary>
        /// Provisioning status
        /// </summary>
        [JsonProperty("provisioningState")]
        public ProvisioningStatusEnum ProvisioningState { get; set; } = ProvisioningStatusEnum.Default;

        /// <summary>
        /// True if this is a Linux app plan
        /// </summary>
        [JsonProperty("reserved")]
        public bool IsLinuxAppPlan { get; set; } = false;

        /// <summary>
        /// Name of the resource group this ASP is homed in
        /// </summary>
        [JsonProperty("resourceGroup")]
        public string ResourceGroupName { get; set; } = string.Empty;

        /// <summary>
        /// Date/time when the spot server farm expires -- NULL if this is not a spot farm
        /// </summary>
        [JsonProperty("spotExpirationTime")]
        public DateTime? SpotExpirationTime { get; set; } = null;

        /// <summary>
        /// Status of the plan
        /// </summary>
        [JsonProperty("status")]
        public AppServicePlanStatusEnum Status { get; set; } = AppServicePlanStatusEnum.Default;

        /// <summary>
        /// Subscription Guid
        /// </summary>
        [JsonProperty("subscription")]
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// For scaling-enabled ASPs, the count of scaling workers
        /// </summary>
        [JsonProperty("targetWorkerCount")]
        public int ScalingWorkersCount { get; set; } = 0;

        /// <summary>
        /// For scaling-enabled ASPs, the size Id of the scale-instance workers
        /// </summary>
        [JsonProperty("targetWorkerSizeId")]
        public int ScalingWorkerSizeId { get; set; } = 0;

        /// <summary>
        /// Target worker tier assigned to the ASP
        /// </summary>
        [JsonProperty("workerTierName")]
        public string? WorkerTierName { get; set; } = null;

        /// <summary>
        /// Id of the server farm (Numeric ID of the ASP)
        /// </summary>
        [JsonProperty("serverFarmId")]
        public int? ServerFarmId { get; set; } = null;

        /// <summary>
        /// Name of the plan
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Current maximum number of workers
        /// </summary>
        [JsonProperty("numberOfWorkers")]
        public int? NumberOfWorkers { get; set; } = null;

        /// <summary>
        /// Size of workers at current time (at current scale level)
        /// </summary>
        [JsonProperty("currentWorkerSize")]
        public string? CurrentWorkerSize { get; set; } = null;

        /// <summary>
        /// Size Id of the current worker (at current scale level)
        /// </summary>
        [JsonProperty("currentWorkerSizeId")]
        public int? CurrentWorkerSizeId { get; set; } = null;

        /// <summary>
        /// Number of current workers
        /// </summary>
        [JsonProperty("currentNumberOfWorkers")]
        public int? CurrentNumberOfWorkers { get; set; } = null;

        /// <summary>
        /// Name of the webspace
        /// </summary>
        [JsonProperty("webSpace")]
        public string? WebspaceName { get; set; } = null;

        /// <summary>
        /// Name of the administration (SCM) site
        /// </summary>
        [JsonProperty("adminSiteName")]
        public string? AdministrationSiteName { get; set; } = null;

        /// <summary>
        /// Name of the plan that has been provisioned
        /// </summary>
        [JsonProperty("planName")]
        public string? PlanName { get; set; } = null;

        /// <summary>
        /// Runtime site name of the administration site
        /// </summary>
        [JsonProperty("adminRuntimeSiteName")]
        public string? AdministrationRuntimeSiteName { get; set; } = null;

        /// <summary>
        /// Compute mode - whether shared or dedicated
        /// </summary>
        [JsonProperty("computeMode")]
        public string? ComputeMode { get; set; } = null;

        /// <summary>
        /// Site mode (???)
        /// </summary>
        [JsonProperty("siteMode")]
        public string? SiteMode { get; set; } = null;

        /// <summary>
        /// Id of the ASE
        /// </summary>
        [JsonProperty("hostingEnvironmentId")]
        public string? HostingEnvironmentId { get; set; } = null;

        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, object>? Tags { get; set; } = null;

        /// <summary>
        /// Kind of ASP - web or api
        /// </summary>
        [JsonProperty("kind")]
        public string? Kind { get; set; } = null;

        /// <summary>
        /// Id of the physical server hosting this VM
        /// </summary>
        [JsonProperty("mdmId")]
        public string? HostPhysicalServerId { get; set; } = null;

        /// <summary>
        /// Website Id (???)
        /// </summary>
        [JsonProperty("webSiteId")]
        public string? WebsiteId { get; set; } = null;

        /// <summary>
        /// Ids of existing server farms (???)
        /// </summary>
        [JsonProperty("existingServerFarmIds")]
        public string[]? ExistingServerFarmIds { get; set; } = null;


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServicePlanProperties() { }
    }
}
