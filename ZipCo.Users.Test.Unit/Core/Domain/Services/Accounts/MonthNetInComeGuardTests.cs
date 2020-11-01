using System.Collections.Generic;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;
using ZipCo.Users.Domain.Service.AccountSignUp;
using ZipCo.Users.Test.Shared.DataBuilders;

namespace ZipCo.Users.Test.Unit.Core.Domain.Services.Accounts
{
    public class MonthNetInComeGuardTests
    {
        private  IAccountSignUpGuard _accountSignUpGuard;

        public MonthNetInComeGuardTests()
        {
            _accountSignUpGuard = new MonthNetIncomeGuard();
        }

        [Theory]
        [InlineData(2000, 1000, 1000, true)]
        [InlineData(1000, 2000, 1000, false)]
        [InlineData(3000, 2000, 3000, false)]
        public void GivenMonthNetIncomeGuard_WhenCallCanSignUp_ShouldReturnResult(decimal monthlySalary, decimal monthlyExpense, decimal monthNetIncomeLimit, bool canSignUp)
        {
            // assign

            var memberId = 1;
            var memberExpensive = MemberDataBuilder.CreateMemberExpense(1, memberId, FrequencyIds.Month, monthlyExpense);
            var memberSalary = MemberDataBuilder.CreateMemberSalary(1, memberId, FrequencyIds.Month, monthlySalary);
            var member = MemberDataBuilder.CreateMember(1, new List<MemberAccount>(), memberExpensive, memberSalary);
            var strategy = AccountSignUpStrategyDataBuilder.CreateAccountSignUpStrategy(1, true, monthNetIncomeLimit);
            
            // act
            var result = _accountSignUpGuard.CanSignUp(member, strategy);

            // assert
            result.IsSuccess.ShouldBe(canSignUp);
        }
    }
}
