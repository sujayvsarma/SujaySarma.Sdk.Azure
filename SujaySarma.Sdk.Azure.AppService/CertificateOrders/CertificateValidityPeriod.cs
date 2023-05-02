namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Period of validity for an SSL certificate
    /// </summary>
    public enum CertificateValidityPeriod
    {
        /// <summary>
        /// Service default - one year
        /// </summary>
        Default = OneYear,

        /// <summary>
        /// One year
        /// </summary>
        OneYear = 1,

        /// <summary>
        /// Two years
        /// </summary>
        TwoYears = 2,

        /// <summary>
        /// Three years
        /// </summary>
        ThreeYears = 3
    }
}
