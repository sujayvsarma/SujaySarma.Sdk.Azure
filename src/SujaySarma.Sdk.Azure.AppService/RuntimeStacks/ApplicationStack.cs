using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.RuntimeStacks
{
    /// <summary>
    /// Definition of one available application stack
    /// </summary>
    public class ApplicationStack : AzureObjectBase
    {

        /// <summary>
        /// Properties of the stack
        /// </summary>
        [JsonProperty("properties")]
        public ApplicationStackProperties Properties { get; set; } = new ApplicationStackProperties();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationStack() : base() { }
    }
}
