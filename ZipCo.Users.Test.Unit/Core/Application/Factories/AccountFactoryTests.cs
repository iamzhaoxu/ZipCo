
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Factories;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Test.Unit.Core.Application.Factories
{
    public class AccountFactoryTests
    {
        private readonly IAccountFactory _accountFactory;

        public AccountFactoryTests()
        {
            _accountFactory = new AccountFactory();
        }

        [Fact]
        public void GivenAccountFactory_WhenCallCreate_ShouldReturnAccount()
        {
            var testData = (AccountNumber: "zip1000001", memberId: 101);
            var account = _accountFactory.Create(testData.AccountNumber, testData.memberId);
            account.ShouldNotBeNull();
            account.ShouldSatisfyAllConditions(
                () => account.AccountNumber.ShouldBe(testData.AccountNumber),
                () => account.MemberAccounts.ShouldContain(m => m.MemberId == testData.memberId),
                () => account.MemberAccounts.ShouldContain(m => m.AccountId == 0),
                () => account.AccountStatusId.ShouldBe((long)AccountStatusIds.Active));
        }
    }
}
