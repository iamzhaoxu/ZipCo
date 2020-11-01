using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZipCo.Users.Application;
using ZipCo.Users.Domain;
using ZipCo.Users.Infrastructure.Bootstrap.ErrorHandling;
using ZipCo.Users.Infrastructure.Persistence;
using ZipCo.Users.Infrastructure.Persistence.DataContext;

namespace ZipCo.Users.Infrastructure.Bootstrap.Extensions
{
    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection serviceCollection,
             IConfiguration configuration)
        {
            serviceCollection.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserDbConnection"));
                // we will us no tracking as default. When we update data, will change the state to modified to update it.
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
            });
        }


        public static void AddDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApplicationsDependencies();
            serviceCollection.AddPersistenceDependencies();
            serviceCollection.AddDomainDependencies();
            serviceCollection.AddSingleton<IHttpResponseHelper, HttpResponseHelper>();
        }

    }
}
