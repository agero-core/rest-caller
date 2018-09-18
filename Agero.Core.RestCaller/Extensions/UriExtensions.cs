using Agero.Core.Checker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Agero.Core.RestCaller.Extensions
{
    /// <summary>URL extensions</summary>
    public static class UriExtensions
    {
        /// <summary>Composing URL from base URL and parameters</summary>
        /// <param name="uri">Base URL</param>
        /// <param name="parameters">Parameters (key/values)</param>
        /// <returns>Composed URL</returns>
        public static Uri ComposeUri(this Uri uri, IReadOnlyDictionary<string, string> parameters)
        {
            Check.ArgumentIsNull(uri, "uri");
            Check.ArgumentIsNull(parameters, "parameters");
            Check.Argument(parameters.Any(), "parameters.Any()");

            var queryBuilder = new StringBuilder();
            foreach (var parameter in parameters)
            {
                if (parameter.Value == null)
                    continue;

                queryBuilder.Append("&");
                queryBuilder.Append(parameter.Key);
                queryBuilder.Append("=");
                queryBuilder.Append(WebUtility.UrlEncode(parameter.Value));
            }

            if (queryBuilder.Length > 0)
                queryBuilder[0] = '?';

            return new Uri(uri, queryBuilder.ToString());
        }

        /// <summary>Adds relative URL to base URL and returns result</summary>
        /// <param name="baseUri">Base URL</param>
        /// <param name="relativeUrl">Relative URL</param>
        /// <returns>Composed URL</returns>
        public static Uri Add(this Uri baseUri, string relativeUrl)
        {
            Check.ArgumentIsNull(baseUri, "baseUri");
            Check.ArgumentIsNullOrWhiteSpace(relativeUrl, "relativeUrl");

            if (relativeUrl.Last() != '/')
                relativeUrl += '/';

            if (relativeUrl.First() == '/' && relativeUrl.Length != 1)
                relativeUrl = relativeUrl.Substring(1);

            var originalString = baseUri.OriginalString;
            if (originalString.Last() != '/')
                baseUri = new Uri(originalString + "/");

            return new Uri(baseUri, relativeUrl);
        }
    }
}
