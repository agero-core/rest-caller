using Agero.Core.Checker;
using System;
using System.Collections.Generic;
using System.Net;

namespace Agero.Core.RestCaller.Exceptions
{
    /// <summary>REST Caller exception</summary>
    [Serializable]
    public class RESTCallerException : Exception
    {
        /// <summary>Constructor</summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        /// <param name="attemptErrors">Retry errors</param>
        public RESTCallerException(string message, Exception innerException, IReadOnlyCollection<WebException> attemptErrors)
            : base(message, innerException)
        {
            Check.ArgumentIsNullOrWhiteSpace(message, nameof(message));
            Check.ArgumentIsNull(innerException, nameof(innerException));
            Check.ArgumentIsNull(attemptErrors, nameof(attemptErrors));

            AttemptErrors = attemptErrors;
        }

        /// <summary>Retry errors</summary>
        public IReadOnlyCollection<WebException> AttemptErrors { get; }
    }
}
