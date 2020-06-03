using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.Internal
{
    /// <summary>
    /// This is a specially modified version of our RestApiClient class. Handles the actual calls to the 
    /// REST API and returns the relevant responses. Most of our methods should be threadsafe. 
    /// </summary>
    public static class RestApiClient
    {

        /// <summary>
        /// Fire a GET request
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="requestBaseURI">The base URI for the request, without any parameters</param>
        /// <param name="apiVersionRequired">The API version requested</param>
        /// <param name="queryParameters">Query string parameters (do not add apiVersion!)</param>
        /// <param name="requestBodyObject">The object containing the data for the request body (this will be json'ized)</param>
        /// <param name="expectedSuccessCodes">HTTP success codes that would indicate successful execution</param>
        /// <returns>RestApiResponse structure containing the result</returns>
        public static async Task<RestApiResponse> GET(string bearerToken, string requestBaseURI, string apiVersionRequired, Dictionary<string, string>? queryParameters, object? requestBodyObject, IEnumerable<int>? expectedSuccessCodes)
        {
            StringBuilder requestURI = new StringBuilder(requestBaseURI);
            if (apiVersionRequired != string.Empty)
            {
                requestURI.Append("?api-version=").Append(apiVersionRequired);
            }

            if ((queryParameters != null) && (queryParameters.Count > 0))
            {
                foreach (string key in queryParameters.Keys)
                {
                    requestURI.Append("&")
                        .Append(key).Append("=").Append(Uri.EscapeDataString(queryParameters[key]));
                }
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestURI.ToString());
            if (requestBodyObject != null)
            {
                request.Content = new StringContent(((requestBodyObject is string str) ? str : JsonConvert.SerializeObject(requestBodyObject)));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            tryAgain:
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await client.SendAsync(request);
            }
            catch (Exception badEx) when (badEx.Message.Contains("No such host", StringComparison.InvariantCultureIgnoreCase) || badEx.Message.Contains("cancelled", StringComparison.InvariantCultureIgnoreCase))
            {
                goto tryAgain;
            }
            catch (Exception ex)
            {
                return new RestApiResponse(ex);
            }

            return new RestApiResponse(responseMessage, expectedSuccessCodes ?? new int[] { 200, 201, 202 });
        }

        /// <summary>
        /// Performs a GET without authentication. For use with some undocumented Azure APIs that seem not to expect auth tokens.
        /// </summary>
        /// <param name="requestBaseURI">The base URI for the request, without any parameters</param>
        /// <param name="apiVersionRequired">The API version requested</param>
        /// <param name="queryParameters">Query string parameters (do not add apiVersion!)</param>
        /// <param name="requestBodyObject">The object containing the data for the request body (this will be json'ized)</param>
        /// <param name="expectedSuccessCodes">HTTP success codes that would indicate successful execution</param>
        /// <param name="timeOutSeconds">Timeout in seconds</param>
        /// <returns>RestApiResponse structure containing the result</returns>
        public static async Task<RestApiResponse> GETWithoutAuthentication(string requestBaseURI, string? apiVersionRequired, Dictionary<string, string>? queryParameters, object? requestBodyObject, IEnumerable<int>? expectedSuccessCodes, int timeOutSeconds = 15)
        {
            StringBuilder requestURI = new StringBuilder(requestBaseURI);
            if (!string.IsNullOrWhiteSpace(apiVersionRequired))
            {
                requestURI.Append("?api-version=").Append(apiVersionRequired);
            }

            if ((queryParameters != null) && (queryParameters.Count > 0))
            {
                foreach (string key in queryParameters.Keys)
                {
                    requestURI.Append("&")
                        .Append(key).Append("=").Append(Uri.EscapeDataString(queryParameters[key]));
                }
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestURI.ToString());
            if (requestBodyObject != null)
            {
                request.Content = new StringContent(((requestBodyObject is string str) ? str : JsonConvert.SerializeObject(requestBodyObject)));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            tryAgain:
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(timeOutSeconds)
            };

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await client.SendAsync(request);
            }
            catch (Exception badEx) when (badEx.Message.Contains("No such host", StringComparison.InvariantCultureIgnoreCase) || badEx.Message.Contains("cancelled", StringComparison.InvariantCultureIgnoreCase))
            {
                goto tryAgain;
            }
            catch (Exception ex)
            {
                return new RestApiResponse(ex);
            }

            return new RestApiResponse(responseMessage, expectedSuccessCodes ?? new int[] { 200, 201, 202 });
        }

        /// <summary>
        /// Fire a GET request and follow it until all the continuations are fetched. Data from each fetch is intelligently 
        /// appended to the response's body string.
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="requestBaseURI">The base URI for the request, without any parameters</param>
        /// <param name="apiVersionRequired">The API version requested</param>
        /// <param name="queryParameters">Query string parameters (do not add apiVersion!)</param>
        /// <param name="requestBodyObject">The object containing the data for the request body (this will be json'ized)</param>
        /// <param name="expectedSuccessCodes">HTTP success codes that would indicate successful execution</param>
        /// <returns>RestApiResponse structure containing the result</returns>
        public static async Task<RestApiResponse> GETWithContinuations<T>(string bearerToken, string requestBaseURI, string apiVersionRequired, Dictionary<string, string>? queryParameters, object? requestBodyObject, IEnumerable<int>? expectedSuccessCodes)
        {
            StringBuilder requestURI = new StringBuilder(requestBaseURI);
            if (apiVersionRequired != string.Empty)
            {
                requestURI.Append("?api-version=").Append(apiVersionRequired);
            }
            if ((queryParameters != null) && (queryParameters.Count > 0))
            {
                foreach (string key in queryParameters.Keys)
                {
                    requestURI.Append("&")
                        .Append(key).Append("=").Append(Uri.EscapeDataString(queryParameters[key]));
                }
            }

            List<int> successCodes = expectedSuccessCodes?.ToList() ?? new List<int>() { 200, 201, 202 };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestURI.ToString());
            if (requestBodyObject != null)
            {
                request.Content = new StringContent(((requestBodyObject is string str) ? str : JsonConvert.SerializeObject(requestBodyObject)));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            tryAgain:
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);

            HttpResponseMessage responseMessage;
            List<T> returnableObjects = new List<T>();

            try
            {
                responseMessage = await client.SendAsync(request);
                if (responseMessage.IsSuccessStatusCode || successCodes.Contains((int)responseMessage.StatusCode))
                {
                    ListResultWithContinuations<T> result = JsonConvert.DeserializeObject<ListResultWithContinuations<T>>(await responseMessage.Content.ReadAsStringAsync());
                    if ((result.Values != null) && (result.Values.Count > 0))
                    {
                        returnableObjects.AddRange(result.Values);
                    }

                    while (!string.IsNullOrWhiteSpace(result.NextPage))
                    {
                        request = new HttpRequestMessage(HttpMethod.Get, result.NextPage);
                        responseMessage = await client.SendAsync(request);
                        if (responseMessage.IsSuccessStatusCode || successCodes.Contains((int)responseMessage.StatusCode))
                        {
                            result = JsonConvert.DeserializeObject<ListResultWithContinuations<T>>(await responseMessage.Content.ReadAsStringAsync());
                            if ((result.Values != null) && (result.Values.Count > 0))
                            {
                                returnableObjects.AddRange(result.Values);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception badEx) when (badEx.Message.Contains("No such host", StringComparison.InvariantCultureIgnoreCase) || badEx.Message.Contains("cancelled", StringComparison.InvariantCultureIgnoreCase))
            {
                goto tryAgain;
            }
            catch (Exception ex)
            {
                return new RestApiResponse(ex);
            }

            return new RestApiResponse()
            {
                WasException = false,
                IsExpectedSuccess = true,
                ExceptionMessage = null,
                HttpStatus = 200,
                Body = JsonConvert.SerializeObject(returnableObjects)
            };
        }

        /// <summary>
        /// Fire a POST request
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="requestBaseURI">The base URI for the request, without any parameters</param>
        /// <param name="apiVersionRequired">The API version requested</param>
        /// <param name="queryParameters">Query string parameters (do not add apiVersion!)</param>
        /// <param name="requestBodyObject">The object containing the data for the request body (this will be json'ized)</param>
        /// <param name="expectedSuccessCodes">HTTP success codes that would indicate successful execution</param>
        /// <returns>RestApiResponse structure containing the result</returns>
        public static async Task<RestApiResponse> POST(string bearerToken, string requestBaseURI, string apiVersionRequired, Dictionary<string, string>? queryParameters, object? requestBodyObject, IEnumerable<int>? expectedSuccessCodes)
        {
            StringBuilder requestURI = new StringBuilder(requestBaseURI);
            if (apiVersionRequired != string.Empty)
            {
                requestURI.Append("?api-version=").Append(apiVersionRequired);
            }
            if ((queryParameters != null) && (queryParameters.Count > 0))
            {
                foreach (string key in queryParameters.Keys)
                {
                    requestURI.Append("&")
                        .Append(key).Append("=").Append(Uri.EscapeDataString(queryParameters[key]));
                }
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestURI.ToString());
            if (requestBodyObject != null)
            {
                request.Content = new StringContent(((requestBodyObject is string str) ? str : JsonConvert.SerializeObject(requestBodyObject)));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            tryAgain:
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await client.SendAsync(request);
            }
            catch (Exception badEx) when (badEx.Message.Contains("No such host", StringComparison.InvariantCultureIgnoreCase) || badEx.Message.Contains("cancelled", StringComparison.InvariantCultureIgnoreCase))
            {
                goto tryAgain;
            }
            catch (Exception ex)
            {
                return new RestApiResponse(ex);
            }

            return new RestApiResponse(responseMessage, expectedSuccessCodes ?? new int[] { 200, 201, 202 });
        }

        /// <summary>
        /// Fire a HEAD request
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="requestBaseURI">The base URI for the request, without any parameters</param>
        /// <param name="apiVersionRequired">The API version requested</param>
        /// <param name="queryParameters">Query string parameters (do not add apiVersion!)</param>
        /// <param name="expectedSuccessCodes">HTTP success codes that would indicate successful execution</param>
        /// <returns>RestApiResponse structure containing the result</returns>
        public static async Task<RestApiResponse> HEAD(string bearerToken, string requestBaseURI, string apiVersionRequired, Dictionary<string, string>? queryParameters, IEnumerable<int>? expectedSuccessCodes)
        {
            StringBuilder requestURI = new StringBuilder(requestBaseURI);
            if (apiVersionRequired != string.Empty)
            {
                requestURI.Append("?api-version=").Append(apiVersionRequired);
            }
            if ((queryParameters != null) && (queryParameters.Count > 0))
            {
                foreach (string key in queryParameters.Keys)
                {
                    requestURI.Append("&")
                        .Append(key).Append("=").Append(Uri.EscapeDataString(queryParameters[key]));
                }
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, requestURI.ToString());

            tryAgain:
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await client.SendAsync(request);
            }
            catch (Exception badEx) when (badEx.Message.Contains("No such host", StringComparison.InvariantCultureIgnoreCase) || badEx.Message.Contains("cancelled", StringComparison.InvariantCultureIgnoreCase))
            {
                goto tryAgain;
            }
            catch (Exception ex)
            {
                return new RestApiResponse(ex);
            }

            return new RestApiResponse(responseMessage, expectedSuccessCodes ?? new int[] { 200, 201, 202, 204, 404 });
        }

        /// <summary>
        /// Fire a DELETE request
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="requestBaseURI">The base URI for the request, without any parameters</param>
        /// <param name="apiVersionRequired">The API version requested</param>
        /// <param name="queryParameters">Query string parameters (do not add apiVersion!)</param>
        /// <param name="expectedSuccessCodes">HTTP success codes that would indicate successful execution</param>
        /// <returns>RestApiResponse structure containing the result</returns>
        public static async Task<RestApiResponse> DELETE(string bearerToken, string requestBaseURI, string apiVersionRequired, Dictionary<string, string>? queryParameters, IEnumerable<int>? expectedSuccessCodes)
        {
            StringBuilder requestURI = new StringBuilder(requestBaseURI);
            if (apiVersionRequired != string.Empty)
            {
                requestURI.Append("?api-version=").Append(apiVersionRequired);
            }
            if ((queryParameters != null) && (queryParameters.Count > 0))
            {
                foreach (string key in queryParameters.Keys)
                {
                    requestURI.Append("&")
                        .Append(key).Append("=").Append(Uri.EscapeDataString(queryParameters[key]));
                }
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestURI.ToString());

            tryAgain:
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await client.SendAsync(request);
            }
            catch (Exception badEx) when (badEx.Message.Contains("No such host", StringComparison.InvariantCultureIgnoreCase) || badEx.Message.Contains("cancelled", StringComparison.InvariantCultureIgnoreCase))
            {
                goto tryAgain;
            }
            catch (Exception ex)
            {
                return new RestApiResponse(ex);
            }

            return new RestApiResponse(responseMessage, expectedSuccessCodes ?? new int[] { 200, 202 });
        }

        /// <summary>
        /// Fire a PUT request
        /// </summary>
        /// <param name="bearerToken">The authorization bearer token</param>
        /// <param name="requestBaseURI">The base URI for the request, without any parameters</param>
        /// <param name="apiVersionRequired">The API version requested</param>
        /// <param name="queryParameters">Query string parameters (do not add apiVersion!)</param>
        /// <param name="requestBodyObject">The object containing the data for the request body (this will be json'ized)</param>
        /// <param name="expectedSuccessCodes">HTTP success codes that would indicate successful execution</param>
        /// <returns>RestApiResponse structure containing the result</returns>
        public static async Task<RestApiResponse> PUT(string bearerToken, string requestBaseURI, string apiVersionRequired, Dictionary<string, string>? queryParameters, object? requestBodyObject, IEnumerable<int>? expectedSuccessCodes)
        {
            StringBuilder requestURI = new StringBuilder(requestBaseURI);
            if (apiVersionRequired != string.Empty)
            {
                requestURI.Append("?api-version=").Append(apiVersionRequired);
            }
            if ((queryParameters != null) && (queryParameters.Count > 0))
            {
                foreach (string key in queryParameters.Keys)
                {
                    requestURI.Append("&")
                        .Append(key).Append("=").Append(Uri.EscapeDataString(queryParameters[key]));
                }
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestURI.ToString());
            if (requestBodyObject != null)
            {
                request.Content = new StringContent(((requestBodyObject is string str) ? str : JsonConvert.SerializeObject(requestBodyObject)));
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            tryAgain:
            HttpClient client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15)
            };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await client.SendAsync(request);
            }
            catch (Exception badEx) when (badEx.Message.Contains("No such host", StringComparison.InvariantCultureIgnoreCase) || badEx.Message.Contains("cancelled", StringComparison.InvariantCultureIgnoreCase))
            {
                goto tryAgain;
            }
            catch (Exception ex)
            {
                return new RestApiResponse(ex);
            }

            return new RestApiResponse(responseMessage, expectedSuccessCodes ?? new int[] { 200, 201, 202 });
        }

    }
}
