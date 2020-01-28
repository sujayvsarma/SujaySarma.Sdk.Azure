using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.DeletedWebApps
{
    /// <summary>
    /// Metadata about a deleted web app
    /// </summary>
    public class DeletedWebApp : AzureObjectBase
    {
        /// <summary>
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public List<DeletedWebAppProperties> Properties { get; set; } = new List<DeletedWebAppProperties>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DeletedWebApp() : base() { }
    }
}
