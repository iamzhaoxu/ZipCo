using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Infrastructure.Persistence.DataContext;
using ZipCo.Users.Infrastructure.Persistence.Repositories;
using ZipCo.Users.Test.Shared.DataBuilders;
using ZipCo.Users.Test.Shared.InMemoryDb;

namespace ZipCo.Users.Test.Unit.Infrastructure.Persistence.Repositories
{
    public class AccountRepositoryTests
    {
        [Fact]
        public async Task GivenAccountRepository_WhenCallGetById_ShouldReturnAccount()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                var account = await CreateOneAccountWithOneMember(1, 1, context);

                // act
                var repo = new AccountRepository(context);
                var actualAccount = await repo.GetById(account.Id);

                // assert
                actualAccount.ShouldNotBeNull();
                actualAccount.ShouldSatisfyAllConditions(
                    () => actualAccount.Id.ShouldBe(account.Id),
                    () => actualAccount.MemberAccounts.ShouldNotBeNull(),
                    () => actualAccount.MemberAccounts.Count.ShouldBe(1));
            }
        }

        [Fact]
        public async Task GivenAccountRepository_WhenCallGetByAccountNumber_ShouldReturnAccount()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                var account = await CreateOneAccountWithOneMember(1, 1, context);

                // act
                var repo = new AccountRepository(context);
                var actualAccount = await repo.GetByAccountNumber(account.AccountNumber);

                // assert
                actualAccount.ShouldNotBeNull();
                actualAccount.ShouldSatisfyAllConditions(
                    () => actualAccount.Id.ShouldBe(account.Id),
                    () => actualAccount.MemberAccounts.ShouldNotBeNull(),
                    () => actualAccount.MemberAccounts.Count.ShouldBe(1));
            }
        }

        [Fact]
        public async Task GivenAccountRepository_WhenCallCreate_ShouldSuccess()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                var testData = (memebrId: 0, accountId: 0, memebrAccountId: 1);
                var member = MemberDataBuilder.CreateMember(testData.memebrId, new List<MemberAccount>(), null, null);
                await context.Members.AddAsync(member);
                await context.SaveChangesAsync();

                var memberAccount = MemberDataBuilder.CreateMemberAccount(testData.memebrAccountId, testData.accountId, testData.memebrId);
                var account = AccountDataBuilder.CreateAccount(testData.accountId, new List<MemberAccount> { memberAccount });

                // act
                var repo = new AccountRepository(context);
                await repo.Create(account);
                await context.SaveChangesAsync();

                // assert
                account.Id.ShouldBeGreaterThan(0);
            }
        }

        private static async Task<Account> CreateOneAccountWithOneMember(int accountId, int memberId, UserContext context)
        {
            var memberAccount = MemberDataBuilder.CreateMemberAccount(1, accountId, memberId);
            var member = MemberDataBuilder.CreateMember(1, new List<MemberAccount> { memberAccount }, null, null);
            var account = AccountDataBuilder.CreateAccount(1, new List<MemberAccount> { memberAccount });

            await context.Accounts.AddAsync(account);
            await context.Members.AddAsync(member);
            await context.SaveChangesAsync();
            return account;
        }
    }
}
