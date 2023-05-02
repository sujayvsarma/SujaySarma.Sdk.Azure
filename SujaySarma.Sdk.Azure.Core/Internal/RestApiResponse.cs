using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SujaySarma.Sdk.Azure.Internal
{
    /// <summary>
    /// Encapsulate the important information returned from the remote endpoint. 
    /// Used as the function return types for the methods in <see cref="RestApiClient"/>.
    /// </summary>
    public class RestApiResponse
    {

        #region Properties

        /// <summary>
        /// The HTTP status
        /// </summary>
        public int HttpStatus { get; set; }

        /// <summary>
        /// The body content of the response. Could be an error
        /// </summary>
        public string? Body { get; set; } = null;

        /// <summary>
        /// Collection of headers received in response
        /// </summary>
        public HttpResponseHeaders? Headers { get; } = null;

        /// <summary>
        /// For each API, there is a set of expected success response codes. This property is set to 
        /// TRUE if the returned HttpStatus is one of those response codes.
        /// </summary>
        public bool IsExpectedSuccess { get; set; } = false;

        /// <summary>
        /// Set to TRUE if there was a higher level Exception thrown
        /// </summary>
        public bool WasException { get; set; } = false;

        /// <summary>
        /// The exception message (only set if WasException is true)
        /// </summary>
        public string? ExceptionMessage { get; set; } = null;

        #endregion

        /// <summary>
        /// Dummy
        /// </summary>
        public RestApiResponse() { }

        /// <summary>
        /// Import a HttpResponseMessage
        /// </summary>
        /// <param name="httpResponse">The HttpResponseMessage</param>
        /// <param name="expectedSuccessCodes">HTTP status codes that mean success for this API</param>
        public RestApiResponse(HttpResponseMessage httpResponse, IEnumerable<int>? expectedSuccessCodes)
        {
            HttpStatus = (int)httpResponse.StatusCode;
            Headers = httpResponse.Headers;
            IsExpectedSuccess = false;

            if (expectedSuccessCodes != null)
            {
                foreach (int code in expectedSuccessCodes)
                {
                    if (code == HttpStatus)
                    {
                        IsExpectedSuccess = true;
                    }
                }
            }

            Body = httpResponse.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Import an Exception
        /// </summary>
        /// <param name="exception">Exception that was thrown</param>
        public RestApiResponse(Exception exception)
        {
            WasException = true;
            ExceptionMessage = exception.Message;
        }

    }
}
