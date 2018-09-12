using Agero.Core.Checker;
using System.Collections.Generic;
using System.Net;

namespace Agero.Core.RestCaller
{
    /// <summary>REST Caller response</summary>
    public class RestCallerResponse
    {
        /// <summary>Constructor</summary>
        /// <param name="httpStatusCode">HTTP status code</param>
        /// <param name="contentType">Content type</param>
        /// <param name="text">Response text</param>
        /// <param name="headers">HTTP headers</param>
        /// <param name="attemptErrors">Attempt errors</param>
        public RestCallerResponse(HttpStatusCode httpStatusCode, string contentType, string text, IReadOnlyDictionary<string, string> headers, IReadOnlyCollection<WebException> attemptErrors)
        {
            Check.ArgumentIsNull(headers, nameof(headers));
            Check.ArgumentIsNull(attemptErrors, nameof(attemptErrors));

            HttpStatusCode = httpStatusCode;
            ContentType = contentType;
            Text = text;
            Headers = headers;
            AttemptErrors = attemptErrors;
        }

        /// <summary>HTTP status code</summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>Content type</summary>
        public string ContentType { get; }

        /// <summary>Response text</summary>
        public string Text { get; }

        /// <summary>HTTP headers</summary>
        public IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>Retry errors</summary>
        public IReadOnlyCollection<WebException> AttemptErrors { get; }
    }
}
