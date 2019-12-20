using Microsoft.IdentityModel.Clients.ActiveDirectory;

using System;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.AuthZ
{
    /// <summary>
    /// Authenticate with Azure and fetch authentication tokens
    /// </summary>
    public static class Authentication
    {

        /// <summary>
        /// Get the Bearer token to authorize the application with Azure
        /// </summary>
        /// <param name="tenantName">Name or Guid of the tenant</param>
        /// <param name="clientId">The application's Client ID as registered in the Azure Active Directory</param>
        /// <param name="clientSecret">The application's Client Secret from the Azure Active Directory</param>
        /// <param name="resourceDomain">The domain to get the token for</param>
        /// <returns>The bearer token. NULL if there was a problem</returns>
        public static async Task<string?> GetBearerToken(string tenantName, string clientId, string clientSecret, string resourceDomain)
        {
            if (string.IsNullOrWhiteSpace(tenantName)) { throw new ArgumentNullException(nameof(tenantName)); }
            if (string.IsNullOrWhiteSpace(clientId)) { throw new ArgumentNullException(nameof(clientId)); }
            if (string.IsNullOrWhiteSpace(clientSecret)) { throw new ArgumentNullException(nameof(clientSecret)); }
            if (string.IsNullOrWhiteSpace(resourceDomain)) { throw new ArgumentNullException(nameof(resourceDomain)); }

            ClientCredential credential = new ClientCredential(clientId, clientSecret);
            AuthenticationContext context = new AuthenticationContext($"https://login.microsoftonline.com/{tenantName}", false);
            AuthenticationResult result;

            try
            {
                result = await context.AcquireTokenAsync(resourceDomain, credential);
            }
            catch
            {
                return null;
            }

            return result.AccessToken;
        }

    }
}
