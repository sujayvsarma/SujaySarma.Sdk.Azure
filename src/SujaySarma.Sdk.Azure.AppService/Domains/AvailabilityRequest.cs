using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Check availability of a domain name
    /// </summary>
    public class AvailabilityRequest
    {

        /// <summary>
        /// Domain name to check.
        /// </summary>
        [JsonProperty("name")]
        public string DomainName { get; set; } = "contoso.com";


        /// <summary>
        /// Default constructor
        /// </summary>
        public AvailabilityRequest() { }

        /// <summary>
        /// Instantiate the object
        /// </summary>
        /// <param name="name">Domain name to check</param>
        public AvailabilityRequest(string name)
        {
            if (!Common.Extensions.ValidateDomainName(name, SupportedTldNames))
            {
                throw new ArgumentOutOfRangeException($"The Tld in '{name}' is not supported for registration at this time.");
            }

            DomainName = name;
        }

        /// <summary>
        /// Validate the provided <see cref="DomainName"/>.
        /// </summary>
        /// <returns>True is everything is okay</returns>
        public bool Validate() => Common.Extensions.ValidateDomainName(DomainName, SupportedTldNames);


        /// <summary>
        /// Tld names supported for registration through this service
        /// </summary>
        private static readonly string[] SupportedTldNames = new string[]
        {
            // Order is not really important, but keep the single extensions 
            // ahead of the complex extensions

            "com", "net", "org", "biz",
            "nl", "in",

            "co.in", "co.uk", "org.uk"
        };
    }
}
