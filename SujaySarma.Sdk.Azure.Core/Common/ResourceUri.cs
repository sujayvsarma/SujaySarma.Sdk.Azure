using Newtonsoft.Json;

using System;
using System.Text;

namespace SujaySarma.Sdk.Azure.Common
{

    /// <summary>
    /// A class to manipulate and create Azure RM resource Uri strings
    /// </summary>
    public class ResourceUri
    {

        /// <summary>
        /// Subscription's Guid
        /// </summary>
        [JsonProperty("subscription")]
        public Guid Subscription { get; set; }

        /// <summary>
        /// All Azure resources are part of a resource group, the name or Id of the containing group
        /// </summary>
        [JsonProperty("resourceGroupName")]
        public string? ResourceGroupName { get; set; }

        /// <summary>
        /// Name of the AzureRM provider that is used to work with the resource
        /// </summary>
        [JsonProperty("providerName")]
        public string? ProviderName { get; set; }

        /// <summary>
        /// The class/type of the resource within the provider's namespace
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Name of the final (leaf) resource
        /// </summary>
        [JsonProperty("resourceName")]
        public string? ResourceName { get; set; }

        /// <summary>
        /// Returns if the structure contains valid data that can be turned into an 
        /// AzureRM-valid resource URI
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (Subscription != default)
                {
                    if (!string.IsNullOrWhiteSpace(ResourceName))
                    {
                        if ((!string.IsNullOrWhiteSpace(Type)) && (!string.IsNullOrWhiteSpace(ProviderName)) && (!string.IsNullOrWhiteSpace(ResourceGroupName)))
                        {
                            return true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Type))
                    {
                        if ((!string.IsNullOrWhiteSpace(ProviderName)) && (!string.IsNullOrWhiteSpace(ResourceGroupName)))
                        {
                            return true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ProviderName))
                    {
                        if (!string.IsNullOrWhiteSpace(ResourceGroupName))
                        {
                            return true;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ResourceGroupName))
                    {
                        return true;
                    }

                    return true;
                }

