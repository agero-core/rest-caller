using Agero.Core.Checker;
using Agero.Core.RestCaller.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agero.Core.RestCaller.Extensions
{
    /// <summary>REST Caller extensions</summary>
    public static class RESTCallerExtensions
    {
        /// <summary>Makes GET request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static RestCallerResponse Get(this IRESTCaller restCaller, Uri uri, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string accept = "application/json", int timeout = 1 * 60 * 1000,
            int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            return restCaller.MakeRequest(
                uri: uri,
                httpMethod: "GET",
                parameters: parameters,
                httpHeaders: headers,
                accept: accept,
                timeout: timeout,
                maxAttempts: maxAttempts);
        }

        /// <summary>Makes GET request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static async Task<RestCallerResponse> GetAsync(this IRESTCaller restCaller, Uri uri, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string accept = "application/json", int timeout = 1 * 60 * 1000, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            if (parameters != null && parameters.Any())
                uri = uri.ComposeUri(parameters);

            return await restCaller.MakeRequestAsync(
                uri: uri,
                httpMethod: "GET",
                parameters: parameters,
                httpHeaders: headers,
                accept: accept,
                timeout: timeout,
                maxAttempts: maxAttempts);
        }

        /// <summary>Makes POST request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="body">Request body</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static RestCallerResponse Post(this IRESTCaller restCaller, Uri uri, string body = null, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", 
            int timeout = 1 * 60 * 1000, bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            return restCaller.MakeRequest(
                uri: uri,
                httpMethod: "POST",
                parameters: parameters,
                httpHeaders: headers,
                body: body,
                contentType: contentType,
                accept: accept,
                timeout: timeout,
                compressBody: compressBody,
                maxAttempts: maxAttempts);
        }

        /// <summary>Makes POST request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="body">Request body</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static async Task<RestCallerResponse> PostAsync(this IRESTCaller restCaller, Uri uri, string body = null, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", 
            int timeout = 1 * 60 * 1000, bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            return await restCaller.MakeRequestAsync(
                uri: uri,
                httpMethod: "POST",
                parameters: parameters,
                httpHeaders: headers,
                body: body,
                contentType: contentType,
                accept: accept,
                timeout: timeout,
                compressBody: compressBody,
                maxAttempts: maxAttempts);
        }

        /// <summary>Makes PUT request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="body">Request body</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static RestCallerResponse Put(this IRESTCaller restCaller, Uri uri, string body = null, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", 
            int timeout = 1 * 60 * 1000, bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            return restCaller.MakeRequest(
                uri: uri,
                httpMethod: "PUT",
                parameters: parameters,
                httpHeaders: headers,
                body: body,
                contentType: contentType,
                accept: accept,
                timeout: timeout,
                compressBody: compressBody,
                maxAttempts: maxAttempts);
        }

        /// <summary>Makes PUT request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="body">Request body</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static async Task<RestCallerResponse> PutAsync(this IRESTCaller restCaller, Uri uri, string body = null, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", 
            int timeout = 1 * 60 * 1000, bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            return await restCaller.MakeRequestAsync(
                uri: uri,
                httpMethod: "PUT",
                parameters: parameters,
                httpHeaders: headers,
                body: body,
                contentType: contentType,
                accept: accept,
                timeout: timeout,
                compressBody: compressBody,
                maxAttempts: maxAttempts);
        }

        /// <summary>Makes DELETE request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="body">Request body</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static RestCallerResponse Delete(this IRESTCaller restCaller, Uri uri, string body = null, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", 
            int timeout = 1 * 60 * 1000, bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            return restCaller.MakeRequest(
                uri: uri,
                httpMethod: "DELETE",
                parameters: parameters,
                httpHeaders: headers,
                body: body,
                contentType: contentType,
                accept: accept,
                timeout: timeout,
                compressBody: compressBody,
                maxAttempts: maxAttempts);
        }

        /// <summary>Makes DELETE request</summary>
        /// <param name="restCaller">REST Caller</param>
        /// <param name="uri">Request URL</param>
        /// <param name="body">Request body</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="headers">Request headers</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public static async Task<RestCallerResponse> DeleteAsync(this IRESTCaller restCaller, Uri uri, string body = null, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> headers = null, string contentType = "application/json; charset=utf-8", string accept = "application/json", int timeout = 1 * 60 * 1000,
            bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(restCaller, "restCaller");
            Check.ArgumentIsNull(uri, "uri");
            Check.Argument(timeout >= 0, "timeout >= 0");

            return await restCaller.MakeRequestAsync(
                uri: uri,
                httpMethod: "DELETE",
                parameters: parameters,
                httpHeaders: headers,
                body: body,
                contentType: contentType,
                accept: accept,
                timeout: timeout,
                compressBody: compressBody,
                maxAttempts: maxAttempts);
        }
    }
}
