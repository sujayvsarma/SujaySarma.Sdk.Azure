namespace SujaySarma.Sdk.Azure.AppService.Domains
{
    /// <summary>
    /// Status of domain registration
    /// </summary>
    public enum DomainRegistrationStatusEnum
    {
        /// <summary>
        /// Domain is active and usable
        /// </summary>
        Active = 0,

        /// <summary>
        /// Awaiting status or update
        /// </summary>
        Awaiting,

        /// <summary>
        /// Domain has been cancelled
        /// </summary>
        Cancelled,

        /// <summary>
        /// Confiscated (by a court order, etc)
        /// </summary>
        Confiscated,

        /// <summary>
        /// Disabled for some reason
        /// </summary>
        Disabled,

        /// <summary>
        /// Excluded
        /// </summary>
        Excluded,

        /// <summary>
        /// Domain has expired
        /// </summary>
        Expired,

        /// <summary>
        /// Registration failed
        /// </summary>
        Failed,

        /// <summary>
        /// Domain registration has been held (for legal reasons?)
        /// </summary>
        Held,

        /// <summary>
        /// Registration has been locked
        /// </summary>
        Locked,

        /// <summary>
        /// Parked for use
        /// </summary>
        Parked,

        /// <summary>
        /// Pending registration
        /// </summary>
        Pending,

        /// <summary>
        /// Reserved from use
        /// </summary>
        Reserved,

        /// <summary>
        /// Reverted from a previous state
        /// </summary>
        Reverted,

        /// <summary>
        /// Suspended, typically because the parent subscription has been suspended 
        /// for billing reasons
        /// </summary>
        Suspended,

        /// <summary>
        /// Transferred out
        /// </summary>
        Transferred,

        /// <summary>
        /// Unknown status
        /// </summary>
        Unknown,

        /// <summary>
        /// Unlocked a previously locked registration
        /// </summary>
        Unlocked,

        /// <summary>
        /// Unparked
        /// </summary>
        Unparked,

        /// <summary>
        /// Registration updated
        /// </summary>
        Updated
    }
}
