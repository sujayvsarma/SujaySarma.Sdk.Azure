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


        public DeploymentRequest()
        {
            Properties = new DeploymentRequestProperties();
        }
    }
}
