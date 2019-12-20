using Newtonsoft.Json;

using System.Collections.Generic;


namespace SujaySarma.Sdk.Azure.ResourceGroups
{
    /// <summary>
    /// Denotes a resource group
    /// </summary>
    public class ResourceGroup : Common.AzureObjectBase
    {
        #region Properties

        /// <summary>
        /// Properties of the resource group. Will typically contain only one keypair: "provisioningState" with a 
        /// value of "Succeeded"
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, string> Properties { get; set; }

        #endregion

        public ResourceGroup() : base()
        {
            Properties = new Dictionary<string, string>();
        }

    }
}
