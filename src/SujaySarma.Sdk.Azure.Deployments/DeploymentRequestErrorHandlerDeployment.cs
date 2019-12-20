using Newtonsoft.Json;

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
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Name of deployment, only required if Type = "SpecificDeployment"
        /// </summary>
        [JsonProperty("deploymentName")]
        public string? DeploymentName { get; set; }

        public DeploymentRequestErrorHandlerDeployment()
        {
            Type = "LastSuccessful";
            DeploymentName = null;
        }

    }
}
