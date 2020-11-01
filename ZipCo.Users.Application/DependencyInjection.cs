using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Pipelines;
using ZipCo.Users.Application.Services.Accounts;
using ZipCo.Users.Application.Services.Members;

namespace ZipCo.Users.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationsDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LogPipeLineBehavior<,>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMemberService, MemberService>();
        }
    }
}
