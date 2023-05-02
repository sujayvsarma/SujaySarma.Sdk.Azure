namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// The state in which to execute the unattended setup commands
    /// </summary>
    public enum VMUnattendedWindowsSetupRunAtEnum
    {
        /// <summary>
        /// Run at first login of any user
        /// </summary>
        FirstLogonCommands,

        /// <summary>
        /// Automatically login and finish these steps
        /// </summary>
        AutoLogon
    }
}
