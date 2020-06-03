using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Error handling during a deployment
    /// </summary>
    public class DeploymentRequestErrorHandlerDeployment
    {
        /// <summary>
        /// Type of error handling. One of: "SpecificDeployment" or "LastSuccessful"
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public DeploymentRequestErrorHandlerTypesEnum Type { get; set; } = DeploymentRequestErrorHandlerTypesEnum.LastSuccessful;

        /// <summary>
        /// Name of deployment, only required if Type = "SpecificDeployment"
        /// </summary>
        [JsonProperty("deploymentName")]
        public string? DeploymentName { get; set; }

        /// <summary>
        /// Create a new error handler (sets for LastSuccessful)
        /// </summary>
        public DeploymentRequestErrorHandlerDeployment()
        {
            Type = DeploymentRequestErrorHandlerTypesEnum.LastSuccessful;
            DeploymentName = null;
        }

        /// <summary>
        /// Create a new specific deployment handler
        /// </summary>
        /// <param name="deploymentName">Name of the specific deployment to redeploy</param>
        public DeploymentRequestErrorHandlerDeployment(string deploymentName)
        {
            if (string.IsNullOrWhiteSpace(deploymentName)) { throw new System.ArgumentNullException(nameof(deploymentName)); }

            Type = DeploymentRequestErrorHandlerTypesEnum.SpecificDeployment;
            DeploymentName = deploymentName;
        }

    }
}
