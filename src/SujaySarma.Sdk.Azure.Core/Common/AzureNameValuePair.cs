using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// This is a weird form of an Azure "dictionary" where there is an object with just two elements, 
    /// one called Name, containing the "key" and another called Value containing the "value"
    /// </summary>
    public class AzureNameValuePair
    {

        /// <summary>
        /// Name of the item
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Value of the item
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AzureNameValuePair() { }

        /// <summary>
        /// Create an instance
        /// </summary>
        /// <param name="name">Name of the key</param>
        /// <param name="value">Value</param>
        public AzureNameValuePair(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Returns a KeyValuePair equivalent of this object
        /// </summary>
        /// <returns>KeyValuePair</returns>
        public KeyValuePair<string, string> ToKeyValuePair() => new KeyValuePair<string, string>(Name, Value);

        /// <summary>
        /// Returns a Dictionary equivalent of a collection of name/value pair objects
        /// </summary>
        /// <param name="nameValuePairs">A collection of AzureNameValuePair objects to turn into a dictionary</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> ToDictionary(IEnumerable<AzureNameValuePair> nameValuePairs)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            foreach (AzureNameValuePair pair in nameValuePairs)
            {
                results.Add(pair.Name, pair.Value);
            }

            return results;
        }

        /// <summary>
        /// Add or set the value
        /// </summary>
        /// <param name="nameValuePairs">Collection of namevalue objects</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value to set</param>
        public static void AddOrSet(IList<AzureNameValuePair> nameValuePairs, string key, string value)
        {
            foreach (AzureNameValuePair pair in nameValuePairs)
            {
                if (pair.Name == key)
                {
                    pair.Value = value;
                    return;
                }
            }

            nameValuePairs.Add(new AzureNameValuePair(key, value));
        }
    }
}
