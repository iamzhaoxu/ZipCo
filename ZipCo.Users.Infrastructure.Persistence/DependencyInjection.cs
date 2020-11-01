using Microsoft.Extensions.DependencyInjection;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Infrastructure.Persistence.DataContext;
using ZipCo.Users.Infrastructure.Persistence.Repositories;

namespace ZipCo.Users.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMemberDataAccessor, MemberRepository>();
            services.AddScoped<IAccountSignUpStrategyDataAccessor, AccountSignUpStrategyRepository>();
            services.AddScoped<IAccountDataAccessor, AccountRepository>();
            services.AddTransient<IUserUnitOfWork>(serviceProvider => serviceProvider.GetService<UserContext>());
        }
    }
}
