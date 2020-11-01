using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Test.Unit.Core.Domain.Entities.Accounts
{
    public class AccountEntityTests
    {
        [Theory]
        [InlineData(1000, -100, 900)]
        [InlineData(2400, 100, 2500)]
        public void GivenAccountEntity_WhenCallAvailableBalance_ShouldReturnValue(decimal accountBalance,
            decimal pendingBalance, decimal availableBalance)
        {
            // assign
            var memberExpensive = new Account
            {
                AccountBalance = accountBalance,
                PendingBalance = pendingBalance
            };

            // act
            var result = memberExpensive.AvailableBalance();

            //  assert
            result.ShouldBe(availableBalance);
        }
    }
}

