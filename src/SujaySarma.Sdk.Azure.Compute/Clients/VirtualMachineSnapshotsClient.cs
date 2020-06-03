using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Snapshots;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    public static class VirtualMachineSnapshotsClient
    {
        /// <summary>
        /// Take a new snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the snapshot into</param>
        /// <param name="snapshotName">Name of the snapshot</param>
        /// <param name="location">Azure data center geo location short/internal name (eg: "westus")</param>
        /// <param name="properties">Properties of the snapshot</param>
        /// <param name="tags">Tags (optional)</param>
        /// <returns>VMSnapshot object if successful, else NULL</returns>
        public static async Task<VMSnapshot?> Take(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, string snapshotName,
            string location, VMSnapshotProperties properties, Dictionary<string, string>? tags = null)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }
            if (string.IsNullOrWhiteSpace(snapshotName)) { throw new ArgumentNullException(nameof(snapshotName)); }
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }

            string vmResourceUri = $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/snapshots/{snapshotName}";
            VMSnapshot snapshot = new VMSnapshot()
            {
                Location = location,
                ManagedBy = vmResourceUri,
                Name = snapshotName,
                ResourceId = $"/{vmResourceUri}/snapshots/{snapshotName}",
                Tags = tags,
                Type = "virtualMachines/snapshots",
                Properties = new VMSnapshotProperties()
                {
                    CreationMetadata = properties.CreationMetadata,
                    DiskSizeGB = properties.DiskSizeGB,
                    ContentEncryptionProperties = properties.ContentEncryptionProperties,
                    AzureDiskEncryptionSettings = properties.AzureDiskEncryptionSettings,
                    HyperVGeneration = properties.HyperVGeneration,
                    IsIncrementalSnapshot = properties.IsIncrementalSnapshot,
                    OperatingSystem = properties.OperatingSystem
                }
            };

            return await Take(bearerToken, snapshot);
        }

        /// <summary>
        /// Take a new snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="snapshotName">Name of the snapshot</param>
        /// <param name="location">Azure data center geo location short/internal name (eg: "westus")</param>
        /// <param name="properties">Properties of the snapshot</param>
        /// <param name="tags">Tags (optional)</param>
        /// <returns>VMSnapshot object if successful, else NULL</returns>
        public static async Task<VMSnapshot?> Create(string bearerToken, string virtualMachineResourceUri, string snapshotName,
            string location, VMSnapshotProperties properties, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }
            if (string.IsNullOrWhiteSpace(snapshotName)) { throw new ArgumentNullException(nameof(snapshotName)); }
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }

            VMSnapshot snapshot = new VMSnapshot()
            {
                Location = location,
                ManagedBy = virtualMachineResourceUri,
                Name = snapshotName,
                ResourceId = $"{virtualMachineResourceUri}/snapshots/{snapshotName}",
                Tags = tags,
                Type = "virtualMachines/snapshots",
                Properties = new VMSnapshotProperties()
                {
                    CreationMetadata = properties.CreationMetadata,
                    DiskSizeGB = properties.DiskSizeGB,
                    ContentEncryptionProperties = properties.ContentEncryptionProperties,
                    AzureDiskEncryptionSettings = properties.AzureDiskEncryptionSettings,
                    HyperVGeneration = properties.HyperVGeneration,
                    IsIncrementalSnapshot = properties.IsIncrementalSnapshot,
                    OperatingSystem = properties.OperatingSystem
                }
            };

            return await Take(bearerToken, snapshot);
        }

        /// <summary>
        /// Take or update a snapshot. If the snapshot already exists, it is replaced.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineSnapshot">The prepopulated Virtual Machine Snapshot object</param>
        /// <returns>VMSnapshot object if successful, else NULL</returns>
        public static async Task<VMSnapshot?> Take(string bearerToken, VMSnapshot virtualMachineSnapshot)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(virtualMachineSnapshot)); }
            if (virtualMachineSnapshot == null) { throw new ArgumentNullException(nameof(virtualMachineSnapshot)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineSnapshot.ResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    virtualMachineSnapshot,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VMSnapshot>(response.Body);
        }

        /// <summary>
        /// Delete a virtual machine snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the snapshot into</param>
        /// <param name="snapshotName">Name of the snapshot</param>
        /// <returns>True if the task was accepted successfully</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, string snapshotName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }
            if (string.IsNullOrWhiteSpace(snapshotName)) { throw new ArgumentNullException(nameof(snapshotName)); }

            return await Delete(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/snapshots/{snapshotName}");
        }

        /// <summary>
        /// Delete a virtual machine snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the Virtual Machine</param>
        /// <param name="snapshotName">Name of the snapshot</param>
        /// <returns>True if the task was accepted successfully</returns>
        public static async Task<bool> Delete(string bearerToken, string virtualMachineResourceUri, string snapshotName)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }
            if (string.IsNullOrWhiteSpace(snapshotName)) { throw new ArgumentNullException(nameof(snapshotName)); }

            return await Delete(bearerToken, $"{virtualMachineResourceUri}/snapshots/{snapshotName}");
        }

        /// <summary>
        /// Delete a virtual machine snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineSnapshotResourceUri">Resource Uri to the Virtual Machine snapshot</param>
        /// <returns>True if the task was accepted successfully</returns>
        public static async Task<bool> Delete(string bearerToken, string virtualMachineSnapshotResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineSnapshotResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineSnapshotResourceUri)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineSnapshotResourceUri[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 202, 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get a virtual machine snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the snapshot into</param>
        /// <param name="snapshotName">Name of the snapshot</param>
        /// <returns>Virtual machine snapshot or NULL</returns>
        public static async Task<VMSnapshot?> Get(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, string snapshotName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }
            if (string.IsNullOrWhiteSpace(snapshotName)) { throw new ArgumentNullException(nameof(snapshotName)); }

            return await Get(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/snapshots/{snapshotName}");
        }

        /// <summary>
        /// Get a virtual machine snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the Virtual Machine</param>
        /// <param name="snapshotName">Name of the snapshot</param>
        /// <returns>Virtual machine snapshot or NULL</returns>
        public static async Task<VMSnapshot?> Get(string bearerToken, string virtualMachineResourceUri, string snapshotName)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }
            if (string.IsNullOrWhiteSpace(snapshotName)) { throw new ArgumentNullException(nameof(snapshotName)); }

            return await Get(bearerToken, $"{virtualMachineResourceUri}/snapshots/{snapshotName}");
        }

        /// <summary>
        /// Get a virtual machine snapshot
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineSnapshotResourceUri">Resource Uri to the Virtual Machine snapshot</param>
        /// <returns>Virtual machine snapshot or NULL</returns>
        public static async Task<VMSnapshot?> Get(string bearerToken, string virtualMachineSnapshotResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineSnapshotResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineSnapshotResourceUri)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineSnapshotResourceUri[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 202, 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VMSnapshot>(response.Body);
        }

        /// <summary>
        /// List all virtual machine snapshots on a virtual machine
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the snapshot into</param>
        /// <returns>List of virtual machine snapshot or empty list</returns>
        public static async Task<List<VMSnapshot>> List(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await List(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/snapshots");
        }

        /// <summary>
        /// List all virtual machine snapshots on a virtual machine
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the Virtual Machine</param>
        /// <returns>List of virtual machine snapshots or NULL</returns>
        public static async Task<List<VMSnapshot>> List(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<VMSnapshot>(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/snapshots",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<VMSnapshot>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<VMSnapshot>>(response.Body).Values;
        }


        public static string CLIENT_API_VERSION = "2019-07-01";
    }
}
