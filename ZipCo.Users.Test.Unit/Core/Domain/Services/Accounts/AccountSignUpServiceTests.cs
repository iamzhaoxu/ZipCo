using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;
using ZipCo.Users.Domain.Service.AccountSignUp;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Domain.Services.Accounts
{
    public class AccountSignUpServiceTests
    {

        [Fact]
        public void GivenAccountSignUpService_WhenCallMethodEvaluate_IfOneOfGuardFailed_ShouldReturnFailedResult()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.TryAddEnumerable(ServiceDescriptor.Singleton<IAccountSignUpGuard, AlwaysSuccessGuard>());
            serviceCollection.TryAddEnumerable(ServiceDescriptor.Singleton<IAccountSignUpGuard, AlwaysFailedGuard>());
            serviceCollection.TryAddEnumerable(ServiceDescriptor.Singleton<IAccountSignUpGuard, MonthNetIncomeGuard>());

            using (var provider = serviceCollection.BuildServiceProvider())
            {
                var accountSignUpService = new AccountSignUpService(provider.GetService<IEnumerable<IAccountSignUpGuard>>());
                var member = MemberDataBuilder.CreateMember(1, new List<MemberAccount>(), null, null);
                var strategy = AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(1);
                var result = accountSignUpService.Evaluate(member, strategy);
                result.IsSuccess.ShouldBeFalse();
                result.ReasonPhase.ShouldBe("always failed!");
            }
        }

        [Fact]
        public void GivenAccountSignUpService_WhenCallMethodEvaluate_IfNoGuardFailed_ShouldReturnSuccessResult()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.TryAddEnumerable(ServiceDescriptor.Singleton<IAccountSignUpGuard, AlwaysSuccessGuard>());

            using (var provider = serviceCollection.BuildServiceProvider())
            {
                var accountSignUpService = new AccountSignUpService(provider.GetService<IEnumerable<IAccountSignUpGuard>>());
                var member = MemberDataBuilder.CreateMember(1, new List<MemberAccount>(), null, null);
                var strategy = AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(1);
                var result = accountSignUpService.Evaluate(member, strategy);
                result.IsSuccess.ShouldBeTrue();
            }
        }

        public class AlwaysSuccessGuard : IAccountSignUpGuard
        {
            public AccountSignUpResult CanSignUp(Member member, AccountSignUpStrategy strategy)
            {
                return AccountSignUpResult.Success();
            }
        }

        public class AlwaysFailedGuard : IAccountSignUpGuard
        {
            public AccountSignUpResult CanSignUp(Member member, AccountSignUpStrategy strategy)
            {
                return AccountSignUpResult.Fail("always failed!");
            }
        }

     
    }
}
