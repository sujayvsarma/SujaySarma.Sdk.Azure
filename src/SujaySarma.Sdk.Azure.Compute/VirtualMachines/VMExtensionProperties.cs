using Newtonsoft.Json;

using System;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Properties of a VMExtension
    /// </summary>
    public class VMExtensionProperties
    {
        /// <summary>
        /// Indicates whether the extension should use a newer minor version if one is 
        /// available at deployment time. Once deployed, however, the extension will not 
        /// upgrade minor versions unless redeployed, even with this property set to true 
        /// </summary>
        [JsonProperty("autoUpgradeMinorVersion")]
        public bool InstallNewerMinorVersionIfAvailable { get; set; } = true;

        /// <summary>
        /// Force an update to the extension even if nothing has changed.
        /// </summary>
        [JsonProperty("forceUpdateTag")]
        public string? ForceUpdateMode { get; set; } = null;

        /// <summary>
        /// Runtime view of the extension
        /// </summary>
        [JsonProperty("instanceView")]
        public VMExtensionInstanceView? InstanceView { get; set; } = null;

        /// <summary>
        /// Either protectedSettings or protectedSettingsFromKeyVault or no protected settings at all
        /// </summary>
        [JsonProperty("protectedSettings")]
        public object? ProtectedSettings { get; set; } = null;

        /// <summary>
        /// Either protectedSettings or protectedSettingsFromKeyVault or no protected settings at all
        /// </summary>
        [JsonProperty("protectedSettingsFromKeyVault")]
        public object? ProtectedSettingsFromKeyVault { get; set; } = null;

        /// <summary>
        /// Provisioning state
        /// </summary>
        [JsonProperty("provisioningState")]
        public string? ProvisioningState { get; set; } = "Succeeded";

        /// <summary>
        /// Name of the extension publisher
        /// </summary>
        [JsonProperty("publisher")]
        public string? PublisherName { get; set; }

        /// <summary>
        /// Json-formatted public settings for the extension
        /// </summary>
        [JsonProperty("settings")]
        public string? Settings { get; set; }

        /// <summary>
        /// Type of extension
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Version of the type handler for this extension
        /// </summary>
        [JsonProperty("typeHandlerVersion")]
        public string? TypeHandlerVersion { get; set; }


        public VMExtensionProperties() { }

        /// <summary>
        /// Initialize properties for the VM extension
        /// </summary>
        public VMExtensionProperties(string type, string typeHandlerVersion, string? publisherName, string? jsonSerializedSettings = null, bool useNewerMinorVersionIfAvailable = true)
        {
            if (string.IsNullOrWhiteSpace(type)) { throw new ArgumentNullException(nameof(type)); }
            if (string.IsNullOrWhiteSpace(typeHandlerVersion)) { throw new ArgumentNullException(nameof(typeHandlerVersion)); }
            if (string.IsNullOrWhiteSpace(publisherName))
            {
                publisherName = null;
            }

            if (string.IsNullOrWhiteSpace(jsonSerializedSettings))
            {
                jsonSerializedSettings = null;
            }

            InstallNewerMinorVersionIfAvailable = useNewerMinorVersionIfAvailable;
            Type = type;
            TypeHandlerVersion = typeHandlerVersion;
            PublisherName = publisherName;
            Settings = jsonSerializedSettings;
        }

        /// <summary>
        /// Configure protected settings
        /// </summary>
        /// <param name="settings">Object containing details about the protected settings to use</param>
        /// <param name="isSettingsFromKeyVault">If set, sets the settings to be used from an Azure Key Vault</param>
        public void UseProtectedSettings(object settings, bool isSettingsFromKeyVault)
        {
            if (settings == null) { throw new ArgumentNullException(nameof(settings)); }

            if (isSettingsFromKeyVault)
            {
                ProtectedSettingsFromKeyVault = settings;
            }
            else
            {
                ProtectedSettings = settings;
            }
        }
    }
}
