using System;

namespace SujaySarma.Sdk.Azure.AppService.Common
{
    /// <summary>
    /// Extensions functions
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// Validates the structure of a domain name. Used by the App Service Domain client
        /// </summary>
        /// <param name="domainName">Domain name to validate</param>
        /// <param name="supportedTlds">List of supported TLD names</param>
        /// <returns>True if domain name appears to be valid. FALSE if there is a problem.</returns>
        public static bool ValidateDomainName(string domainName, string[] supportedTlds)
        {
            string? possibleTld = GetPossibleTopLevelDomainName(domainName);
            if (possibleTld == null)
            {
                return false;
            }

            foreach (string tld in supportedTlds)
            {
                if (possibleTld == tld)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the components of a domain name. For example, it splits "foo.com" into [ "foo", "com" ]
        /// </summary>
        /// <param name="domainName">Domain name to split</param>
        /// <returns>Components of the domain name</returns>
        public static string[] GetDomainNameComponents(string domainName)
        {
            if (string.IsNullOrWhiteSpace(domainName))
            {
                return Array.Empty<string>();
            }

            // leave it as None because we can then cause empty segments to throw an error
            return domainName.ToLower().Split(new char[] { '.' }, StringSplitOptions.None);
        }

        /// <summary>
        /// Tries to determine the possible top level domain name of the provided domain
        /// </summary>
        /// <param name="domainName">Domain name to parse</param>
        /// <returns>The possible Tld. NULL if the domain name is not long enough</returns>
        public static string? GetPossibleTopLevelDomainName(string domainName)
        {
            string[] nameComponents = GetDomainNameComponents(domainName);

            // we do not allow sub-domains, meaning there can be a max of 2 '.'s in the Tld 
            // meaning 3 component elements
            if ((nameComponents.Length > 3) || (nameComponents.Length < 2))
            {
                return null;
            }

            return ((nameComponents.Length == 2) ? nameComponents[^1] : $"{nameComponents[1]}.{nameComponents[2]}");
        }
    }
}
