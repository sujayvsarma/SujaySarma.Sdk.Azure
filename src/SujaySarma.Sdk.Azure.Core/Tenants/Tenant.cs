using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Tenants
{
    /// <summary>
    /// Details about an Azure Tenant. This is purely a result-data structure and there are no code flows 
    /// that require the caller to generate this data.
    /// </summary>
    public class Tenant
    {

        #region Properties

        /// <summary>
        /// The tenant ID URI
        /// </summary>
        [JsonProperty("id")]
        public string TenantId { get; set; }

        /// <summary>
        /// The Guid for this tenant
        /// </summary>
        [JsonProperty("tenantId")]
        public Guid Id { get; set; }

        /// <summary>
        /// Display name of the tenant
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// ISO Country code where the tenant's administrator(s) have classified as 
        /// being homed in
        /// </summary>
        [JsonProperty("countryCode")]
        public string? CountryCode { get; set; }

        /// <summary>
        /// The category name of the tenant - typically "Home", but are there any other values?
        /// </summary>
        [JsonProperty("tenantCategory")]
        public string? Category { get; set; }

        /// <summary>
        /// Associated domain names. Will at least contain the ".onmicrosoft.com" domain + 
        /// if present, custom domain associations
        /// </summary>
        [JsonProperty("domains")]
        public List<string> AssociatedDomainNames { get; set; }

        /// <summary>
        /// Returns the primary domain name associated with this tenant. If it could not be determined for 
        /// some reason, will return ".onmicrosoft.com".
        /// </summary>
        public string PrimaryDomainName
        {
            get
            {
                if ((_primaryName == string.Empty) && (AssociatedDomainNames != null))
                {
                    foreach (string d in AssociatedDomainNames)
                    {
                        if (d.EndsWith(".onmicrosoft.com") && (!d.EndsWith(".mail.onmicrosoft.com")))
                        {
                            _primaryName = d;
                            break;
                        }
                    }
                }

                return _primaryName;
            }
        }
        private string _primaryName = string.Empty;

        #endregion

        public Tenant()
        {
            TenantId = "/tenants/00000000-0000-0000-0000-000000000000";
            Id = Guid.Empty;
            DisplayName = "Default tenant";
            AssociatedDomainNames = new List<string>();
        }
    }
}
