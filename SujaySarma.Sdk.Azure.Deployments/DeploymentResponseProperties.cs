using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Properties of a deployment response
    /// </summary>
    public class DeploymentResponseProperties
    {

        /// <summary>
        /// Template hash
        /// </summary>
        [JsonProperty("templateHash")]
        public string? TemplateHash { get; set; }

        /// <summary>
        /// State of provisioning
        /// </summary>
        [JsonProperty("provisioningState")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Deployment correlation Id
        /// </summary>
        [JsonProperty("correlationId")]
        public string CorrelationId { get; set; } = string.Empty;

        /// <summary>
        /// Duration the deployment took (as a time string)
        /// </summary>
        [JsonProperty("duration")]
        public string Duration { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp of deployment run
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Mode of deployment
        /// </summary>
        [JsonProperty("mode"), JsonConverter(typeof(StringEnumConverter))]
        public DeploymentTypesEnum Mode { get; set; } = DeploymentTypesEnum.Incremental;

        /// <summary>
        /// Error handling deployment action
        /// </summary>
        [JsonProperty("onErrorDeployment")]
        public DeploymentRequestErrorHandlerDeployment OnErrorAction { get; set; } = new DeploymentRequestErrorHandlerDeployment();

        /// <summary>
        /// Output of the deployment run
        /// </summary>
        [JsonProperty("outputs")]
        public Dictionary<string, object>? Output { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Absolute URI to the deployment template. Mutually exclusive with Template
        /// </summary>
        [JsonProperty("templateLink")]
        public VersionedUri? TemplateURI { get; set; }

        /// <summary>
        /// Text of the template. Mutually exclusive with the TemplateURI
        /// </summary>
        [JsonProperty("template")]
        public string? Template { get; set; }

        /// <summary>
        /// Input parameter values (as defined in the template). Mutually exclusive with ParametersURI
        /// </summary>
        [JsonProperty("parameters")]
        public object? Parameters { get; set; }

        /// <summary>
        /// Input parameter values (as defined in the template). Mutually exclusive with the Parameters
        /// </summary>
        [JsonProperty("parametersLink")]
        public VersionedUri? ParametersURI { get; set; }

        /// <summary>
        /// Providers required for this deployment run
        /// </summary>
        [JsonProperty("providers")]
        public List<DeploymentProviderType> Providers { get; set; } = new List<DeploymentProviderType>();
    }
}
