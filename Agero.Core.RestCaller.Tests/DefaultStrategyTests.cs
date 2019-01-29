using Agero.Core.RestCaller.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Agero.Core.RestCaller.Tests
{
    [TestClass]
    public class DefaultStrategyTests
    {
        static IRetryStrategy retryStrategy = new DefaultRetryStrategy();

        [DataTestMethod]
        [DataRow(WebExceptionStatus.ConnectFailure)]
        [DataRow(WebExceptionStatus.ConnectionClosed)]
        [DataRow(WebExceptionStatus.NameResolutionFailure)]
        [DataRow(WebExceptionStatus.PipelineFailure)]
        [DataRow(WebExceptionStatus.ReceiveFailure)]
        [DataRow(WebExceptionStatus.SendFailure)]
        [DataRow(WebExceptionStatus.Timeout)]
        public void Status_IsTransient(WebExceptionStatus status)
        {
            var ex = new WebException(status.ToString(), status);

            Assert.IsTrue(retryStrategy.IsTransient(ex));
        }

        [DataTestMethod]
        [DataRow(WebExceptionStatus.CacheEntryNotFound)]
        [DataRow(WebExceptionStatus.KeepAliveFailure)]
        [DataRow(WebExceptionStatus.MessageLengthLimitExceeded)]
        [DataRow(WebExceptionStatus.Pending)]
        [DataRow(WebExceptionStatus.ProtocolError)]
        [DataRow(WebExceptionStatus.ProxyNameResolutionFailure)]
        [DataRow(WebExceptionStatus.RequestCanceled)]
        [DataRow(WebExceptionStatus.RequestProhibitedByCachePolicy)]
        [DataRow(WebExceptionStatus.RequestProhibitedByProxy)]
        [DataRow(WebExceptionStatus.SecureChannelFailure)]
        [DataRow(WebExceptionStatus.ServerProtocolViolation)]
        [DataRow(WebExceptionStatus.Success)]
        [DataRow(WebExceptionStatus.TrustFailure)]
        [DataRow(WebExceptionStatus.UnknownError)]
        public void Status_IsNotTransient(WebExceptionStatus status)
        {
            var ex = new WebException(status.ToString(), status);

            Assert.IsFalse(retryStrategy.IsTransient(ex));
        }
    }
}
