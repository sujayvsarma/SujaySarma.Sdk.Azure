using Newtonsoft.Json;
using System;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Properties of a deployment request
    /// </summary>
    public class DeploymentRequestProperties
    {
        /// <summary>
        /// Absolute URI to the deployment template to process. Ideally this is hosted on 
        /// an Azure Storage Blob, but it can be anywhere that the Azure system can access
        /// </summary>
        [JsonProperty("templateLink")]
        public string TemplateURI { get; set; }

        /// <summary>
        /// Input parameter values (as defined in the template)
        /// </summary>
        [JsonProperty("parameters")]
        public object Parameters { get; set; }

        /// <summary>
        /// Mode of deployment (for example: "Complete" or "Incremental")
        /// </summary>
        [JsonProperty("mode", ItemConverterType = typeof(DeploymentTypesEnum))]
        public DeploymentTypesEnum Mode { get; set; } = DeploymentTypesEnum.Incremental;

        /// <summary>
        /// How to handle an error during deployment
        /// </summary>
        [JsonProperty("onErrorDeployment")]
        public DeploymentRequestErrorHandlerDeployment OnError { get; set; }

        /// <summary>
        /// Create a new request
        /// </summary>
        public DeploymentRequestProperties()
        {
            TemplateURI = string.Empty;
            Parameters = new object();
            Mode = DeploymentTypesEnum.Incremental;
            OnError = new DeploymentRequestErrorHandlerDeployment();
        }


        /// <summary>
        /// Create a new request
        /// </summary>
        /// <param name="templateUri">URI to the deployment ARM template</param>
        /// <param name="mode">Mode of deployment</param>
        /// <param name="templateParameters">Object containing the parameter values for the deployment template</param>
        /// <param name="errorHandlerMode">Mode of error handling</param>
        /// <param name="redeployDeploymentName">If <paramref name="errorHandlerMode"/> is SpecificDeployment, the name of the deployment to redeploy</param>
        public DeploymentRequestProperties(string templateUri, DeploymentTypesEnum mode, object? templateParameters,
            DeploymentRequestErrorHandlerTypesEnum errorHandlerMode, string? redeployDeploymentName = null)
        {
            if (string.IsNullOrWhiteSpace(templateUri)) { throw new ArgumentNullException(nameof(templateUri)); }
            if (!Enum.IsDefined(typeof(DeploymentTypesEnum), mode)) { throw new ArgumentOutOfRangeException(nameof(mode)); }
            if (!Enum.IsDefined(typeof(DeploymentRequestErrorHandlerTypesEnum), errorHandlerMode)) { throw new ArgumentOutOfRangeException(nameof(errorHandlerMode)); }

            TemplateURI = templateUri;
            Mode = mode;
            Parameters = templateParameters ?? new object();
            OnError = new DeploymentRequestErrorHandlerDeployment();
            if (errorHandlerMode == DeploymentRequestErrorHandlerTypesEnum.SpecificDeployment)
            {
                if (string.IsNullOrWhiteSpace(redeployDeploymentName)) { throw new ArgumentNullException(nameof(redeployDeploymentName)); }
                
                OnError.Type = DeploymentRequestErrorHandlerTypesEnum.SpecificDeployment;
                OnError.DeploymentName = redeployDeploymentName;
            }
        }
    }
}
