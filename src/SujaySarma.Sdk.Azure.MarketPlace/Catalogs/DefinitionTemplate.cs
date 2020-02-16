using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// Provides links to auxilliary Json structures that help define an item from the marketplace
    /// </summary>
    public class DefinitionTemplate
    {

        /// <summary>
        /// Uri to the UI definition Json file
        /// </summary>
        [JsonProperty("uiDefinitionFileUri")]
        public string? UIDefinitionFileUri { get; set; }

        /// <summary>
        /// Name of the deployment template to use
        /// </summary>
        [JsonProperty("defaultDeploymentTemplateId")]
        public string? DefaultDeploymentTemplate { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefinitionTemplate() { }

    }
}
