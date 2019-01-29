using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Agero.Core.RestCaller.Strategies
{
    /// <summary>
    /// Interface implemented by retry strategies.
    /// </summary>
    public interface IRetryStrategy
    {
        /// <summary>
        /// Determines whether or not the error is transient.
        /// </summary>
        /// <param name="webException">The <see cref="WebException"/> to apply the strategy too.</param>
        /// <returns><c>true</c> if the error is transient, otherwise <c>false</c>.</returns>
        bool IsTransient(WebException webException);
    }
}
