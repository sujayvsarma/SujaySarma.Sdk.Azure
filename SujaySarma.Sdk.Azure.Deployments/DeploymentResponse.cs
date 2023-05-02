﻿
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Response of a deployment action
    /// </summary>
    public class DeploymentResponse
    {
        /// <summary>
        /// Deployment ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Location where the deployment metadata is stored
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; } = "global";

        /// <summary>
        /// Name of the deployment (provided when the deployment is queued)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of deployment ("Microsoft.Resources/deployments")
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = "Microsoft.Resources/deployments";

        /// <summary>
        /// Properties of the deployment
        /// </summary>
        [JsonProperty("properties")]
        public DeploymentResponseProperties Properties { get; set; } = new DeploymentResponseProperties();

    }
}
