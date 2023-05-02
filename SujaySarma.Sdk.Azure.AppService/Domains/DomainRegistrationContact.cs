
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Contact for a domain registration
    /// </summary>
    public class DomainRegistrationContact
    {
        /// <summary>
        /// First name
        /// </summary>
        [JsonProperty("nameFirst")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name
        /// </summary>
        [JsonProperty("nameLast")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Middle name
        /// </summary>
        [JsonProperty("nameMiddle")]
        public string? MiddleName { get; set; } = null;

        /// <summary>
        /// Name of the organization
        /// </summary>
        [JsonProperty("organization")]
        public string? Organization { get; set; } = null;

        /// <summary>
        /// Job title of the contact
        /// </summary>
        [JsonProperty("jobTitle")]
        public string? JobTitle { get; set; } = null;

        /// <summary>
        /// Full international phone number in any acceptable phone number format.
        /// </summary>
        [JsonProperty("phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Email address
        /// </summary>
        [JsonProperty("email")]
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Address of the contact
        /// </summary>
        [JsonProperty("addressMailing")]
        public DomainRegistrationContactAddress Address { get; set; } = new DomainRegistrationContactAddress();


        /// <summary>
        /// Default constructor
        /// </summary>
        public DomainRegistrationContact() { }
    }

}
