
namespace SujaySarma.Sdk.Azure.AppService.Common
{
    /// <summary>
    /// Names of types of app services resources
    /// </summary>
    public enum AppServiceResourceTypesEnum
    {

        /*
         * Order is not important, Name itself is important
         * 
         * !!! cASe is important!!!
         * 
         * Replace '.'s in name with '_' and replace "/' with '__' (double '_')
         * 
         */


        /// <summary>
        /// An App Service Environment
        /// </summary>
        HostingEnvironment,

        /// <summary>
        /// An App Service Website
        /// </summary>
        Site,

        /// <summary>
        /// An App Service slot (eg: Production, Staging)
        /// </summary>
        Slot,

        /// <summary>
        /// A user that has publishing rights to an App Service resource
        /// </summary>
        PublishingUser,


        /// <summary>
        /// An App Service Environment
        /// </summary>
        Microsoft_Web__hostingEnvironments,

        /// <summary>
        /// An App Service Website
        /// </summary>
        Microsoft_Web__sites,

        /// <summary>
        /// An App Service slot (eg: Production, Staging)
        /// </summary>
        Microsoft_Web__sites__slots,

        /// <summary>
        /// A user that has publishing rights to an App Service resource
        /// </summary>
        Microsoft_Web__publishingUsers

    }
}