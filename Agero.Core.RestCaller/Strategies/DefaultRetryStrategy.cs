using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Agero.Core.RestCaller.Strategies
{
    /// <summary>
    /// Default strategy for detecting transient failures.
    /// </summary>
    /// <remarks>
    /// The strategy considers the following to be transient and retriable:
    ///    WebExceptionStatus.ConnectFailure,
    ///    WebExceptionStatus.ConnectionClosed,
    ///    WebExceptionStatus.NameResolutionFailure,
    ///    WebExceptionStatus.PipelineFailure,
    ///    WebExceptionStatus.ReceiveFailure,
    ///    WebExceptionStatus.SendFailure,
    ///    WebExceptionStatus.Timeout
    /// </remarks>
    public class DefaultRetryStrategy : IRetryStrategy
    {
        private static readonly IReadOnlyCollection<WebExceptionStatus> _transientWebExceptionStatuses =
            new[]
            {
                WebExceptionStatus.ConnectFailure,
                WebExceptionStatus.ConnectionClosed,
                WebExceptionStatus.NameResolutionFailure,
                WebExceptionStatus.PipelineFailure,
                WebExceptionStatus.ReceiveFailure,
                WebExceptionStatus.SendFailure,
                WebExceptionStatus.Timeout
            };

        /// <inheritDoc />
        public bool IsTransient(WebException webException)
        {
            return _transientWebExceptionStatuses.Contains(webException.Status);
        }
    }
}
