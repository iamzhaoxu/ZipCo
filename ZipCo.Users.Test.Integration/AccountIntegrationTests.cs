using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.WebApi;

namespace ZipCo.Users.Test.Integration
{
    public class AccountIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AccountIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        //[Fact(Skip = "Hack Rank Not Supported")]
        [Trait("Category", "Integration")]
        [Fact]
        public async Task GivenAccountController_WhenSignUpAccount_ShouldReturnAccount()
        {
            using (var client = _factory.CreateClient())
            {
                // assign
                var member = await client.SignUpMember("jack");

                // act
                var account = await client.SignUpAccount(member.Id);

                // assert
                account.ShouldNotBeNull();
                account.ShouldSatisfyAllConditions(
                        () => account.AccountNumber.ShouldStartWith("ZIP"),
                        () => account.AccountStatus.ShouldBe(AccountStatusIds.Active.ToString()),
                        () => account.AccountBalance.ShouldBe(0),
                        () => account.PendingBalance.ShouldBe(0),
                        () => account.AvailableBalance.ShouldBe(0),
                        () => account.Id.ShouldBeGreaterThan(0));
            }
        }

        //[Fact(Skip = "Hack Rank Not Supported")]
        [Trait("Category", "Integration")]
        [Fact]
        public async Task GivenAccountController_WhenListAccount_ShouldReturnAccounts()
        {
            using (var client = _factory.CreateClient())
            {
                // assign
                var member = await client.SignUpMember("jack");
                var account = await client.SignUpAccount(member.Id);
                var parameters = new Dictionary<string, string>
                {
                    {"AccountNumber", account.AccountNumber }
                };

                // act
                var response = await client.ListAccounts(parameters);

                // assert
                response.TotalPageNumber.ShouldBe(1);
                response.Data.Count().ShouldBe(1);
                var listedAccount = response.Data.First();
                listedAccount.ShouldSatisfyAllConditions(
                    () => listedAccount.AccountNumber.ShouldBe(account.AccountNumber),
                    () => listedAccount.AccountStatus.ShouldBe(account.AccountStatus),
                    () => listedAccount.AccountBalance.ShouldBe(account.AccountBalance),
                    () => listedAccount.PendingBalance.ShouldBe(account.PendingBalance),
                    () => listedAccount.AvailableBalance.ShouldBe(account.AvailableBalance));
            }
        }
    }
}