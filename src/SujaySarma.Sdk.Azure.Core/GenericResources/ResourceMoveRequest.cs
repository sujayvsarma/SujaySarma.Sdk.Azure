using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Core.GenericResources
{
    /// <summary>
    /// Request sent to the AzureRM REST API to move resources from one 
    /// RG to another. This is used only internally.
    /// </summary>
    public class ResourceMoveRequest
    {
        /// <summary>
        /// Absolute Resource Id of the target resource group
        /// </summary>
        [JsonProperty("targetResourceGroup")]
        public string TargetResourceGroupId { get; set; } = string.Empty;

        /// <summary>
        /// List of absolute Resource Ids of the resources to move out. All of them must be 
        /// in the same resource group.
        /// </summary>
        [JsonProperty("resources")]
        public List<string> Resources { get; set; } = new List<string>();


        public ResourceMoveRequest() { }
    }
}
