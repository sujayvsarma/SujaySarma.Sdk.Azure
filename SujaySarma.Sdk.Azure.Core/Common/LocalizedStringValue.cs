using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Common
{
    /// <summary>
    /// A string that has an English value as well as something that might 
    /// be localized. Sometimes both values are identical or one may be missing.
    /// </summary>
    /// <remarks>This class is only present in results and never used in data sent TO an API</remarks>
    public class LocalizedStringValue
    {

        /// <summary>
        /// The value name in English language
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// The value name localized
        /// </summary>
        [JsonProperty("localizedValue")]
        public string? Localized { get; set; } = null;


        public LocalizedStringValue() { }

    }
}
