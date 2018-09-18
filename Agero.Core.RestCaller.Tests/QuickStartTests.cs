using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Agero.Core.RestCaller.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agero.Core.RestCaller.Tests
{
    [TestClass]
    public class QuickStartTests
    {
        [TestMethod]
        public async Task Test_Different_Restful_Calls()
        {
            // Creating REST caller
            var caller = new RESTCaller();
            
            // Making GET call to https://jsonplaceholder.typicode.com/posts?userId=1
            var getResponse = await caller.GetAsync(
                uri: new Uri("https://jsonplaceholder.typicode.com/posts"),
                parameters: new Dictionary<string, string>
                {
                    {"userId", "1"}
                });
            Assert.AreEqual(HttpStatusCode.OK, getResponse.HttpStatusCode);
            Assert.AreEqual("application/json; charset=utf-8", getResponse.ContentType);
            Assert.IsFalse(string.IsNullOrWhiteSpace(getResponse.Text));
            
            // Making POST call to https://jsonplaceholder.typicode.com/posts
            var postResponse = await caller.PostAsync(
                uri: new Uri("https://jsonplaceholder.typicode.com/posts"),
                body: @"{ ""title"": ""foo"", ""body"": ""bar"", ""userId"": 1 }");
            Assert.AreEqual(HttpStatusCode.OK, getResponse.HttpStatusCode);
            Assert.AreEqual("application/json; charset=utf-8", getResponse.ContentType);
            Assert.IsFalse(string.IsNullOrWhiteSpace(getResponse.Text));
        }
        
    }
}