                return false;
            }
        }


        /// <summary>
        /// Create a new resource Uri
        /// </summary>
        public ResourceUri()
        {
            Subscription = Guid.Empty;
            ResourceGroupName = null;
            ProviderName = null;
            Type = null;
            ResourceName = null;
        }

        /// <summary>
        /// Create a new resource Uri
        /// </summary>
        /// <param name="subscriptionId">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of the resource group, can also be the Guid</param>
        /// <param name="providerName">Name of the provider. In case of nested providers, include the complete provider namespace in the single string</param>
        /// <param name="typeName">The name of the class/type of the resource. In case of nested types, include the complete type string in the single string</param>
        /// <param name="resourceName">Name of the leaf resource node (eg: name of the VM or name of the domain, etc)</param>
        public ResourceUri(Guid subscriptionId, string? resourceGroupName = null, string? providerName = null, string? typeName = null, string? resourceName = null)
        {
            Subscription = subscriptionId;
            ResourceGroupName = resourceGroupName;
            ProviderName = providerName;
            Type = typeName;
            ResourceName = resourceName;
        }

        /// <summary>
        /// Create a resource Uri by importing an existing string
        /// </summary>
        /// <param name="resourceUriString">The existing resource Uri string</param>
        public ResourceUri(string resourceUriString)
        {
            if (string.IsNullOrWhiteSpace(resourceUriString)) { throw new ArgumentNullException(nameof(resourceUriString)); }
            string[] pieces = resourceUriString.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];
                switch (piece.ToLower())
                {
                    case "subscriptions":
                        Subscription = Guid.Parse(pieces[++i]);
                        break;

                    case "resourcegroups":
                        ResourceGroupName = pieces[++i];
                        break;

                    case "providers":
                        // providers can be nested. So eat up all the pieces except the last two
                        StringBuilder providerName = new StringBuilder();
                        while (i < (pieces.Length - 3))
                        {
                            if (providerName.Length > 0)
                            {
                                providerName.Append("/");
                            }
                            providerName.Append(pieces[++i]);
                        }

                        ProviderName = providerName.ToString();
                        break;

                    default:
                        if (i == (pieces.Length - 2))
                        {
                            Type = pieces[i++];
                            ResourceName = pieces[i];
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the subscription Id
        /// </summary>
        /// <param name="subscriptionId">Guid of the subscription</param>
        /// <returns>ResourceUri</returns>
        public ResourceUri WithSubscriptionId(Guid subscriptionId)
        {
            Subscription = subscriptionId;
            return this;
        }

        /// <summary>
        /// Sets the resource group name
        /// </summary>
        /// <param name="resourceGroupNameOrId">Name or Guid of the resource group</param>
        /// <returns>ResourceUri</returns>
        public ResourceUri WithResourceGroup(string resourceGroupNameOrId)
        {
            ResourceGroupName = resourceGroupNameOrId;
            return this;
        }

        /// <summary>
        /// Sets the resource group name
        /// </summary>
        /// <param name="resourceGroupId">Guid of the resource group</param>
        /// <returns>ResourceUri</returns>
        public ResourceUri WithResourceGroup(Guid resourceGroupId)
        {
            ResourceGroupName = resourceGroupId.ToString("d");
            return this;
        }

        /// <summary>
        /// Sets the provider name
        /// </summary>
        /// <param name="providerName">Name of the provider</param>
        /// <returns>ResourceUri</returns>
        public ResourceUri WithProvider(string providerName)
        {
            ProviderName = providerName;
            return this;
        }

        /// <summary>
        /// Sets the type of resource
        /// </summary>
        /// <param name="type">Type of resource</param>
        /// <returns>ResourceUri</returns>
        public ResourceUri WithType(string type)
        {
            Type = type;
            return this;
        }

        /// <summary>
        /// Sets the resource name
        /// </summary>
        /// <param name="resourceName">Name of the resource</param>
        /// <returns>ResourceUri</returns>
        public ResourceUri WithResource(string resourceName)
        {
            ResourceName = resourceName;
            return this;
        }

        /// <summary>
        /// Validates that all components are present correctly
        /// </summary>
        /// <returns>ResourceUri</returns>
        public ResourceUri Build()
        {
            if (!IsValid)
            {
                throw new Exception("ResourceUri does not contain valid components to create a valid AzureRM resource Uri string.");
            }

            return this;
        }

        /// <summary>
        /// Creates an accurate AzureRM resource Uri string
        /// </summary>
        /// <returns>AzureRM resource Uri String</returns>
        public override string ToString()
        {
            if (!IsValid)
            {
                throw new Exception("ResourceUri does not contain valid components to create a valid AzureRM resource Uri string.");
            }

            // test & cleanup
            string? providerName = (string.IsNullOrWhiteSpace(ProviderName) ? null : ProviderName.Trim());
            string? type = (string.IsNullOrWhiteSpace(Type) ? null : Type.Trim());
            string? resourceGroupName = (string.IsNullOrWhiteSpace(ResourceGroupName) ? null : ResourceGroupName.Trim());
            string? objectName = (string.IsNullOrWhiteSpace(ResourceName) ? null : ResourceName.Trim());

            StringBuilder uri = new StringBuilder();

            uri.Append("/subscriptions/").Append(Subscription.ToString("d"));

            if (resourceGroupName != null)
            {
                uri.Append("/resourceGroups/").Append(resourceGroupName);
            }

            if (providerName != null)
            {
                uri.Append("/providers/").Append(providerName);
            }

            if (type != null)
            {
                uri.Append("/").Append(type);
            }

            if (objectName != null)
            {
                uri.Append("/").Append(objectName);
            }

            return uri.ToString();
        }

        /// <summary>
        /// To an absolute URI that can be used with Azure RM endpoints 
        /// </summary>
        /// <param name="endpointName">Name of the AzureRM endpoint name (appended to the endpoint)</param>
        /// <returns>An absolute URL of the form 'https://management.azure.com/(resourceUri)/(endpointName)'</returns>
        public string ToAbsoluteAzureRMEndpointUri(string? endpointName = null)
            => $"https://management.azure.com{ToString()}{(string.IsNullOrWhiteSpace(endpointName) ? "" : "/" + endpointName)}";

        /// <summary>
        /// Checks if the provided component is the value provided
        /// </summary>
        /// <param name="componentToCompare">The component of the ResourceUri to compare</param>
        /// <param name="value">The value the component should be</param>
        /// <returns>TRUE if the component matches the value provided. FALSE (including if the component is NULL)</returns>
        public bool Is(ResourceUriCompareLevel componentToCompare, string value)
        => componentToCompare switch
        {
            ResourceUriCompareLevel.ResourceName => (ResourceName != null) && ResourceName.Equals(value, StringComparison.InvariantCultureIgnoreCase),
            ResourceUriCompareLevel.Type => (Type != null) && Type.Equals(value, StringComparison.InvariantCultureIgnoreCase),
            ResourceUriCompareLevel.Provider => (ProviderName != null) && ProviderName.Equals(value, StringComparison.InvariantCultureIgnoreCase),
            ResourceUriCompareLevel.ResourceGroup => (ResourceGroupName != null) && ResourceGroupName.Equals(value, StringComparison.InvariantCultureIgnoreCase),
            ResourceUriCompareLevel.Subscription => (Subscription == Guid.Parse(value)),
            _ => false,
        };



        /// <summary>
        /// Compare the current instance with the provided instance and return if they match. 
        /// </summary>
        /// <param name="targetUri">The other ResourceUri object to compare with</param>
        /// <param name="level">What components to compare (multiple items can be OR'ed)</param>
        /// <returns>True if the specified components match</returns>
        /// <remarks>
        ///     We only compare the strings as they are provided in the two object instances. We cannot 
        ///     for instance determine that a NAME provided in one component is the same thing as a GUID 
        ///     provided in the other instance, though to AzureRM both will mean the same thing. This is 
        ///     especially true for the ResourceGroupName component that may be a Guid or a Name!
        /// </remarks>
        public bool Compare(ResourceUri targetUri, ResourceUriCompareLevel level)
        {
            int differences = 0;

            if (((level == ResourceUriCompareLevel.All) || level.HasFlag(ResourceUriCompareLevel.ResourceName)) && (ResourceName != null) && (!ResourceName.Equals(targetUri.ResourceName, StringComparison.InvariantCultureIgnoreCase)))
            {
                differences++;
            }

            if (((level == ResourceUriCompareLevel.All) || level.HasFlag(ResourceUriCompareLevel.Type)) && (Type != null) && (!Type.Equals(targetUri.Type, StringComparison.InvariantCultureIgnoreCase)))
            {
                differences++;
            }

            if (((level == ResourceUriCompareLevel.All) || level.HasFlag(ResourceUriCompareLevel.Provider)) && (ProviderName != null) && (!ProviderName.Equals(targetUri.ProviderName, StringComparison.InvariantCultureIgnoreCase)))
            {
                differences++;
            }

            if (((level == ResourceUriCompareLevel.All) || level.HasFlag(ResourceUriCompareLevel.ResourceGroup)) && (ResourceGroupName != null) && (!ResourceGroupName.Equals(targetUri.ResourceGroupName, StringComparison.InvariantCultureIgnoreCase)))
            {
                differences++;
            }

            if (((level == ResourceUriCompareLevel.All) || level.HasFlag(ResourceUriCompareLevel.Subscription)) && (Subscription != targetUri.Subscription))
            {
                differences++;
            }

            return (differences == 0);
        }
    }

    /// <summary>
    /// Level of comparison to perform
    /// </summary>
    [Flags]
    public enum ResourceUriCompareLevel
    {
        /// <summary>
        /// Compare everything
        /// </summary>
        All = 0,

        /// <summary>
        /// Compare only the leaf-resource names
        /// </summary>
        ResourceName = 1,

        /// <summary>
        /// Compare class/type of object
        /// </summary>
        Type = 2,

        /// <summary>
        /// Compare the resource provider IDs
        /// </summary>
        Provider = 4,

        /// <summary>
        /// Compare the resource group information
        /// </summary>
        ResourceGroup = 8,

        /// <summary>
        /// Compare the subscription Ids
        /// </summary>
        Subscription = 16
    }
}
