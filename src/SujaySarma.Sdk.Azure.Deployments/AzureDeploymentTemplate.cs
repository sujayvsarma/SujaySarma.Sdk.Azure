
using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Maps to the Deployment template structure for Azure Deployments
    /// REF: https://github.com/Azure/azure-quickstart-templates/blob/master/100-blank-template/azuredeploy.json
    /// </summary>
    public class AzureDeploymentTemplate
    {

        /// <summary>
        /// Schema. Hardcoded
        /// </summary>
        [JsonProperty("$schema")]
        public string Schema { get; set; } = "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#";

        /// <summary>
        /// Template content version. Always 1.0.0.0
        /// </summary>
        [JsonProperty("contentVersion")]
        public string ContentVersion { get; set; } = "1.0.0.0";

        /// <summary>
        /// List of resources
        /// </summary>
        [JsonProperty("resources")]
        public List<AzureDeploymentTemplateResource> Resources { get; set; } = new List<AzureDeploymentTemplateResource>();

        /// <summary>
        /// Parameters - we do not use this in our code-flow at this time
        /// </summary>
        [JsonProperty("parameters")]
        public object Parameters { get; set; } = new object();

        /// <summary>
        /// Template variables - we do not use this in our code-flow at this time
        /// </summary>
        [JsonProperty("variables")]
        public object Variables { get; set; } = new object();

        /// <summary>
        /// Outputs from deployment - we do not use this in our code-flow at this time
        /// </summary>
        [JsonProperty("outputs")]
        public object Outputs { get; set; } = new object();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AzureDeploymentTemplate() { }
    }

    /// <summary>
    /// A resource within an AzureDeploymentTemplate. 
    /// REF: https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/template-syntax
    /// </summary>
    public class AzureDeploymentTemplateResource
    {

        /// <summary>
        /// Version of the ARM API service to use
        /// </summary>
        [JsonProperty("apiVersion")]
        public string ArmApiVersion { get; set; } = string.Empty;

        /// <summary>
        /// Azure resource type name (eg: "Microsoft.Compute/virtualMachines")
        /// </summary>
        [JsonProperty("type")]
        public string AzureResourceType { get; set; } = string.Empty;

        /// <summary>
        /// Name of the resource. Can use ARM functions like 'uniqueString()' etc here.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Azure region (location) to deploy to. 
        /// Can use ARM functions like 'resourceGroup().Location' etc here.
        /// </summary>
        [JsonProperty("location")]
        public string AzureLocation { get; set; } = string.Empty;

        /// <summary>
        /// Tags to attach to the resource
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Serialized property values of the resource
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// List of resource Uris of dependency resources. The resources listed here should be deployed before 
        /// or exist, before deploying this current resource.
        /// </summary>
        [JsonProperty("dependsOn")]
        public List<string> Dependencies { get; set; } = new List<string>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AzureDeploymentTemplateResource() { }
    }
}
