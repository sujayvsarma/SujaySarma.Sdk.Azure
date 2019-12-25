namespace SujaySarma.Sdk.Azure.Deployments
{
    /// <summary>
    /// Type of error handler
    /// </summary>
    public enum DeploymentRequestErrorHandlerTypesEnum
    {
        /// <summary>
        /// A specific deployment
        /// </summary>
        SpecificDeployment,

        /// <summary>
        /// The last successful deployment
        /// </summary>
        LastSuccessful
    }
}
