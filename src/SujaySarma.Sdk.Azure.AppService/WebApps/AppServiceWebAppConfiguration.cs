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
        /// Name of the slot to swap to
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
        [JsonProperty("ftpsState"), JsonConverter(typeof(StringEnumConverter))]
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
        /// (If Java is enabled) Version number of Java
        /// </summary>
        [JsonProperty("javaVersion")]
        public string? JavaVersion { get; set; } = null;

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
        [JsonProperty("loadBalancing"), JsonConverter(typeof(StringEnumConverter))]
        public AppServiceWebAppSiteLoadBalancingType? LoadBalancing { get; set; } = null;

        /// <summary>
        /// True to enable local instance of MySql Server service
        /// </summary>
        [JsonProperty("localMySqlEnabled")]
        public bool IsLocalMySqlServiceEnabled { get; set; } = false;

        /// <summary>
        /// Size limit for the "/logs" directory, in GB
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
        [JsonProperty("managedPipelineMode"), JsonConverter(typeof(StringEnumConverter))]
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
        [JsonProperty("scmType"), JsonConverter(typeof(StringEnumConverter))]
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
        /// Additional metadata
        /// </summary>
        [JsonProperty("metadata")]
        public List<AzureNameValuePair>? Metadata { get; set; } = null;


        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceWebAppConfiguration() { }

        /// <summary>
        /// Sets application to be always On
        /// </summary>
        public AppServiceWebAppConfiguration WithAlwaysOn()
        {
            IsAlwaysOn = true;
            return this;
        }

        public AppServiceWebAppConfiguration WithAutoHeal()
        {
            IsAutoHealEnabled = true;
            return this;
        }

        /// <summary>
        /// Sets the auto-swap slot name
        /// </summary>
        public AppServiceWebAppConfiguration WithAutoSwap(string slotName)
        {
            AutoSwapSlotName = slotName;
            return this;
        }

        /// <summary>
        /// Sets the launch command line string
        /// </summary>
        public AppServiceWebAppConfiguration WithCommandLine(string commandLineString)
        {
            LaunchCommandLine = commandLineString;
            return this;
        }

        /// <summary>
        /// Sets CORS policy
        /// </summary>
        public AppServiceWebAppConfiguration WithCors(CorsPolicy policy)
        {
            CorsSettings = policy;
            return this;
        }

        /// <summary>
        /// Enables detailed logging
        /// </summary>
        public AppServiceWebAppConfiguration WithDetailedLogging()
        {
            IsDetailedErrorLoggingEnabled = true;
            return this;
        }

        /// <summary>
        /// Set the document root (wwwroot) for the application. Set carefully!
        /// </summary>
        public AppServiceWebAppConfiguration WithWebRoot(string path)
        {
            WebRootPath = path;
            return this;
        }

        /// <summary>
        /// Set FTP state
        /// </summary>
        public AppServiceWebAppConfiguration WithFtp(AppServiceFtpServiceStateEnum state)
        {
            FtpServiceState = state;
            return this;
        }

        /// <summary>
        /// Set HTTP 2.0
        /// </summary>
        public AppServiceWebAppConfiguration WithHttp20()
        {
            IsHttp20Enabled = true;
            return this;
        }

        /// <summary>
        /// Set Httpd Logging ON
        /// </summary>
        public AppServiceWebAppConfiguration WithWebServerLogging()
        {
            IsWebServerLoggingEnabled = true;
            return this;
        }

        /// <summary>
        /// Enable local MySQL instance
        /// </summary>
        public AppServiceWebAppConfiguration WithLocalMySql()
        {
            IsLocalMySqlServiceEnabled = true;
            return this;
        }

        /// <summary>
        /// Set load balancing type
        /// </summary>
        public AppServiceWebAppConfiguration WithLoadBalancing(AppServiceWebAppSiteLoadBalancingType type)
        {
            LoadBalancing = type;
            return this;
        }

        /// <summary>
        /// Set log directory size limit (in GB)
        /// </summary>
        public AppServiceWebAppConfiguration WithLogLimit(int limit)
        {
            SizeLimitForLogsDirectory = limit;
            return this;
        }

        /// <summary>
        /// Set the pipeline mode (classic/integrated)
        /// </summary>
        public AppServiceWebAppConfiguration WithPipeline(AppServiceWebAppManagedPipelineMode mode)
        {
            PipelineMode = mode;
            return this;
        }

        /// <summary>
        /// Set minimum version of Tls allowed
        /// </summary>
        public AppServiceWebAppConfiguration WithTls(TlsVersionEnum tlsVersion)
        {
            MinimumTlsVersion = tlsVersion switch
            {
                TlsVersionEnum.V1_2 => "1.2",
                TlsVersionEnum.v1_1 => "1.1",
                _ => "1.0"
            };

            return this;
        }

        /// <summary>
        /// Turn ON remote debugging and set the engine version
        /// </summary>
        public AppServiceWebAppConfiguration WithRemoteDebugging(AppServiceDebugEngineVersionEnum engineVersion)
        {
            IsRemoteDebuggingEnabled = true;
            RemoteDebuggingEngineVersion = engineVersion switch
            {
                AppServiceDebugEngineVersionEnum.VS2019 => "2019",
                AppServiceDebugEngineVersionEnum.VS2018 => "2018",
                _ => "2017"
            };
            return this;
        }

        /// <summary>
        /// Set request tracing ON with the auto turnoff time
        /// </summary>
        public AppServiceWebAppConfiguration WithRequestTracing(DateTime turnOffAt)
        {
            IsRequestTracingEnabled = true;
            RequestTracingExpiryAt = turnOffAt;

            return this;
        }

        /// <summary>
        /// Set type of source control
        /// </summary>
        public AppServiceWebAppConfiguration WithSourceControl(AppServiceWebAppSourceControlType type)
        {
            SourceControlType = type;
            return this;
        }

        /// <summary>
        /// Set 32-bit mode -- by default we provision 64-bit apps!
        /// </summary>
        public AppServiceWebAppConfiguration With32Bit()
        {
            Is32BitApp = true;
            return this;
        }


        /// <summary>
        /// Turn ON websockets
        /// </summary>
        public AppServiceWebAppConfiguration WithWebSockets()
        {
            IsWebSocketsEnabled = true;
            return this;
        }

        /// <summary>
        /// Add an autoheal rule
        /// </summary>
        /// <param name="rule">Rule to add</param>
        public void AddAutoHealRule(AppServiceWebAppAutoHealRule rule)
        {
            IsAutoHealEnabled = true;
            if (AutoHealRules == null)
            {
                AutoHealRules = new List<AppServiceWebAppAutoHealRule>();
            }

            AutoHealRules.Add(rule);
        }

        /// <summary>
        /// Add an app setting
        /// </summary>
        /// <param name="key">Key/name of setting</param>
        /// <param name="value">Value of setting</param>
        public void AddAppSetting(string key, string value)
        {
            if (AppSettings == null)
            {
                AppSettings = new List<AzureNameValuePair>();
            }

            AppSettings.Add(new AzureNameValuePair(key, value));
        }

        /// <summary>
        /// Get app setting value
        /// </summary>
        /// <param name="key">Key/name of setting</param>
        /// <returns>value if found, or NULL</returns>
        public string? GetAppSetting(string key)
        {
            if (AppSettings != null)
            {
                foreach (AzureNameValuePair pair in AppSettings)
                {
                    if (pair.Name == key)
                    {
                        return pair.Value;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Convert app settings structure into a dictionary
        /// </summary>
        /// <returns>Dictionary equivalent of app settings</returns>
        public IDictionary<string, string> GetAppSettings() => AzureNameValuePair.ToDictionary(AppSettings);

        /// <summary>
        /// Add a connection string
        /// </summary>
        /// <param name="type">Type of connection string</param>
        /// <param name="key">Name of key</param>
        /// <param name="value">Connection string</param>
        public void AddConnectionString(AppServiceWebAppConnectionStringType type, string key, string value)
        {
            if (ConnectionStrings == null)
            {
                ConnectionStrings = new List<AppServiceWebAppConnectionString>();
            }

            ConnectionStrings.Add(
                    new AppServiceWebAppConnectionString()
                    {
                        Type = type,
                        Name = key,
                        Value = value
                    }
                ); ;
        }

        /// <summary>
        /// Add default documents
        /// </summary>
        /// <param name="documentNames">Collection of default document names</param>
        /// <returns>AppServiceWebAppConfiguration</returns>
        public AppServiceWebAppConfiguration UseDefaultDocument(params string[] documentNames)
        {
            if (DefaultDocuments == null)
            {
                DefaultDocuments = new List<string>();
            }

            foreach (string doc in documentNames)
            {
                if (!DefaultDocuments.Contains(doc))
                {
                    DefaultDocuments.Add(doc);
                }
            }

            return this;
        }


        /// <summary>
        /// Configure the app for .NET Framework
        /// </summary>
        /// <param name="version">.NET Framework version ("v3.5" or "v4.0")</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UseDotNetFramework(string version)
        {
            if (Metadata == null)
            {
                Metadata = new List<AzureNameValuePair>();
            }

            DotNetFrameworkVersion = version;
            AzureNameValuePair.AddOrSet(Metadata, "CURRENT_STACK", "dotnet");
            return this;
        }

        /// <summary>
        /// Configure the app for .NET Core on Windows OS
        /// </summary>
        public AppServiceWebAppConfiguration UseDotNetCoreWithWindows()
        {
            if (Metadata == null)
            {
                Metadata = new List<AzureNameValuePair>();
            }

            AzureNameValuePair.AddOrSet(Metadata, "CURRENT_STACK", "dotnetcore");
            return this;
        }

        /// <summary>
        /// Configure the app for .NET Core on Linux OS
        /// </summary>
        /// <param name="versionNumber">Only the version number (eg: "3.0")</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UseDotNetCoreWithLinux(string versionNumber)
        {
            LinuxAppFrameworkVersion = $"DOTNETCORE|{versionNumber}";
            return this;
        }

        /// <summary>
        /// Configure the app for Java with Linux OS
        /// </summary>
        /// <param name="version">Java version string. Looks like "JAVA|11-java-11"</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UseJavaWithLinux(string version)
        {
            LinuxAppFrameworkVersion = version;
            return this;
        }

        /// <summary>
        /// Configure the app for Java with Windows OS
        /// </summary>
        /// <param name="containerName">Name of the Java container (eg: Java, Tomcat, Wildfly)</param>
        /// <param name="containerVersion">Version number of the Java container</param>
        /// <param name="javaVersion">Version number of Java</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UseJavaWithWindows(string containerName, string containerVersion, string javaVersion)
        {
            JavaContainerName = containerName;
            JavaContainerVersion = containerVersion;
            JavaVersion = javaVersion;

            if (Metadata == null)
            {
                Metadata = new List<AzureNameValuePair>();
            }

            AzureNameValuePair.AddOrSet(Metadata, "CURRENT_STACK", "java");
            return this;
        }

        /// <summary>
        /// Configure the app for Node.Js with Windows OS
        /// </summary>
        /// <param name="version">Node.js version number - only the version number portion</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UseNodeJsWithWindows(string version)
        {
            if (Metadata == null)
            {
                Metadata = new List<AzureNameValuePair>();
            }

            AzureNameValuePair.AddOrSet(Metadata, "CURRENT_STACK", "node");
            AddAppSetting("WEBSITE_NODE_DEFAULT_VERSION", version);
            NodeJsVersion = version;

            return this;
        }

        /// <summary>
        /// Configure the app for Node.Js with Linux OS
        /// </summary>
        /// <param name="version">Node.js version number - only the version number portion</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UseNodeJsWithLinux(string version)
        {
            LinuxAppFrameworkVersion = $"NODE|{version}";

            return this;
        }

        /// <summary>
        /// Configure the app for PHP with Windows OS
        /// </summary>
        /// <param name="version">PHP version number - only the version number portion</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UsePHPWithWindows(string version)
        {
            if (Metadata == null)
            {
                Metadata = new List<AzureNameValuePair>();
            }

            AzureNameValuePair.AddOrSet(Metadata, "CURRENT_STACK", "php");
            PhpVersion = version;

            return this;
        }

        /// <summary>
        /// Configure the app for PHP with Linux OS
        /// </summary>
        /// <param name="version">PHP version number - only the version number portion</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UsePHPWithLinux(string version)
        {
            LinuxAppFrameworkVersion = $"PHP|{version}";

            return this;
        }

        /// <summary>
        /// Configure the app for Python with Windows OS
        /// </summary>
        /// <param name="version">Python version number - only the version number portion</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UsePythonWithWindows(string version)
        {
            if (Metadata == null)
            {
                Metadata = new List<AzureNameValuePair>();
            }

            AzureNameValuePair.AddOrSet(Metadata, "CURRENT_STACK", "python");
            PythonVersion = version;

            return this;
        }

        /// <summary>
        /// Configure the app for Python with Linux OS
        /// </summary>
        /// <param name="version">Python version number - only the version number portion</param>
        /// <remarks>
        ///     Version number is not validated!
        /// </remarks>
        public AppServiceWebAppConfiguration UsePythonWithLinux(string version)
        {
            LinuxAppFrameworkVersion = $"PYTHON|{version}";

            return this;
        }

        /// <summary>
        /// Configure the app for Ruby with Linux OS
        /// </summary>
        /// <param name="version">Ruby version number - only the version number portion</param>
        /// <remarks>
        ///     Version number is not validated! 
        ///     As per the Portal UX, you cannot run Ruby on Windows yet.
        /// </remarks>
        public AppServiceWebAppConfiguration UseRubyWithLinux(string version)
        {
            LinuxAppFrameworkVersion = $"RUBY|{version}";

            return this;
        }


    }
}
