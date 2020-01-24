namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Certificate status
    /// </summary>
    public enum CertificateOrderStatus
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = NotSubmitted,

        /// <summary>
        /// Not yet submitted
        /// </summary>
        NotSubmitted = 0,

        /// <summary>
        /// Cancelled
        /// </summary>
        Canceled,

        /// <summary>
        /// Certificate was denied
        /// </summary>
        Denied,

        /// <summary>
        /// Order expired
        /// </summary>
        Expired,

        /// <summary>
        /// Certificate was issued
        /// </summary>
        Issued,

        /// <summary>
        /// Pending re-key operation (in case of renewal with a new private key)
        /// </summary>
        PendingRekey,

        /// <summary>
        /// Pending final issuance
        /// </summary>
        Pendingissuance,

        /// <summary>
        /// Pending revocation
        /// </summary>
        Pendingrevocation,

        /// <summary>
        /// Has been revoked
        /// </summary>
        Revoked,

        /// <summary>
        /// Unused (???)
        /// </summary>
        Unused
    }
}
