using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SujaySarma.Sdk.Azure.MarketPlace.Catalogs
{
    /// <summary>
    /// This is a category or subsection hierarchically under the <seealso cref="IndexCatalog"/>. 
    /// (Examples: "Windows", "Applications", etc)
    /// </summary>
    public class IndexCatalogCategory
    {

        /// <summary>
        /// The ID of this group
        /// </summary>
        [JsonProperty("submenuId")]
        public string GroupId { get; set; } = string.Empty;

        /// <summary>
        /// A user-displayable name for this group
        /// </summary>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    IEnumerable<string> words = Regex.Matches(GroupId, @"([A-Z][a-z]+)").Cast<Match>().Select(m => m.Value);
                    if (words.Count() <= 1)
                    {
                        return $"{char.ToUpper(GroupId[0])}{GroupId[1..].ToLower()}";
                    }

                    StringBuilder result = new StringBuilder();
                    foreach (string word in words)
                    {
                        if (result.Length > 0)
                        {
                            result.Append(" ");
                        }

                        result.Append(char.ToUpper(word[0])).Append(word[1..].ToLower());
                    }

                    _name = result.ToString();
                }

                return _name;
            }
        }
        private string? _name = null;

        /// <summary>
        /// Items in this group
        /// </summary>
        [JsonProperty("items")]
        public List<IndexItem> Items { get; set; } = new List<IndexItem>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public IndexCatalogCategory() { }

    }
}
