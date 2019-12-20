using System;
using System.Collections.Generic;
using System.Text;

namespace SujaySarma.Sdk.Azure.Common.Common
{

    /// <summary>
    /// Do stuff with Resource URI strings
    /// </summary>
    public static class ResourceUri
    {

        /// <summary>
        /// Create a resource Uri from the provided components
        /// </summary>
        /// <param name="subscription">Guid of the subscription</param>
        /// <param name="resourceGroupName">Name of resource group containing the leaf resource</param>
        /// <param name="providerName">Name of the provider</param>
        /// <param name="type">Type of object</param>
        /// <param name="objectName">Name of the leaf-resource</param>
        /// <returns>Absolute URI string</returns>
        public static string CreateResourceUri(Guid subscription, string? resourceGroupName = null, string? providerName = null, string? type = null, string? objectName = null)
        {
            // test & cleanup
            providerName = (string.IsNullOrWhiteSpace(providerName) ? null : providerName.Trim());
            type = (string.IsNullOrWhiteSpace(type) ? null : type.Trim());
            resourceGroupName = (string.IsNullOrWhiteSpace(resourceGroupName) ? null : resourceGroupName.Trim());
            objectName = (string.IsNullOrWhiteSpace(objectName) ? null : objectName.Trim());


            StringBuilder uri = new StringBuilder();

            uri.Append("/subscriptions/").Append(subscription.ToString("d"));

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
        /// Split a resource Uri to its components
        /// </summary>
        /// <param name="resourceUri">Absolute Uri string</param>
        /// <returns>Components as a dictionary. Keys are the constants defined as COMPONENT_* in this class.</returns>
        public static Dictionary<string, string> GetComponents(string resourceUri)
        {
            if (string.IsNullOrWhiteSpace(resourceUri))
            {
                throw new ArgumentNullException(nameof(resourceUri));
            }

            Dictionary<string, string> components = new Dictionary<string, string>();
            string[] pieces = resourceUri.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];
                switch (piece.ToLower())
                {
                    case "subscriptions":
                        components.Add(COMPONENT_SUBSCRIPTIONID, Guid.Parse(pieces[++i]).ToString("d"));
                        break;

                    case "resourcegroups":
                        components.Add(COMPONENT_RESOURCEGROUPNAME, pieces[++i]);
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
                        components.Add(COMPONENT_PROVIDERNAME, providerName.ToString());
                        break;

                    default:
                        if (i == (pieces.Length - 2))
                        {
                            components.Add(COMPONENT_TYPE, pieces[i++]);
                            components.Add(COMPONENT_RESOURCENAME, pieces[i]);
                        }
                        break;
                }
            }

            return components;
        }



        public static readonly string COMPONENT_SUBSCRIPTIONID = "subscriptionId";
        public static readonly string COMPONENT_RESOURCEGROUPNAME = "resourceGroupName";
        public static readonly string COMPONENT_PROVIDERNAME = "providerName";
        public static readonly string COMPONENT_TYPE = "type";
        public static readonly string COMPONENT_RESOURCENAME = "resourceName";
    }
}
