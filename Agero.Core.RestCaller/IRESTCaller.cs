using Agero.Core.RestCaller.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agero.Core.RestCaller
{
    /// <summary>REST client to make REST calls</summary>
    public interface IRestCaller
    {
        /// <summary>Makes request based on httpMethod</summary>
        /// <param name="uri">Request URL</param>
        /// <param name="httpMethod">HTTP method (GET, POST etc)</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="httpHeaders">Request headers</param>
        /// <param name="body">Request body</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="ignoreCertificateErrors">OBSOLETED AND IGNORED: Ignores SSL Certificate error if its true</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RestCallerException">Thrown when any error happened.</exception>
        RestCallerResponse MakeRequest(Uri uri, string httpMethod, IReadOnlyDictionary<string, string> parameters = null, IReadOnlyDictionary<string, string> httpHeaders = null,
            string body = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", int timeout = 1 * 60 * 1000,
            bool ignoreCertificateErrors = false, bool compressBody = false, int maxAttempts = 1);

        /// <summary>Makes request based on httpMethod</summary>
        /// <param name="uri">Request URL</param>
        /// <param name="httpMethod">HTTP method (GET, POST etc)</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="httpHeaders">Request headers</param>
        /// <param name="body">Request body</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="ignoreCertificateErrors">OBSOLETED AND IGNORED: Ignores SSL Certificate error if its true</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RestCallerException">Thrown when any error happened.</exception>
        Task<RestCallerResponse> MakeRequestAsync(Uri uri, string httpMethod, IReadOnlyDictionary<string, string> parameters = null, IReadOnlyDictionary<string, string> httpHeaders = null,
            string body = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", int timeout = 1 * 60 * 1000,
            bool ignoreCertificateErrors = false, bool compressBody = false, int maxAttempts = 1);
    }
}
