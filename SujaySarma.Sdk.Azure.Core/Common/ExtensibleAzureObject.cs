using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// This is an extensible version of AzureObjectBase
    /// </summary>
    public sealed class ExtensibleAzureObject : AzureObjectBase
    {

        /// <summary>
        /// Get/set the value of a property. Properties already defined in 
        /// AzureObjectBase are not returned or set!
        /// </summary>
        /// <param name="propertyName">Name of the properties collection property to set</param>
        /// <returns>Value or NULL</returns>
        public object? this[string propertyName]
        {
            get
            {
                if (Properties.TryGetValue(propertyName, out object? value))
                {
                    return value;
                }

                if (OtherData.TryGetValue(propertyName, out JToken? token))
                {
                    return token.ToObject<object>();
                }

                return null;
            }

            set
            {
                if (value == null)
                {
                    if (Properties.ContainsKey(propertyName))
                    {
                        Properties.Remove(propertyName);
                    }
                    else if (OtherData.ContainsKey(propertyName))
                    {
                        OtherData.Remove(propertyName);
                    }

                    return;
                }

                if (Properties.ContainsKey(propertyName))
                {
                    Properties[propertyName] = value;
                }
                else if (OtherData.ContainsKey(propertyName))
                {
                    OtherData[propertyName] = JToken.FromObject(value);
                }

            }
        }


        /// <summary>
        /// Properties of the object
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, object?> Properties { get; set; } = new Dictionary<string, object?>();

        /// <summary>
        /// Any other data
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JToken> OtherData { get; set; } = new Dictionary<string, JToken>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExtensibleAzureObject() : base() { }
    }
}
