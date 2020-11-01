using System.Threading.Tasks;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Infrastructure.Persistence.Repositories;
using ZipCo.Users.Test.Shared.InMemoryDb;

namespace ZipCo.Users.Test.Unit.Infrastructure.Persistence.Repositories
{
    public class AccountSignUpStrategyRepositoryTests
    {
        [Fact]
        public async Task GivenAccountSignUpStrategyRepository_WhenCallGetById_ShouldReturnAccountSignUpStrategy()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                await context.AccountSignUpStrategies.AddAsync(
                    new AccountSignUpStrategy
                    {
                        IsDefault = true,
                        Name = "Master Program",
                        MonthNetIncomeLimit = 100
                    });
                await context.SaveChangesAsync();

                // act
                var repo = new AccountSignUpStrategyRepository(context);
                var actualAccountStrategy = await repo.GetDefaultStrategy();

                // assert
                actualAccountStrategy.ShouldNotBeNull();
                actualAccountStrategy.IsDefault.ShouldBeTrue();
            }
        }
    }
}
