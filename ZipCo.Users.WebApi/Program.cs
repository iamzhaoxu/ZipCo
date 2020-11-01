using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ZipCo.Users.WebApi.Extensions;

namespace ZipCo.Users.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ReadAppConfiguration()
                .ReadLogConfiguration()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
