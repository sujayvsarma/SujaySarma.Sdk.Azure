using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Request for a deployment
    /// </summary>
    public class DeploymentRequest
    {
        /// <summary>
        /// Properties of the request
        /// </summary>
        [JsonProperty("properties")]
        public DeploymentRequestProperties Properties { get; set; }

        /// <summary>
        /// Location of deployment (required for Subscription level deployments)
        /// </summary>
        [JsonProperty("location")]
        public string? Location { get; set; } = null;


        public DeploymentRequest()
        {
            Properties = new DeploymentRequestProperties();
        }
    }
}
