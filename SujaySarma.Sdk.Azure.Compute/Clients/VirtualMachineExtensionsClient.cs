using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.VirtualMachines;
using SujaySarma.Sdk.Azure.Internal;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SujaySarma.Sdk.Azure.Compute.Clients
{
    /// <summary>
    /// Create, manage and use Virtual Machine Extensions using the AzureRM API endpoints
    /// </summary>
    public static class VirtualMachineExtensionsClient
    {

        /// <summary>
        /// Create/register a new extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the extension into</param>
        /// <param name="extensionName">Name of the extension</param>
        /// <param name="location">Azure data center geo location short/internal name (eg: "westus")</param>
        /// <param name="properties">Properties of the extension</param>
        /// <param name="tags">Tags (optional)</param>
        /// <returns>VMExtension object if successful, else NULL</returns>
        public static async Task<VMExtension?> Create(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, string extensionName,
            string location, VMExtensionProperties properties, Dictionary<string, string>? tags = null)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }
            if (string.IsNullOrWhiteSpace(extensionName)) { throw new ArgumentNullException(nameof(extensionName)); }
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }

            // avoid carrying junk around
            VMExtensionProperties requestProperties = new VMExtensionProperties()
            {
                ForceUpdateMode = properties.ForceUpdateMode,
                InstallNewerMinorVersionIfAvailable = properties.InstallNewerMinorVersionIfAvailable,
                InstanceView = null,
                ProtectedSettings = properties.ProtectedSettings,
                PublisherName = properties.PublisherName,
                Settings = properties.Settings,
                Type = properties.Type,
                TypeHandlerVersion = properties.TypeHandlerVersion
            };

            return await CreateOrUpdate(
                    bearerToken,
                    new VMExtension()
                    {
                        Location = location,
                        Name = extensionName,
                        Properties = requestProperties,
                        ResourceId = $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/extensions/{extensionName}",
                        Tags = tags,
                        Type = "virtualMachines/extensions"
                    }
                );
        }

        /// <summary>
        /// Create/register a new extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="extensionName">Name of the extension</param>
        /// <param name="location">Azure data center geo location short/internal name (eg: "westus")</param>
        /// <param name="properties">Properties of the extension</param>
        /// <param name="tags">Tags (optional)</param>
        /// <returns>VMExtension object if successful, else NULL</returns>
        public static async Task<VMExtension?> Create(string bearerToken, string virtualMachineResourceUri, string extensionName,
            string location, VMExtensionProperties properties, Dictionary<string, string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }
            if (string.IsNullOrWhiteSpace(extensionName)) { throw new ArgumentNullException(nameof(extensionName)); }
            if (string.IsNullOrWhiteSpace(location)) { throw new ArgumentNullException(nameof(location)); }
            if (properties == null) { throw new ArgumentNullException(nameof(properties)); }

            // avoid carrying junk around
            VMExtensionProperties requestProperties = new VMExtensionProperties()
            {
                ForceUpdateMode = properties.ForceUpdateMode,
                InstallNewerMinorVersionIfAvailable = properties.InstallNewerMinorVersionIfAvailable,
                InstanceView = null,
                ProtectedSettings = properties.ProtectedSettings,
                PublisherName = properties.PublisherName,
                Settings = properties.Settings,
                Type = properties.Type,
                TypeHandlerVersion = properties.TypeHandlerVersion
            };

            return await CreateOrUpdate(
                    bearerToken,
                    new VMExtension()
                    {
                        Location = location,
                        Name = extensionName,
                        Properties = requestProperties,
                        ResourceId = virtualMachineResourceUri,
                        Tags = tags,
                        Type = "virtualMachines/extensions"
                    }
                );
        }

        /// <summary>
        /// Create/register a new extension. If the extension is already present/registered, updates the registration with the 
        /// provided values.
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineExtension">The prepopulated Virtual Machine Extension object.</param>
        /// <returns>VMExtension object if successful, else NULL</returns>
        public static async Task<VMExtension?> CreateOrUpdate(string bearerToken, VMExtension virtualMachineExtension)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(virtualMachineExtension)); }
            if (virtualMachineExtension == null) { throw new ArgumentNullException(nameof(virtualMachineExtension)); }

            RestApiResponse response = await RestApiClient.PUT(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineExtension.ResourceId[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    virtualMachineExtension,
                    new int[] { 200, 201 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VMExtension>(response.Body);
        }

        /// <summary>
        /// Delete a virtual machine extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the extension into</param>
        /// <param name="extensionName">Name of the extension</param>
        /// <returns>True if the task was accepted successfully</returns>
        public static async Task<bool> Delete(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, string extensionName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }
            if (string.IsNullOrWhiteSpace(extensionName)) { throw new ArgumentNullException(nameof(extensionName)); }

            return await Delete(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/extensions/{extensionName}");
        }

        /// <summary>
        /// Delete a virtual machine extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the Virtual Machine</param>
        /// <param name="extensionName">Name of the extension</param>
        /// <returns>True if the task was accepted successfully</returns>
        public static async Task<bool> Delete(string bearerToken, string virtualMachineResourceUri, string extensionName)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }
            if (string.IsNullOrWhiteSpace(extensionName)) { throw new ArgumentNullException(nameof(extensionName)); }

            return await Delete(bearerToken, $"{virtualMachineResourceUri}/extensions/{extensionName}");
        }

        /// <summary>
        /// Delete a virtual machine extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineExtensionResourceUri">Resource Uri to the Virtual Machine Extension</param>
        /// <returns>True if the task was accepted successfully</returns>
        public static async Task<bool> Delete(string bearerToken, string virtualMachineExtensionResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineExtensionResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineExtensionResourceUri)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineExtensionResourceUri[1..]}",
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
        /// Get a virtual machine extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the extension into</param>
        /// <param name="extensionName">Name of the extension</param>
        /// <returns>Virtual machine extension or NULL</returns>
        public static async Task<VMExtension?> Get(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName, string extensionName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }
            if (string.IsNullOrWhiteSpace(extensionName)) { throw new ArgumentNullException(nameof(extensionName)); }

            return await Get(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/extensions/{extensionName}");
        }

        /// <summary>
        /// Get a virtual machine extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the Virtual Machine</param>
        /// <param name="extensionName">Name of the extension</param>
        /// <returns>Virtual machine extension or NULL</returns>
        public static async Task<VMExtension?> Get(string bearerToken, string virtualMachineResourceUri, string extensionName)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }
            if (string.IsNullOrWhiteSpace(extensionName)) { throw new ArgumentNullException(nameof(extensionName)); }

            return await Get(bearerToken, $"{virtualMachineResourceUri}/extensions/{extensionName}");
        }

        /// <summary>
        /// Get a virtual machine extension
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineExtensionResourceUri">Resource Uri to the Virtual Machine Extension</param>
        /// <returns>Virtual machine extension or NULL</returns>
        public static async Task<VMExtension?> Get(string bearerToken, string virtualMachineExtensionResourceUri)
        {
            if (string.IsNullOrWhiteSpace(bearerToken)) { throw new ArgumentNullException(nameof(bearerToken)); }
            if (string.IsNullOrWhiteSpace(virtualMachineExtensionResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineExtensionResourceUri)); }

            RestApiResponse response = await RestApiClient.DELETE(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineExtensionResourceUri[1..]}",
                    CLIENT_API_VERSION,
                    null,
                    new int[] { 200, 202, 204 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<VMExtension>(response.Body);
        }

        /// <summary>
        /// List all virtual machine extensions on a virtual machine
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Resource group (name) containing the virtual machine</param>
        /// <param name="virtualMachineName">Name of the virtual machine to create the extension into</param>
        /// <returns>List of virtual machine extension or empty list</returns>
        public static async Task<List<VMExtension>> List(string bearerToken, Guid subscription, string resourceGroupName, string virtualMachineName)
        {
            if (subscription == Guid.Empty) { throw new ArgumentNullException(nameof(subscription)); }
            if (string.IsNullOrWhiteSpace(resourceGroupName)) { throw new ArgumentNullException(nameof(resourceGroupName)); }
            if (string.IsNullOrWhiteSpace(virtualMachineName)) { throw new ArgumentNullException(nameof(virtualMachineName)); }

            return await List(bearerToken, $"/{subscription.ToString("d")}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/virtualMachines/{virtualMachineName}/extensions");
        }

        /// <summary>
        /// List all virtual machine extensions on a virtual machine
        /// </summary>
        /// <param name="bearerToken">Authorization bearer token</param>
        /// <param name="virtualMachineResourceUri">Resource Uri to the Virtual Machine</param>
        /// <returns>List of virtual machine extensions or NULL</returns>
        public static async Task<List<VMExtension>> List(string bearerToken, string virtualMachineResourceUri)
        {
            if (string.IsNullOrWhiteSpace(virtualMachineResourceUri)) { throw new ArgumentNullException(nameof(virtualMachineResourceUri)); }

            RestApiResponse response = await RestApiClient.GETWithContinuations<VMExtension>(
                    bearerToken,
                    $"https://management.azure.com/{virtualMachineResourceUri[1..]}/extensions",
                    CLIENT_API_VERSION,
                    null, null,
                    new int[] { 200 }
                );

            if ((!response.IsExpectedSuccess) || response.WasException || string.IsNullOrWhiteSpace(response.Body))
            {
                return new List<VMExtension>();
            }

            return JsonConvert.DeserializeObject<ListResultWithContinuations<VMExtension>>(response.Body).Values;
        }


        public static string CLIENT_API_VERSION = "2019-03-01";
    }
}
