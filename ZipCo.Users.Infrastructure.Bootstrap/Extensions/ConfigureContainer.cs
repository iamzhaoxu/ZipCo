using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using ZipCo.Users.Infrastructure.Bootstrap.ErrorHandling;

namespace ZipCo.Users.Infrastructure.Bootstrap.Extensions
{
    public static class ConfigureContainer
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
        }

        public static void UseLogger(this ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
        }

    }
}
