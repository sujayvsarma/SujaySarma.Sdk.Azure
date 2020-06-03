using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Properties of a deployment request
    /// </summary>
    public class DeploymentRequestProperties
    {
        /// <summary>
        /// Absolute URI to the deployment template to process. Ideally this is hosted on 
        /// an Azure Storage Blob, but it can be anywhere that the Azure system can access. 
        /// Use mutually exclusively with the Template property.
        /// </summary>
        [JsonProperty("templateLink")]
        public VersionedUri? TemplateURI { get; set; }

        /// <summary>
        /// Text of the template. Use mutually exclusively with the TemplateURI property
        /// </summary>
        [JsonProperty("template")]
        public object? Template { get; set; }

        /// <summary>
        /// Input parameter values (as defined in the template)
        /// </summary>
        [JsonProperty("parameters")]
        public object? Parameters { get; set; }

        /// <summary>
        /// Input parameter values (as defined in the template). Use mutually exclusively with the Parameters property.
        /// </summary>
        [JsonProperty("parametersLink")]
        public VersionedUri? ParametersURI { get; set; }

        /// <summary>
        /// Mode of deployment (for example: "Complete" or "Incremental")
        /// </summary>
        [JsonProperty("mode"), JsonConverter(typeof(StringEnumConverter))]
        public DeploymentTypesEnum Mode { get; set; } = DeploymentTypesEnum.Incremental;

        /// <summary>
        /// How to handle an error during deployment
        /// </summary>
        [JsonProperty("onErrorDeployment")]
        public DeploymentRequestErrorHandlerDeployment? OnError { get; set; }

        /// <summary>
        /// Create a new request
        /// </summary>
        public DeploymentRequestProperties()
        {
            Mode = DeploymentTypesEnum.Incremental;
            OnError = new DeploymentRequestErrorHandlerDeployment();
        }

        /// <summary>
        /// Set deployment mode to incremental
        /// </summary>
        public DeploymentRequestProperties WithIncrementalDeployment()
        {
            Mode = DeploymentTypesEnum.Incremental;
            return this;
        }

        /// <summary>
        /// Set deployment mode to full
        /// </summary>
        public DeploymentRequestProperties WithFullDeployment()
        {
            Mode = DeploymentTypesEnum.Complete;
            return this;
        }

        /// <summary>
        /// Set the template content body (will replace the TemplateURI with NULL)
        /// </summary>
        public DeploymentRequestProperties WithTemplateContent(string templateContent)
        {
            Template = templateContent;
            TemplateURI = null;
            return this;
        }

        /// <summary>
        /// Set template link URI (will replace Template content to NULL)
        /// </summary>
        public DeploymentRequestProperties WithTemplateLink(string templateUri, string contentVersion = "1.0.0.0")
        {
            TemplateURI = new VersionedUri(templateUri) { Version = contentVersion };
            Template = null;
            return this;
        }

        /// <summary>
        /// Set the parameters content body (will replace the ParametersURI to NULL)
        /// </summary>
        public DeploymentRequestProperties WithParametersContent(object parameters)
        {
            Parameters = parameters;
            ParametersURI = null;
            return this;
        }

        /// <summary>
        /// Set parameters link Uri (will replace parameters content to NULL)
        /// </summary>
        public DeploymentRequestProperties WithParametersLink(string parametersUri, string contentVersion = "1.0.0.0")
        {
            ParametersURI = new VersionedUri(parametersUri) { Version = contentVersion };
            Parameters = null;
            return this;
        }

        /// <summary>
        /// Set error handler to redeploy last successful deployment
        /// </summary>
        public DeploymentRequestProperties WithOnErrorRedeployLast()
        {
            OnError = new DeploymentRequestErrorHandlerDeployment();
            return this;
        }

        /// <summary>
        /// Set error handler to deploy a specific previous deployment
        /// </summary>
        public DeploymentRequestProperties WithOnErrorDeploy(string deploymentName)
        {
            OnError = new DeploymentRequestErrorHandlerDeployment(deploymentName);
            return this;
        }
    }

}
