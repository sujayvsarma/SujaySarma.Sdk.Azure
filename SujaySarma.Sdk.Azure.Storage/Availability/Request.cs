using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Storage.Availability
{
    /// <summary>
    /// Availability request
    /// </summary>
    public class Request
    {

        /// <summary>
        /// Name to check
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of resource
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = "Microsoft.Storage/storageAccounts";

        /// <summary>
        /// Default constructor
        /// </summary>
        public Request() { }

        /// <summary>
        /// Create a request
        /// </summary>
        /// <param name="name">Name to check for</param>
        public Request(string name) => Name = name;

    }
}
