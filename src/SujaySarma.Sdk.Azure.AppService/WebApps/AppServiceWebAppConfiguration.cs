using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Core.Cors;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Configuration information for the web app (stuff on the "Configuration" tab of the 
    /// web app's UX on the Azure Portal)
    /// </summary>
    public class AppServiceWebAppConfiguration
    {
        /// <summary>
        /// Set true to leave the app always ON. Otherwise, Azure will shut down the app during periods of disuse 
        /// and save cost.
        /// </summary>
        [JsonProperty("alwaysOn")]
        public bool IsAlwaysOn { get; set; } = false;

        /// <summary>
        /// (If configured as an API app) Formal API definition for the app
        /// </summary>
        [JsonProperty("apiDefinition")]
        public AppServiceWebAppApiDefinition? ApiDefinition { get; set; } = null;

        /// <summary>
        /// Azure API Management service settings linked to this app
        /// </summary>
        [JsonProperty("apiManagementConfig")]
        public AzureApiManagementConfiguration? ApiManagementSettings { get; set; } = null;

        /// <summary>
        /// Command-line string to run to boot up the app. (Eg: "dotnet run foo")
        /// </summary>
        [JsonProperty("appCommandLine")]
        public string LaunchCommandLine { get; set; } = string.Empty;

        /// <summary>
        /// Application settings
        /// </summary>
        [JsonProperty("appSettings")]
        public List<AzureNameValuePair> AppSettings { get; set; } = new List<AzureNameValuePair>();

        /// <summary>
        /// True if auto-healing is enabled
        /// </summary>
        [JsonProperty("autoHealEnabled")]
        public bool IsAutoHealEnabled { get; set; } = false;

        /// <summary>
        /// If auto-heal is enabled, then the rules for auto-healing
        /// </summary>
        [JsonProperty("autoHealRules")]
        public List<AppServiceWebAppAutoHealRule>? AutoHealRules { get; set; } = null;

        /// <summary>
        /// Name of the slot to swap to during auto healing
        /// </summary>
        [JsonProperty("autoSwapSlotName")]
        public string? AutoSwapSlotName { get; set; } = null;

        /// <summary>
        /// Connection strings
        /// </summary>
        [JsonProperty("connectionStrings")]
        public List<AppServiceWebAppConnectionString> ConnectionStrings { get; set; } = new List<AppServiceWebAppConnectionString>();

        /// <summary>
        /// Cors policy
        /// </summary>
        [JsonProperty("cors")]
        public CorsPolicy? CorsSettings { get; set; } = null;

        /// <summary>
        /// List of default documents configured for this app
        /// </summary>
        [JsonProperty("defaultDocuments")]
        public List<string> DefaultDocuments { get; set; } = new List<string>();

        /// <summary>
        /// Set to enable detailed error logging (for troubleshooting)
        /// </summary>
        [JsonProperty("detailedErrorLoggingEnabled")]
        public bool IsDetailedErrorLoggingEnabled { get; set; } = false;

        /// <summary>
        /// The path to the web root (where the website files are stored)
        /// </summary>
        [JsonProperty("documentRoot")]
        public string WebRootPath { get; set; } = string.Empty;

        /// <summary>
        /// Settings for polymorphic apps (workaround settings -- as per Azure API docs)
        /// </summary>
        [JsonProperty("experiments")]
        public AppServiceWebAppExperiment? PolymorphicAppsExperiments { get; set; } = null;

        /// <summary>
        /// Status of the FTP Service
        /// </summary>
        [JsonProperty("ftpsState", ItemConverterType = typeof(StringEnumConverter))]
        public AppServiceFtpServiceStateEnum FtpServiceState { get; set; } = AppServiceFtpServiceStateEnum.Default;

        /// <summary>
        /// HTTP Handler mappings
        /// </summary>
        [JsonProperty("handlerMappings")]
        public List<AppServiceWebAppHandlerMapping> HandlerMappings { get; set; } = new List<AppServiceWebAppHandlerMapping>();

        /// <summary>
        /// Path to a script (???) that checks for the app's health
        /// </summary>
        [JsonProperty("healthCheckPath")]
        public string? HealthCheckURI { get; set; } = null;

        /// <summary>
        /// If HTTP 2.0 is enabled
        /// </summary>
        [JsonProperty("http20Enabled")]
        public bool IsHttp20Enabled { get; set; } = false;

        /// <summary>
        /// If web server (HTTP) logging is enabled
        /// </summary>
        [JsonProperty("httpLoggingEnabled")]
        public bool IsWebServerLoggingEnabled { get; set; } = false;

        /// <summary>
        /// IP security restrictions for the main app
        /// </summary>
        [JsonProperty("ipSecurityRestrictions")]
        public List<AppServiceWebAppIPSecurityRestriction> IpSecurityRestrictions { get; set; } = new List<AppServiceWebAppIPSecurityRestriction>();

        /// <summary>
        /// (If Java is enabled) Name/path to the Java container
        /// </summary>
        [JsonProperty("javaContainer")]
        public string? JavaContainerName { get; set; } = null;

        /// <summary>
        /// (If Java is enabled) Version of the Java container
        /// </summary>
        [JsonProperty("javaContainerVersion")]
        public string? JavaContainerVersion { get; set; } = null;

        /// <summary>
        /// Site limits
        /// </summary>
        [JsonProperty("limits")]
        public AppServiceWebAppSiteLimits? SiteLimits { get; set; } = null;

        /// <summary>
        /// (For Linux Apps) Linux App Framework and version
        /// </summary>
        [JsonProperty("linuxFxVersion")]
        public string? LinuxAppFrameworkVersion { get; set; } = null;

        /// <summary>
        /// Load balancing configuration
        /// </summary>
        [JsonProperty("loadBalancing", ItemConverterType = typeof(StringEnumConverter))]
        public AppServiceWebAppSiteLoadBalancingType? LoadBalancing { get; set; } = null;

        /// <summary>
        /// True to enable local instance of MySql Server service
        /// </summary>
        [JsonProperty("localMySqlEnabled")]
        public bool IsLocalMySqlServiceEnabled { get; set; } = false;

        /// <summary>
        /// Size limit for the "/logs" directory
        /// </summary>
        [JsonProperty("logsDirectorySizeLimit")]
        public int? SizeLimitForLogsDirectory { get; set; } = 0;

        /// <summary>
        /// The site's Machine Key
        /// </summary>
        [JsonProperty("machineKey")]
        public AppServiceWebAppSiteMachineKey? MachineKey { get; set; } = null;

        /// <summary>
        /// HTTP pipeline mode
        /// </summary>
        [JsonProperty("managedPipelineMode", ItemConverterType = typeof(StringEnumConverter))]
        public AppServiceWebAppManagedPipelineMode PipelineMode { get; set; } = AppServiceWebAppManagedPipelineMode.Integrated;

        /// <summary>
        /// Id of the managed services identity for this app
        /// </summary>
        [JsonProperty("managedServiceIdentityId")]
        public int ManagedServiceIdentityId { get; set; } = 0;

        /// <summary>
        /// Minimum version of TLS allowed for SSL requests. Allowed values are: "1.0", "1.1" and "1.2".
        /// </summary>
        [JsonProperty("minTlsVersion")]
        public string MinimumTlsVersion { get; set; } = "1.2";

        /// <summary>
        /// Version of .NET Framework to enable for the app
        /// </summary>
        [JsonProperty("netFrameworkVersion")]
        public string? DotNetFrameworkVersion { get; set; }

        /// <summary>
        /// Version of Node.js to enable for the app
        /// </summary>
        [JsonProperty("nodeVersion")]
        public string? NodeJsVersion { get; set; }

        /// <summary>
        /// Number of worker processes for this site
        /// </summary>
        [JsonProperty("numberOfWorkers")]
        public int NumberOfWorkerProcesses { get; set; } = 1;

        /// <summary>
        /// Version of PHP to enable for this app
        /// </summary>
        [JsonProperty("phpVersion")]
        public string? PhpVersion { get; set; }

        /// <summary>
        /// (Only Consumption/Elastic plans) Number of pre-warmed instances
        /// </summary>
        [JsonProperty("preWarmedInstanceCount")]
        public int? NumberOfPreWarmedInstances { get; set; }

        /// <summary>
        /// Username for publishing to the app
        /// </summary>
        [JsonProperty("publishingUsername")]
        public string PublishUserName { get; set; } = string.Empty;

        /// <summary>
        /// Settings for push notifications
        /// </summary>
        [JsonProperty("push")]
        public AppServiceWebAppPushNotificationsSettings? PushNotificationsSettings { get; set; }

        /// <summary>
        /// Version of Python framework to enable for this app
        /// </summary>
        [JsonProperty("pythonVersion")]
        public string? PythonVersion { get; set; }

        /// <summary>
        /// If remote debugging is enabled
        /// </summary>
        [JsonProperty("remoteDebuggingEnabled")]
        public bool IsRemoteDebuggingEnabled { get; set; } = false;

        /// <summary>
        /// If remote debugging is enabled, version of remote debugging, maps to versions of Visual Studio: 
        /// allowed values being: "2017", "2018" or "2019"
        /// </summary>
        [JsonProperty("remoteDebuggingVersion")]
        public string? RemoteDebuggingEngineVersion { get; set; }

        /// <summary>
        /// If request tracing is currently enabled - 
        /// WARNING: Enable only for duration of a troubleshooting session.
        /// </summary>
        [JsonProperty("requestTracingEnabled")]
        public bool IsRequestTracingEnabled { get; set; } = false;

        /// <summary>
        /// Date/time when request tracing on this app will be disabled again
        /// </summary>
        [JsonProperty("requestTracingExpirationTime")]
        public DateTime? RequestTracingExpiryAt { get; set; }

        /// <summary>
        /// IP security restrictions for the SCM/Kudu site
        /// </summary>
        [JsonProperty("scmIpSecurityRestrictions")]
        public List<AppServiceWebAppIPSecurityRestriction>? IpSecurityRestrictionsForScmSite { get; set; }

        /// <summary>
        /// If the IP security restrictions are to allow/deny access to the app site from the Scm site
        /// </summary>
        [JsonProperty("scmIpSecurityRestrictionsUseMain")]
        public bool IsUseIpSecurityRestrictionsToAccessMainSite { get; set; }

        /// <summary>
        /// Type of source control being used. Will be NULL if not used.
        /// </summary>
        [JsonProperty("scmType", ItemConverterType = typeof(StringEnumConverter))]
        public AppServiceWebAppSourceControlType? SourceControlType { get; set; }

        /// <summary>
        /// Command line options for tracing
        /// </summary>
        [JsonProperty("tracingOptions")]
        public string? TracingOptions { get; set; } = null;

        /// <summary>
        /// Set to true to use 32-bit app processes, otherwise runs at 64-bit.
        /// </summary>
        [JsonProperty("use32BitWorkerProcess")]
        public bool Is32BitApp { get; set; } = false;

        /// <summary>
        /// List of virtual applications running off this site
        /// </summary>
        [JsonProperty("virtualApplications")]
        public List<AppServiceWebAppVirtualApplication> VirtualApplications { get; set; } = new List<AppServiceWebAppVirtualApplication>();

        /// <summary>
        /// Name of the virtual network this app service is attached to. NULL if not on a VNet
        /// </summary>
        [JsonProperty("vnetName")]
        public string? VirtualNetworkName { get; set; }

        /// <summary>
        /// True if web sockets (websock) is enabled
        /// </summary>
        [JsonProperty("webSocketsEnabled")]
        public bool IsWebSocketsEnabled { get; set; } = false;

        /// <summary>
        /// Xenon app framework and version
        /// </summary>
        [JsonProperty("windowsFxVersion")]
        public string? XenonAppFrameworkVersion { get; set; }

        /// <summary>
        /// Explicit Azure Managed Services Identity Id
        /// </summary>
        [JsonProperty("xManagedServiceIdentityId")]
        public int? ExplicitManagedServicesIdentityId { get; set; } = 0;


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceWebAppConfiguration() { }
    }
}
