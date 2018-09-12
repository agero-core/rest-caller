using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Agero.Core.RestCaller.Tests
{
    [TestClass]
    public class RestCallerTests
    {
        [TestMethod]
        public async Task Get()
        {
            // Arrange
            var restCaller = new RestCaller();

            var uri = new Uri("https://api.nuget.org/v3/index.json");

            var response = await restCaller.MakeRequestAsync(httpMethod: "GET", uri: uri);

            // Assert
            Assert.AreEqual("application/json", response.ContentType);
            Assert.AreEqual(HttpStatusCode.OK, response.HttpStatusCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Text));
            Assert.IsTrue(response.Headers.Count > 0);
            Assert.AreEqual(0, response.AttemptErrors.Count);
        }

        [TestMethod]
        public async Task Get_When_Resource_Does_Not_Exist()
        {
            // Arrange
            var restCaller = new RestCaller();

            var uri = new Uri("https://api.nuget.org/v3/does_not_exist.json");

            var response = await restCaller.MakeRequestAsync(httpMethod: "GET", uri: uri);

            // Assert
            Assert.AreEqual("application/xml", response.ContentType);
            Assert.AreEqual(HttpStatusCode.NotFound, response.HttpStatusCode);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Text));
            Assert.IsTrue(response.Headers.Count > 0);
            Assert.AreEqual(1, response.AttemptErrors.Count);
        }
    }
}
