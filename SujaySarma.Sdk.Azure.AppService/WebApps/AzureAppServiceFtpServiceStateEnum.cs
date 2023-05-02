namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Status of FTP service for an Azure App Service app
    /// </summary>
    public enum AppServiceFtpServiceStateEnum
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = AllAllowed,

        /// <summary>
        /// Both FTP and FTPS are allowed
        /// </summary>
        AllAllowed = 0,

        /// <summary>
        /// FTPS (secure FTP) only
        /// </summary>
        FtpsOnly,

        /// <summary>
        /// FTP is disabled
        /// </summary>
        Disabled
    }
}
