using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Core.Cors
{
    /// <summary>
    /// A Cross-Origin Policy setting
    /// </summary>
    public class CorsPolicy
    {
        /// <summary>
        /// A list of allowed origins. Set a single element with "*" to allow all origins
        /// </summary>
        [JsonProperty("allowedOrigins")]
        public List<string> AllowedOrigins { get; set; } = new List<string>();

        /// <summary>
        /// Set if CORS requests containing credentials are to be allowed. 
        /// See https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS#Requests_with_credentials for more info.
        /// </summary>
        [JsonProperty("supportCredentials")]
        public bool IsCorsRequestsWithCredentialsAllowed { get; set; } = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CorsPolicy() { }

        /// <summary>
        /// Allow all origins (clears the current list)
        /// </summary>
        public void AllowAllOrigins() => AllowedOrigins = new List<string>() { "*" };

        /// <summary>
        /// Adds one or more origins
        /// </summary>
        /// <param name="originAddress">Addresses of the origin</param>
        /// <returns>CorsPolicy</returns>
        public CorsPolicy WithOrigins(params string[] originAddress)
        {
            if (AllowedOrigins == null)
            {
                AllowedOrigins = new List<string>();
            }

            if (!AllowedOrigins.Contains("*"))
            {
                foreach (string newOrigin in originAddress)
                {
                    if (!AllowedOrigins.Contains(newOrigin))
                    {
                        AllowedOrigins.Add(newOrigin);
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Same as calling AllowAllOrigins
        /// </summary>
        /// <returns>CorsPolicy</returns>
        public CorsPolicy WithAllOrigins()
        {
            AllowAllOrigins();
            return this;
        }

        /// <summary>
        /// Set credentials allowed
        /// </summary>
        /// <returns>CorsPolicy</returns>
        public CorsPolicy WithCredentialsAllowed()
        {
            IsCorsRequestsWithCredentialsAllowed = true;
            return this;
        }
    }
}
