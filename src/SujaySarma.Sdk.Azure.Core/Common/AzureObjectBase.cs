using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// The basic object in Azure
    /// </summary>
    public class AzureObjectBase
    {

        #region Properties

        /// <summary>
        /// The ResourceId URI for the object
        /// </summary>
        [JsonProperty("id")]
        public string ResourceId { get; set; }

        /// <summary>
        /// Name of the object
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Type of object
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Azure data center location where this is located
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// Tags attached to the object
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<string, string>? Tags { get; set; } = null;

        #endregion


        public AzureObjectBase()
        {
            ResourceId = "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/00000000-0000-0000-0000-000000000000/nullObject";
            Name = "Null Object";
            Type = "SujaySarma.Sdk.Azure/nullObject";
            Location = "EarthsCore";
        }

    }
}
