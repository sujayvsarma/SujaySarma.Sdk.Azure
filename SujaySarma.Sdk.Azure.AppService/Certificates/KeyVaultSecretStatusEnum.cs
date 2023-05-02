namespace SujaySarma.Sdk.Azure.AppService.Certificates
{
    /// <summary>
    /// Status of the KeyVault secret
    /// </summary>
    public enum KeyVaultSecretStatusEnum
    {
        /// <summary>
        /// Status is not known
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Azure API services are not authorized to read the KeyVault
        /// </summary>
        AzureServiceUnauthorizedToAccessKeyVault,

        /// <summary>
        /// Certificate order failed
        /// </summary>
        CertificateOrderFailed,

        /// <summary>
        /// Private key of the certificate is not stored in the KeyVault, it is 
        /// stored somewhere else (perhaps we have a CER certificate?)
        /// </summary>
        ExternalPrivateKey,

        /// <summary>
        /// KeyVault has been initialized
        /// </summary>
        Initialized,

        /// <summary>
        /// The KeyVault specified (or previously attached) no longer exists. 
        /// Perhaps it was deleted?
        /// </summary>
        KeyVaultDoesNotExist,

        /// <summary>
        /// The secret no longer exists in the KeyVault. Perhaps it was deleted?
        /// </summary>
        KeyVaultSecretDoesNotExist,

        /// <summary>
        /// The operation attempted is not permitted on the KeyVault
        /// </summary>
        OperationNotPermittedOnKeyVault,

        /// <summary>
        /// Provisioning of the secret in the KeyVault succeeeded
        /// </summary>
        Succeeded,

        /// <summary>
        /// Some error condition
        /// </summary>
        UnknownError,

        /// <summary>
        /// Certificate order is still pending
        /// </summary>
        WaitingOnCertificateOrder
    }
}
