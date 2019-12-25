namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Types of deployment
    /// </summary>
    public enum DeploymentTypesEnum
    {
        /// <summary>
        /// Complete deployment. Attempts to create all resources in the deployment
        /// </summary>
        Complete,

        /// <summary>
        /// Incremental deployment. Creates only those deployments that are new/changed from the 
        /// previous deployment. Acts as a Complete deployment if there was no previous deployment.
        /// </summary>
        Incremental
    }
}
