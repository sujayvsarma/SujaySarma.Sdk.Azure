using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;


namespace SujaySarma.Sdk.Azure.ResourceGroups
{
    /// <summary>
    /// Denotes a resource group
    /// </summary>
    public class ResourceGroup : AzureObjectBase
    {
        /// <summary>
        /// Properties of the resource group. Will typically contain only one keypair: "provisioningState" with a 
        /// value of "Succeeded"
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, string> Properties { get; set; }


        public ResourceGroup() : base()
        {
            Properties = new Dictionary<string, string>();
        }

    }
}
