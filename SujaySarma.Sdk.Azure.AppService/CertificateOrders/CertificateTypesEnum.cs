namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Named certificate type
    /// </summary>
    public enum CertificateTypesEnum
    {
        /// <summary>
        /// Standard domain-validated SSL
        /// </summary>
        StandardDomainValidatedSsl = 0,

        /// <summary>
        /// Standard domain-validated wildcard SSL 
        /// (warning: costs a whole lot more!)
        /// </summary>
        StandardDomainValidatedWildCardSsl
    }
}
