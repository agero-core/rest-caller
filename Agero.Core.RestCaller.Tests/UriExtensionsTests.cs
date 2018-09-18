using System;
using System.Collections.Generic;
using System.Linq;
using Agero.Core.RestCaller.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agero.Core.RestCaller.Tests
{
    [TestClass]
    public class UriExtensionsTests
    {
        private const string BASE_URL = "https://someurl.org/resource";
        private const string RELATIVE_URL = "students/JohnSmith";
        private readonly IReadOnlyDictionary<string, string> _parameters =
            new Dictionary<string, string>
            {
                {"abc", "123"},
                {"bcd", "www"}
            };

        [TestMethod]
        public void Add()
        {
            // Arrange
            var baseUri = new Uri(BASE_URL);

            // Act
            var result = baseUri.Add(RELATIVE_URL);

            // Assert
            Assert.AreEqual($@"{BASE_URL}/{RELATIVE_URL}/", result.ToString());
        }

        [TestMethod]
        public void Add_With_Slash_At_The_End()
        {
            // Arrange
            var baseUri = new Uri(BASE_URL);

            // Act
            var result = baseUri.Add(RELATIVE_URL + "/");

            // Assert
            Assert.AreEqual($@"{BASE_URL}/{RELATIVE_URL}/", result.ToString());
        }

        [TestMethod]
        public void Add_With_Slash_At_The_Begining()
        {
            // Arrange
            var baseUri = new Uri(BASE_URL);

            // Act
            var result = baseUri.Add("/" + RELATIVE_URL);

            // Assert
            Assert.AreEqual($@"{BASE_URL}/{RELATIVE_URL}/", result.ToString());
        }

        [TestMethod]
        public void ComposeUri()
        {
            // Arrange
            var uri = new Uri(BASE_URL);

            // Act
            var result = uri.ComposeUri(_parameters);

            // Assert
            Assert.AreEqual($@"{BASE_URL}?{_parameters.First().Key}={_parameters.First().Value}&{_parameters.Last().Key}={_parameters.Last().Value}", result.ToString());
        }

        [TestMethod]
        public void ComposeUri_With_Slash_At_The_End()
        {
            // Arrange
            var uri = new Uri(BASE_URL + "/");

            // Act
            var result = uri.ComposeUri(_parameters);

            // Assert
            Assert.AreEqual($@"{BASE_URL}/?{_parameters.First().Key}={_parameters.First().Value}&{_parameters.Last().Key}={_parameters.Last().Value}", result.ToString());
        }
    }
}