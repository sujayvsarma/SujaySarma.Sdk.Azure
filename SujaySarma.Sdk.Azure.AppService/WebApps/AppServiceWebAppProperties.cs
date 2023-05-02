using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.AppService.Plans;
using SujaySarma.Sdk.Azure.Common;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Properties of a web app
    /// </summary>
    public class AppServiceWebAppProperties
    {

        /// <summary>
        /// Management information availability state for the app
        /// </summary>
        [JsonProperty("availabilityState"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceWebAppAvailabilityStateEnum AvailabilityState { get; set; } = AppServiceWebAppAvailabilityStateEnum.Default;

        /// <summary>
        /// State of ARR Affinity of the web app (sets session affinity cookies that ensure routing requests to the 
        /// same instance of the web app)
        /// </summary>
        [JsonProperty("clientAffinityEnabled")]
        public bool IsClientAffinityEnabled { get; set; } = true;

        /// <summary>
        /// Enable or disable TLS mutual authentication by requiring client-sent certificates
        /// </summary>
        [JsonProperty("clientCertEnabled")]
        public bool IsClientCertificateAuthenticationEnabled { get; set; } = false;

        /// <summary>
        /// (Only if client certificate auth is enabled) Comma-seperated exclusion paths for client certificates
        /// </summary>
        [JsonProperty("clientCertExclusionPaths")]
        public string? ClientCertificateExclusionPaths { get; set; } = null;

        /// <summary>
        /// During app creation if it was cloned from another app, this structure would contain information about the 
        /// source of the app clone. Otherwise, NULL.
        /// </summary>
        [JsonProperty("cloningInfo")]
        public AppServiceWebAppCloningInformation? CloningInformation { get; set; } = null;

        /// <summary>
        /// (Only if app uses container-deployments) Size of the app container
        /// </summary>
        [JsonProperty("containerSize")]
        public int? ContainerSize { get; set; } = null;

        /// <summary>
        /// (For dynamic apps only) Limit of daily memory time allowed
        /// </summary>
        [JsonProperty("dailyMemoryTimeQuota")]
        public int? DailyMemoryTimeQuota { get; set; } = null;

        /// <summary>
        /// (Readonly) The default host name of the app
        /// </summary>
        [JsonProperty("defaultHostName")]
        public string DefaultHostName { get; private set; } = string.Empty;

        /// <summary>
        /// If app is enabled or disabled. When FALSE, app is offline. Flag can be used to take 
        /// app offline in an update operation.
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// List of hostnames that are enabled for this app. If hostname is not present in this collection, then 
        /// requests to that hostname (though added to the HostNames property) will not be served.
        /// </summary>
        [JsonProperty("enabledHostNames")]
        public List<string> EnabledHostNames { get; set; } = new List<string>();

        /// <summary>
        /// SSL states for all added hostnames
        /// </summary>
        [JsonProperty("hostNameSslStates")]
        public List<AppServiceHostNameSslStates> HostNameSslStates { get; set; } = new List<AppServiceHostNameSslStates>();

        /// <summary>
        /// List of all hostnames associated to the app. But, unless a hostname is added to the EnabledHostNames collection as well, 
        /// requests to that hostname will not be served.
        /// </summary>
        [JsonProperty("hostNames")]
        public List<string> HostNames { get; set; } = new List<string>();

        /// <summary>
        /// WARNING! If true, then all public hostnames are disabled -- app will be accessible only via API Management process.
        /// </summary>
        [JsonProperty("hostNamesDisabled")]
        public bool IsPublicHostNamesDisabled { get; set; } = false;

        /// <summary>
        /// App service environment to use for this app. NULL if we are not using ASE for this app.
        /// </summary>
        [JsonProperty("hostingEnvironmentProfile")]
        public HostingEnvironmentProfile? EnvironmentProfile { get; set; } = null;

        /// <summary>
        /// When true, allows connections only over HTTPS (SSL). That is, no HTTP (non-SSL) connections.
        /// </summary>
        [JsonProperty("httpsOnly")]
        public bool IsHttpsOnly { get; set; } = true;

        /// <summary>
        /// If an operation on this app is in progress, contains the Id to that operation. Otherwise, NULL
        /// </summary>
        [JsonProperty("inProgressOperationId")]
        public string? InProgressOperationId { get; set; } = null;

        /// <summary>
        /// When app is enabled for container deployments, and this is a default container, will be TRUE. 
        /// NULL when app is not enabled for container deployments.
        /// </summary>
        [JsonProperty("isDefaultContainer")]
        public bool? IsDefaultContainer { get; set; } = null;

        /// <summary>
        /// (Readonly) Date/time when the app was last modified in some way.
        /// </summary>
        [JsonProperty("lastModifiedTimeUtc")]
        public DateTime LastModifiedTimeUTC { get; private set; }

        /// <summary>
        /// (Only for functions container) Maximum number of workers
        /// </summary>
        [JsonProperty("maxNumberOfWorkers")]
        public int? MaximumNumberOfWorkers { get; set; } = null;

        /// <summary>
        /// (Readonly) List of IP Addresses bound to the app for outbound connections
        /// </summary>
        [JsonProperty("outboundIpAddresses")]
        public List<string> OutboundIpAddresses { get; private set; } = new List<string>();

        /// <summary>
        /// (Readonly) List of all possible IP addresses that the app can use for outbound connections. This includes 
        /// all the IP addresses from all Azure data centers worldwide.
        /// </summary>
        [JsonProperty("possibleOutboundIpAddresses")]
        public List<string> AllPossibleOutboundIpAddresses { get; private set; } = new List<string>();

        /// <summary>
        /// Redundancy mode of the App service site
        /// </summary>
        [JsonProperty("redundancyMode"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceRedundancyModeEnum RedundancyMode { get; set; }

        /// <summary>
        /// Name of the SCM website for this app (the "[webapp].scm.azurewebsites.net" site)
        /// </summary>
        [JsonProperty("repositorySiteName")]
        public string? ScmSiteName { get; set; } = null;

        /// <summary>
        /// True if this is a reserved instance of the app
        /// </summary>
        [JsonProperty("reserved")]
        public bool IsReserved { get; set; } = false;

        /// <summary>
        /// Name of the resource group this app is hosted in
        /// </summary>
        [JsonProperty("resourceGroup")]
        public string ResourceGroupName { get; set; } = string.Empty;

        /// <summary>
        /// True to stop the Kudu/SCM site ("[webapp].scm.azurewebsites.net") along with the app
        /// </summary>
        [JsonProperty("scmSiteAlsoStopped")]
        public bool IsScmSiteToBeStoppedWithApp { get; set; } = false;

        /// <summary>
        /// Resource Uri to the service plan used for this app
        /// </summary>
        [JsonProperty("serverFarmId")]
        public string PlanResourceId { get; set; } = string.Empty;

        /// <summary>
        /// Configuration information for the web app (stuff on the "Configuration" tab of the 
        /// web app's UX on the Azure Portal)
        /// </summary>
        [JsonProperty("siteConfig")]
        public AppServiceWebAppConfiguration WebAppConfiguration { get; set; } = new AppServiceWebAppConfiguration();

        /// <summary>
        /// Status of the last slot-swap operation
        /// </summary>
        [JsonProperty("slotSwapStatus")]
        public AppServiceWebAppSlotSwapStatus? LastSlotSwapStatus { get; set; } = null;

        /// <summary>
        /// Current status of the app
        /// </summary>
        [JsonProperty("state")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// When app exceeds memory-time quota limits, the app is suspended. This property provides the 
        /// target time when the suspension expires and the app is resumed
        /// </summary>
        [JsonProperty("suspendedTill")]
        public DateTime? MemoryQuotaExceededSuspensionExpiryAt { get; set; } = null;

        /// <summary>
        /// (Readonly) Name of the slot that deployments are set to slot into.
        /// </summary>
        [JsonProperty("targetSwapSlot")]
        public string? CurrentSwapTargetSlotName { get; private set; } = null;

        /// <summary>
        /// (Readonly) Hostnames registered with Azure Traffic Manager
        /// </summary>
        [JsonProperty("trafficManagerHostNames")]
        public List<string>? TrafficManagerHostNames { get; private set; } = null;

        /// <summary>
        /// Status of usage versus usage quotas
        /// </summary>
        [JsonProperty("usageState"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceWebAppUsageStatusEnum UsageStatus { get; set; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceWebAppProperties() { }


        /// <summary>
        /// Use the app service plan. Note that this cannot be changed after the app 
        /// has been created
        /// </summary>
        public AppServiceWebAppProperties WithAppServicePlan(ResourceUri appServicePlan)
        {
            if (!string.IsNullOrWhiteSpace(PlanResourceId))
            {
                throw new InvalidOperationException($"App is already bound to the app service plan '{PlanResourceId}'.");
            }

            if ((!appServicePlan.IsValid) || (!appServicePlan.Is(ResourceUriCompareLevel.Provider, "Microsoft.Web")) || (!appServicePlan.Is(ResourceUriCompareLevel.Type, "serverfarms")))
            {
                throw new ArgumentNullException();
            }

            PlanResourceId = appServicePlan.ToString();
            return this;
        }

        /// <summary>
        /// Set client (ARR) affinity
        /// </summary>
        public AppServiceWebAppProperties WithClientAffinity()
        {
            IsClientAffinityEnabled = true;
            return this;
        }

        /// <summary>
        /// Enable client-certificate authentication
        /// </summary>
        /// <param name="excludedCertificatePaths">Comma-seperated list of exclusion paths for client certificates</param>
        public AppServiceWebAppProperties WithClientCertificateAuthentication(string? excludedCertificatePaths = null)
        {
            IsClientAffinityEnabled = true;
            ClientCertificateExclusionPaths = excludedCertificatePaths;
            return this;
        }

        /// <summary>
        /// Set hostnames for the app
        /// </summary>
        public AppServiceWebAppProperties WithHostNames(string defaultHostName, params string[] otherHostNames)
        {
            DefaultHostName = defaultHostName;
            HostNames = new List<string>();
            EnabledHostNames = new List<string>();

            foreach (string name in otherHostNames)
            {
                HostNames.Add(name);
                EnabledHostNames.Add(name);
            }

            return this;
        }

        /// <summary>
        /// Add or update an ssl certificate
        /// </summary>
        public AppServiceWebAppProperties WithSslCertificate(string hostName, string certificateThumbprint)
        {
            if (HostNameSslStates == null)
            {
                HostNameSslStates = new List<AppServiceHostNameSslStates>();
            }

            bool updatedCertificate = false;
            foreach (AppServiceHostNameSslStates ssl in HostNameSslStates)
            {
                if (ssl.HostName.Equals(hostName, StringComparison.InvariantCultureIgnoreCase))
                {
                    ssl.SslCertificateThumbprint = certificateThumbprint;
                    ssl.MustUpdateConfiguration = true;
                    updatedCertificate = true;
                    break;
                }
            }

            if (!updatedCertificate)
            {
                HostNameSslStates.Add(
                        new AppServiceHostNameSslStates()
                        {
                            HostName = hostName,
                            HostType = AppServiceWebAppHostTypeEnum.Standard,
                            SslCertificateThumbprint = certificateThumbprint,
                            State = AppServiceWebAppSslTypeEnum.SniEnabled,
                            IpBasedSslVirtualIpAddress = null,
                            MustUpdateConfiguration = true
                        }
                    );
            }

            return this;
        }

        /// <summary>
        /// Enable Https-only mode
        /// </summary>
        public AppServiceWebAppProperties WithHttpsOnly()
        {
            IsHttpsOnly = true;
            return this;
        }

        /// <summary>
        /// Sets the web app configuration settings
        /// </summary>
        public AppServiceWebAppProperties WithConfiguration(AppServiceWebAppConfiguration configuration)
        {
            WebAppConfiguration = configuration;
            return this;
        }


    }
}
