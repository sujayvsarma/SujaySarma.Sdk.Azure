using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;
using SujaySarma.Sdk.Azure.Compute.VirtualMachines;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    /// <summary>
    /// Client to interact with the virtual machines endpoint
    /// </summary>
    public static class VirtualMachineClient
    {

        /// <summary>
        /// Gets details about a single VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="expandInstanceView">If set, the InstanceView structure is also populated. Else (default) this will be NULL.</param>
        /// <returns>VM or NULL</returns>
        public static async Task<VirtualMachine?> Get(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, bool expandInstanceView = false)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await Get(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}", expandInstanceView);
        }

        /// <summary>
        /// Gets details about a single VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <param name="expandInstanceView">If set, the InstanceView structure is also populated. Else (default) this will be NULL.</param>
        /// <returns>Virtual Machine or NULL</returns>
        public static async Task<VirtualMachine?> Get(string bearerToken, string virtualMachineResourceUri, bool expandInstanceView = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (expandInstanceView)
            {
                parameters.Add("expand", "instanceView");
            }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}",
                    CLIENT_API_VERSION,
                    parameters, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VirtualMachine>(response.Body);
        }

        /// <summary>
        /// List all VMs in the subscription
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <returns>List of virtual machines</returns>
        public static async Task<List<VirtualMachine>> ListAll(string bearerToken, Guid subscription)
        {

            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }

            return await ListInternal(bearerToken, $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/providers/Microsoft.Compute/virtualMachines");
        }

        /// <summary>
        /// List all VMs in a resource group
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VMs resides in</param>
        /// <returns>List of virtual machines</returns>
        public static async Task<List<VirtualMachine>> ListByResourceGroup(string bearerToken, Guid subscription, string resourceGroupName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }

            return await ListInternal(bearerToken, $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines");
        }

        /// <summary>
        /// List all VMs in a location
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="locationName">Location internal / short name (eg: "westus")</param>
        /// <returns>List of virtual machines</returns>
        public static async Task<List<VirtualMachine>> ListByLocation(string bearerToken, Guid subscription, string locationName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(locationName)) { throw new ArgumentNullException(nameof(locationName)); }

            return await ListInternal(bearerToken, $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/providers/Microsoft.Compute/locations/{locationName}/virtualMachines");
        }

        /// <summary>
        /// Capture a VM into a template. VM must be stopped-deallocated before running this command, or, it will fail!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>Resource Id of the created template. If you get string.Empty, request is still pending, call again to get the resource Id</returns>
        public static async Task<string?> CreateTemplate(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await CreateTemplate(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Capture a VM into a template. VM must be stopped-deallocated before running this command, or, it will fail!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>Resource Id of the created template. If you get string.Empty, request is still pending, call again to get the resource Id</returns>
        public static async Task<string?> CreateTemplate(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            VMCaptureRequest request = new VMCaptureRequest()
            {
                DestinationContainerName = DateTime.UtcNow.Date.ToString("yyyyMMdd_HHmm"),
                ShouldOverwriteVhds = true,
                VhdFileNamePrefix = string.Empty
            };

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/capture",
                    CLIENT_API_VERSION,
                    null,
                    request,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return null;
            }

            if ((response.HttpStatus == 200) && (!string.IsNullOrWhiteSpace(response.Body)))
            {
                VMCaptureResult result = JsonConvert.DeserializeObject<VMCaptureResult>(response.Body);
                return result.ResourceId;
            }

            // When we get HTTP 202, the request will be completed async.
            return string.Empty;
        }

        /// <summary>
        /// Convert blob-stored disks to Managed Disks. VM must be stopped-deallocated before running this command, or, it will fail!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>Resource Id of the created template. If you get string.Empty, request is still pending, call again to get the resource Id</returns>
        public static async Task<bool> ConvertToManagedDisks(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await ConvertToManagedDisks(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Convert blob-stored disks to Managed Disks. VM must be stopped-deallocated before running this command, or, it will fail!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>Resource Id of the created template. If you get string.Empty, request is still pending, call again to get the resource Id</returns>
        public static async Task<bool> ConvertToManagedDisks(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/convertToManagedDisks",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Generalize the VM by removing the instance-specific data. The VM should be in stopped-deallocated state 
        /// before calling this, or it will FAIL!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Generalize(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await Generalize(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Generalize the VM by removing the instance-specific data. The VM should be in stopped-deallocated state 
        /// before calling this, or it will FAIL!
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Generalize(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/generalize",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a virtual machine. 
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="virtualMachineSettings">Settings of the VM to create or update. Note that not all settings work!</param>
        /// <returns>Virtual Machine object. NULL if there was a problem</returns>
        public static async Task<VirtualMachine?> Create(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName,
            VirtualMachine virtualMachineSettings)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }
            if (virtualMachineSettings == null) { throw new ArgumentNullException(nameof(virtualMachineSettings)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}",
                    CLIENT_API_VERSION,
                    null, virtualMachineSettings,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VirtualMachine>(response.Body);
        }

        /// <summary>
        /// Update a new virtual machine. 
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachine">Settings of the VM to update. Note that not all settings work!</param>
        /// <returns>Virtual Machine object. NULL if there was a problem</returns>
        public static async Task<VirtualMachine?> Update(string bearerToken, VirtualMachine virtualMachine)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (virtualMachine == null) { throw new ArgumentNullException(nameof(virtualMachine)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachine.ResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null, virtualMachine,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VirtualMachine>(response.Body);
        }

        /// <summary>
        /// Stop and deallocate the VM. You will no longer be billed for this VM and its resources
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Deallocate(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await Deallocate(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Stop and deallocate the VM. You will no longer be billed for this VM and its resources
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Deallocate(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/deallocate",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The VM and its child resources are deleted from Azure.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await Delete(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// The VM and its child resources are deleted from Azure.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Delete(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the running instance view of a virtual machine
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>InstanceView or NULL</returns>
        public static async Task<VMInstanceView?> GetRunningView(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await GetRunningView(bearerToken, $"/subscriptions/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Get the running instance view of a virtual machine
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>InstanceView or NULL</returns>
        public static async Task<VMInstanceView?> GetRunningView(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/instanceView",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VMInstanceView>(response.Body);
        }

        /// <summary>
        /// Perform maintenance on a VM. The VM may be restarted
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> PerformMaintenance(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await PerformMaintenance(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Perform maintenance on a VM. The VM may be restarted
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> PerformMaintenance(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/performMaintenance",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Power off a VM, typically by shutting down its OS and then turning off the VM.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="skipShutdown">If TRUE, then the VM is turned off without shutting down the OS first</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> PowerOff(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, bool skipShutdown = false)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await PowerOff(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}", skipShutdown);
        }

        /// <summary>
        /// Power off a VM, typically by shutting down its OS and then turning off the VM.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <param name="skipShutdown">If TRUE, then the VM is turned off without shutting down the OS first</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> PowerOff(string bearerToken, string virtualMachineResourceUri, bool skipShutdown = false)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/powerOff",
                    CLIENT_API_VERSION,
                    new Dictionary<string, string>()
                    {
                        { "skipShutdown", (skipShutdown ? "true" : "false") }
                    },
                    null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Shutdown the VM, move it to a new node and turn it ON.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="skipShutdown">If TRUE, then the VM is turned off without shutting down the OS first</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Redeploy(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await Redeploy(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Shutdown the VM, move it to a new node and turn it ON.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Redeploy(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/redeploy",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reimages the VM from its original source. "Factory reset"
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="skipShutdown">If TRUE, then the VM is turned off without shutting down the OS first</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> ReImage(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await ReImage(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Reimages the VM from its original source. "Factory reset"
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> ReImage(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/reimage",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reboot a VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="skipShutdown">If TRUE, then the VM is turned off without shutting down the OS first</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Restart(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await Restart(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Reboot a VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Restart(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/restart",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Boot up a stopped VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="skipShutdown">If TRUE, then the VM is turned off without shutting down the OS first</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Start(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await Start(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}");
        }

        /// <summary>
        /// Boot up a stopped VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<bool> Start(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/start",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Run a command on a VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Subscription Guid</param>
        /// <param name="resourceGroupName">Resource group the VM resides in</param>
        /// <param name="virtualMachineName">Name of the virtual machine</param>
        /// <param name="skipShutdown">If TRUE, then the VM is turned off without shutting down the OS first</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<List<string>> ExecuteCommand(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName,
            VMCommandTypesEnum type = VMCommandTypesEnum.ifconfig, string? command = null, Dictionary<string, string>? commandParameters = null)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await ExecuteCommand(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}",
                type, command, commandParameters);
        }

        /// <summary>
        /// Run a command on a VM
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the VM</param>
        /// <returns>TRUE if the request was accepted. It will complete async.</returns>
        public static async Task<List<string>> ExecuteCommand(string bearerToken, string virtualMachineResourceUri,
            VMCommandTypesEnum type = VMCommandTypesEnum.ifconfig, string? command = null, Dictionary<string, string>? commandParameters = null)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            Type t_VMCTE = typeof(VMCommandTypesEnum);
            string commandName = (Enum.GetName(typeof(VMCommandTypesEnum), type) ?? string.Empty);
            if ((!Enum.IsDefined(t_VMCTE, type)) || string.IsNullOrEmpty(commandName))
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            if ((type != VMCommandTypesEnum.ifconfig) && string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentNullException($"{nameof(command)} must be provided if {nameof(type)} is not 'ifconfig'.");
            }

            List<RunCommandParameter> parameters = new List<RunCommandParameter>();
            if ((commandParameters != null) && (commandParameters.Count > 0))
            {
                foreach (string key in commandParameters.Keys)
                {
                    parameters.Add(new RunCommandParameter()
                    {
                        Name = key,
                        Value = commandParameters[key]
                    });
                }
            }

            RunCommand runCommand = new RunCommand()
            {
                Command = commandName,
                Script = new List<string>()
                {
                    command ?? "echo \"hello world\""
                },
                Parameters = parameters
            };

            RestApiResponse response = await RestApiClient.POST(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/runCommand",
                    CLIENT_API_VERSION,
                    null, runCommand,
                    new int[] { 200, 202 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<string>();
            }

            RunCommandResult result = JsonConvert.DeserializeObject<RunCommandResult>(response.Body);
            if (result.Status == null)
            {
                return new List<string>();
            }

            List<string> logLines = new List<string>();
            foreach (InstanceViewStatus status in result.Status)
            {
                logLines.Add($"[{status.Level}]: {status.Label}. {status.Message}");
            }

            return logLines;
        }


        // helper function for the List*() methods
        private static async Task<List<VirtualMachine>> ListInternal(string bearerToken, string queryUri)
        {
            // we are not validating any parameters because this is a private call

            RestApiResponse response = await RestApiClient.GET(
                    bearerToken,
                    queryUri,
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<VirtualMachine>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<VirtualMachine>>(response.Body).Values;
        }


        public static string CLIENT_API_VERSION = "2019-03-01";
    }
}
