using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ZipCo.Users.Domain.Factories;
using ZipCo.Users.Domain.Interfaces;
using ZipCo.Users.Domain.Service.AccountSignUp;

namespace ZipCo.Users.Domain
{
    public static class DependencyInjection
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAccountSignUpService, AccountSignUpService>();
            services.AddSingleton<IMemberFactory, MemberFactory>();
            services.AddSingleton<IAccountFactory, AccountFactory>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IAccountSignUpGuard, MonthNetIncomeGuard>());
        }
    }
}
