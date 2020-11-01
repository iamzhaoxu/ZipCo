using System.Collections.Generic;
using System.Text;

namespace ZipCo.Users.Test.Integration
{
    public static class UrlExtension
    {
        public static string CreateUrlWithQueryString(this IDictionary<string, string> parameters, string baseUrl)
        {
            var builder = new StringBuilder(baseUrl);
            if (parameters.Count > 0)
            {
                builder.Append("?");
            }
            var queryPairs = new List<string>();
            foreach (var key in parameters.Keys)
            {
                queryPairs.Add($"{key}={parameters[key]}");
            }
            builder.Append(string.Join("&", queryPairs));
            return builder.ToString();
        }
    }
}
