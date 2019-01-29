using Agero.Core.Checker;
using Agero.Core.RestCaller.Exceptions;
using Agero.Core.RestCaller.Extensions;
using Agero.Core.RestCaller.Strategies;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Agero.Core.RestCaller
{
    /// <summary>REST client to make REST calls</summary>
    public class RESTCaller : IRESTCaller
    {
        private const int WAIT_TIMEOUT_IN_MILLISECONDS_AFTER_FIRST_ATTEMPT = 100;

        private IRetryStrategy retryStrategy;

        /// <summary>
        /// Creates a new instance of <see cref="RESTCaller"/> using the
        /// <see cref="DefaultRetryStrategy"/>.
        /// </summary>
        public RESTCaller()
            : this(new DefaultRetryStrategy())
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="RESTCaller"/> using the
        /// provided <see cref="IRetryStrategy"/>.
        /// </summary>
        /// <param name="retryStrategy">In implementation of <see cref="IRetryStrategy"/> to use when
        /// determining if an error is transient or not.</param>
        public RESTCaller(IRetryStrategy retryStrategy)
        {
            Check.ArgumentIsNull<IRetryStrategy>(retryStrategy, "retryStrategy");

            this.retryStrategy = retryStrategy;
        }

        /// <summary>Makes request based on httpMethod</summary>
        /// <param name="uri">Request URL</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="httpMethod">HTTP method (GET, POST etc)</param>
        /// <param name="httpHeaders">Request headers</param>
        /// <param name="body">Request body</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public RestCallerResponse MakeRequest(Uri uri, string httpMethod, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> httpHeaders = null, string body = null, string contentType = "application/json; charset=utf-8", 
            string accept = "application/json", int timeout = 1 * 60 * 1000, bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(uri, "uri");
            Check.ArgumentIsNullOrWhiteSpace(httpMethod, "httpMethod");
            Check.ArgumentIsNullOrWhiteSpace(contentType, "contentType");
            Check.ArgumentIsNullOrWhiteSpace(accept, "accept");
            Check.Argument(timeout >= 0, "timeout >= 0");
            Check.Argument(maxAttempts >= 1, "maxAttempts >= 1");

            var attemptErrors = new List<WebException>();

            try
            {
                while (true)
                {
                    if (parameters != null && parameters.Any())
                        uri = uri.ComposeUri(parameters);

                    try
                    {
                        var webRequest = CreateWebRequest(uri, httpMethod, contentType, accept, timeout);

                        AddHttpHeaders(webRequest, httpHeaders, compressBody);

                        if (!string.IsNullOrWhiteSpace(body))
                        {
                            if (compressBody)
                            {
                                using (var requestStream = webRequest.GetRequestStream())
                                {
                                    using (var gzipStream = new GZipStream(requestStream, CompressionMode.Compress))
                                    {
                                        using (var streamWriter = new StreamWriter(gzipStream))
                                        {
                                            streamWriter.Write(body);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (var requestStream = webRequest.GetRequestStream())
                                {
                                    using (var streamWriter = new StreamWriter(requestStream))
                                    {
                                        streamWriter.Write(body);
                                    }
                                }
                            }
                        }

                        using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                        {
                            return GetResponse(webResponse, attemptErrors);
                        }
                    }
                    catch (WebException ex)
                    {
                        attemptErrors.Add(ex);

                        if (attemptErrors.Count < maxAttempts && retryStrategy.IsTransient(ex))
                        {
                            Thread.Sleep(WAIT_TIMEOUT_IN_MILLISECONDS_AFTER_FIRST_ATTEMPT * attemptErrors.Count);
                            continue;
                        }

                        var response = ex.Response;
                        if (response == null)
                            throw;

                        using (var webResponse = (HttpWebResponse)response)
                        {
                            return GetResponse(webResponse, attemptErrors);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RESTCallerException("REST Caller Error.", ex, attemptErrors);
            }
        }

        /// <summary>Makes request based on httpMethod</summary>
        /// <param name="uri">Request URL</param>
        /// <param name="httpMethod">HTTP method (GET, POST etc)</param>
        /// <param name="parameters">Request URL parameters</param>
        /// <param name="httpHeaders">Request headers</param>
        /// <param name="body">Request body</param>
        /// <param name="contentType">Content-Type HTTP header</param>
        /// <param name="accept">Accept HTTP header</param>
        /// <param name="timeout">Request timeout</param>
        /// <param name="compressBody">Compresses request using GZIP</param>
        /// <param name="maxAttempts">Maximum number of times to attempt the request. Cannot be less than 1.</param>
        /// <exception cref="RESTCallerException">Thrown when any error happened.</exception>
        public async Task<RestCallerResponse> MakeRequestAsync(Uri uri, string httpMethod, IReadOnlyDictionary<string, string> parameters = null, 
            IReadOnlyDictionary<string, string> httpHeaders = null, string body = null, string contentType = "application/json; charset=utf-8", 
            string accept = "application/json", int timeout = 1 * 60 * 1000, bool compressBody = false, int maxAttempts = 1)
        {
            Check.ArgumentIsNull(uri, "uri");
            Check.ArgumentIsNullOrWhiteSpace(httpMethod, "httpMethod");
            Check.ArgumentIsNullOrWhiteSpace(contentType, "contentType");
            Check.ArgumentIsNullOrWhiteSpace(accept, "accept");
            Check.Argument(timeout >= 0, "timeout >= 0");
            Check.Argument(maxAttempts >= 1, "maxAttempts >= 1");

            var attemptErrors = new List<WebException>();

            try
            {
                while (true)
                {
                    if (parameters != null && parameters.Any())
                        uri = uri.ComposeUri(parameters);

                    try
                    {
                        var webRequest = CreateWebRequest(uri, httpMethod, contentType, accept, timeout);

                        AddHttpHeaders(webRequest, httpHeaders, compressBody);

                        if (!string.IsNullOrWhiteSpace(body))
                        {
                            if (compressBody)
                            {
                                using (var requestStream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false))
                                {
                                    using (var gzipStream = new GZipStream(requestStream, CompressionMode.Compress))
                                    {
                                        using (var streamWriter = new StreamWriter(gzipStream))
                                        {
                                            await streamWriter.WriteAsync(body).ConfigureAwait(false);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (var requestStream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false))
                                {
                                    using (var streamWriter = new StreamWriter(requestStream))
                                    {
                                        await streamWriter.WriteAsync(body).ConfigureAwait(false);
                                    }
                                }
                            }
                        }

                        using (var webResponse = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false))
                        {
                            return await GetResponseAsync(webResponse, attemptErrors).ConfigureAwait(false);
                        }
                    }
                    catch (WebException ex)
                    {
                        attemptErrors.Add(ex);

                        if (attemptErrors.Count < maxAttempts && retryStrategy.IsTransient(ex))
                        {
                            await Task.Delay(WAIT_TIMEOUT_IN_MILLISECONDS_AFTER_FIRST_ATTEMPT * attemptErrors.Count).ConfigureAwait(false);
                            continue;
                        }
                                        
                        var response = ex.Response;
                        if (response == null)
                            throw;

                        using (var webResponse = (HttpWebResponse)response)
                        {
                            return await GetResponseAsync(webResponse, attemptErrors).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RESTCallerException("REST Caller Error.", ex, attemptErrors);
            }
        }

        private static HttpWebRequest CreateWebRequest(Uri uri, string httpMethod, string contentType, string accept, int timeout)
        {
            Check.ArgumentIsNull(uri, "uri");
            Check.ArgumentIsNullOrWhiteSpace(httpMethod, "httpMethod");
            Check.ArgumentIsNullOrWhiteSpace(contentType, "contentType");
            Check.ArgumentIsNullOrWhiteSpace(accept, "accept");
            Check.Argument(timeout >= 0, "timeout >= 0");

            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = httpMethod;
            webRequest.ContentType = contentType;
            webRequest.Accept = accept;
            webRequest.UseDefaultCredentials = true;
            webRequest.Timeout = timeout;
            webRequest.AutomaticDecompression = DecompressionMethods.GZip;

            return webRequest;
        }

        private static void AddHttpHeaders(HttpWebRequest webRequest, IReadOnlyDictionary<string, string> httpHeaders = null, bool compressBody = false)
        {
            Check.ArgumentIsNull(webRequest, "webRequest");

            if (httpHeaders != null)
            {
                foreach (var httpheader in httpHeaders)
                    webRequest.Headers.Add(httpheader.Key, httpheader.Value);
            }

            if (compressBody)
                webRequest.Headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
        }

        private static RestCallerResponse GetResponse(HttpWebResponse webResponse, IReadOnlyCollection<WebException> attemptErrors)
        {
            Check.ArgumentIsNull(webResponse, nameof(webResponse));
            Check.ArgumentIsNull(attemptErrors, nameof(attemptErrors));

            using (var responseStream = webResponse.GetResponseStream())
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var streamReader = new StreamReader(responseStream))
                {
                    return new RestCallerResponse
                    (
                        httpStatusCode: webResponse.StatusCode,
                        contentType: webResponse.ContentType,
                        text: streamReader.ReadToEnd(),
                        headers: GetHeaders(webResponse.Headers),
                        attemptErrors: attemptErrors
                    );
                }
            }
        }

        private static async Task<RestCallerResponse> GetResponseAsync(HttpWebResponse webResponse, IReadOnlyCollection<WebException> attemptErrors)
        {
            Check.ArgumentIsNull(webResponse, nameof(webResponse));
            Check.ArgumentIsNull(attemptErrors, nameof(attemptErrors));

            using (var responseStream = webResponse.GetResponseStream())
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var streamReader = new StreamReader(responseStream))
                {
                    return new RestCallerResponse
                    (
                        httpStatusCode: webResponse.StatusCode,
                        contentType: webResponse.ContentType,
                        text: await streamReader.ReadToEndAsync().ConfigureAwait(false),
                        headers: GetHeaders(webResponse.Headers),
                        attemptErrors: attemptErrors
                    );
                }
            }
        }

        private static IReadOnlyDictionary<string, string> GetHeaders(WebHeaderCollection headers)
        {
            Check.ArgumentIsNull(headers, nameof(headers));

            return headers.AllKeys.ToDictionary(k => k, k => headers[k]);
        }
    }
}
