using Newtonsoft.Json;

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
        [JsonProperty("mode")]
        public string Mode { get; set; }

        /// <summary>
        /// How to handle an error during deployment
        /// </summary>
        [JsonProperty("onErrorDeployment")]
        public DeploymentRequestErrorHandlerDeployment OnError { get; set; }


        public DeploymentRequestProperties()
        {
            TemplateURI = string.Empty;
            Parameters = new object();
            Mode = "Complete";
            OnError = new DeploymentRequestErrorHandlerDeployment();
        }

    }
}
