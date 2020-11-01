using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ZipCo.Users.Infrastructure.Bootstrap.ErrorHandling
{
    public interface IHttpResponseHelper
    {
        Task WriteJsonResponse<T>(HttpContext context, T result);
    }

    internal class HttpResponseHelper : IHttpResponseHelper
    {
        public async Task WriteJsonResponse<T>(HttpContext context,  T result)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
