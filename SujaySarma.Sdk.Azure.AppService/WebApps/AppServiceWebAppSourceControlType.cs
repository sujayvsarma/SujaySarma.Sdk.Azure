namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Type of source control system attached to an Azure App Service
    /// </summary>
    public enum AppServiceWebAppSourceControlType
    {
        None = 0,

        /// <summary>
        /// Bitbucket with Git protocol
        /// </summary>
        BitbucketGit,

        /// <summary>
        /// Bitbucket with Hg protocol
        /// </summary>
        BitbucketHg,

        /// <summary>
        /// Codeplex with Git protocol
        /// </summary>
        CodePlexGit,

        /// <summary>
        /// Codeplex with Hg protocol
        /// </summary>
        CodePlexHg,

        /// <summary>
        /// Dropbox
        /// </summary>
        Dropbox,

        /// <summary>
        /// External Git repository
        /// </summary>
        ExternalGit,

        /// <summary>
        /// External Hg repository
        /// </summary>
        ExternalHg,

        /// <summary>
        /// Github.com
        /// </summary>
        GitHub,

        /// <summary>
        /// Local Git repository
        /// </summary>
        LocalGit,

        /// <summary>
        /// Microsoft OneDrive
        /// </summary>
        OneDrive,

        /// <summary>
        /// Microsoft Team Foundation Server (on-prem)
        /// </summary>
        Tfs,

        /// <summary>
        /// Azure DevOps, formerly "Microsoft Visual Studio Online"
        /// </summary>
        VSO,

        /// <summary>
        /// Visual Studio Team Services RM
        /// </summary>
        VSTSRM
    }

}
