using System;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// Type of identity assigned to a resource
    /// </summary>
    [Flags]
    public enum ResourceIdentityTypeEnum
    {
        /// <summary>
        /// Is a user-assigned identity
        /// </summary>
        UserAssigned = 0,

        /// <summary>
        /// Is a system-assigned identity
        /// </summary>
        SystemAssigned
    }
}
