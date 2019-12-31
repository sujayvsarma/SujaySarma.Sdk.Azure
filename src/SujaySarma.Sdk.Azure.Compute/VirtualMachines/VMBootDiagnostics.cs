using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.VirtualMachines
{
    /// <summary>
    /// Settings for boot-up logging and diagnostics
    /// </summary>
    public class VMBootDiagnostics
    {
        /// <summary>
        /// If this is/should be enabled
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// Uri to a Azure Storage Blob account where the diagnostics information 
        /// should be stored.
        /// </summary>
        [JsonProperty("storageUri")]
        public string? StorageUri { get; set; } = null;


        public VMBootDiagnostics() { }

        /// <summary>
        /// Enable boot diagnostics
        /// </summary>
        /// <param name="storageAccountBlobUri">Uri to a Azure Storage Blob account where the diagnostics information should be stored.</param>
        public VMBootDiagnostics(ResourceUri storageAccountBlobUri)
        {
            if ((storageAccountBlobUri == null) || (!storageAccountBlobUri.IsValid) ||
                (!storageAccountBlobUri.Is(ResourceUriCompareLevel.Provider, "Microsoft.Storage")) || (!storageAccountBlobUri.Is(ResourceUriCompareLevel.Type, "storageAccounts")))
            {
                throw new ArgumentException(nameof(storageAccountBlobUri));
            }

            IsEnabled = true;
            StorageUri = storageAccountBlobUri.ToString();
        }
    }
}
