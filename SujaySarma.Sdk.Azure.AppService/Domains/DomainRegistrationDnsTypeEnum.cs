namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Type of DNS server that will be used with a domain
    /// </summary>
    public enum DomainRegistrationDnsTypeEnum
    {
        /// <summary>
        /// Azure DNS. Azure will create a matching DNS zone for this domain.
        /// </summary>
        AzureDns = 0,

        /// <summary>
        /// A DNS server provided by the registrar or the customer. 
        /// In this case, Azure will not create a matching DNS zone for this 
        /// domain.
        /// </summary>
        DefaultDomainRegistrarDns
    }
}
