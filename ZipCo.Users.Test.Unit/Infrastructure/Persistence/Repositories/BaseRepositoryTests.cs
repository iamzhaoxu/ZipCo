using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Infrastructure.Persistence.DataContext;
using ZipCo.Users.Test.Shared.InMemoryDb;

namespace ZipCo.Users.Test.Unit.Infrastructure.Persistence.Repositories
{
    public class BaseRepositoryTests
    {
        [Fact]
        public async Task GivenBaseRepository_WhenCallUndoChange_IfEntityIsAdded_ShouldRollBackEntityState()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                await CreateAccountSingUpStrategy(context);

                // act
                context.UndoChange();

                // assert
                context.ChangeTracker.Entries().ShouldAllBe(e => e.State == EntityState.Unchanged);
            }
        }

        [Fact]
        public async Task GivenBaseRepository_WhenCallUndoChange_IfEntityIsUpdated_ShouldRollBackEntityState()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                await CreateAccountSingUpStrategy(context);
                await context.SaveChangesAsync();

                var strategy = await context.AccountSignUpStrategies.FirstAsync();
                strategy.IsDefault = false;

                // act
                context.UndoChange();

                // assert
                context.ChangeTracker.Entries().ShouldAllBe(e => e.State == EntityState.Unchanged);
                strategy.IsDefault.ShouldBeTrue();
            }
        }

        [Fact]
        public async Task GivenBaseRepository_WhenCallUndoChange_IfEntityIsDeleted_ShouldRollBackEntityState()
        {
            using (var context = await InMemoryDbContextFactory.CreateUserContext())
            {
                // assign
                await CreateAccountSingUpStrategy(context);
                await context.SaveChangesAsync();

                var strategy = await context.AccountSignUpStrategies.FirstAsync();
                context.AccountSignUpStrategies.Remove(strategy);

                // act
                context.UndoChange();

                // assert
                context.ChangeTracker.Entries().ShouldAllBe(e => e.State == EntityState.Unchanged);
                context.AccountSignUpStrategies.ShouldContain(strategy);
            }
        }


        private static async Task CreateAccountSingUpStrategy(UserContext context)
        {
            var strategy = new AccountSignUpStrategy
            {
                IsDefault = true,
                Name = "Master Program",
                MonthNetIncomeLimit = 100
            };
            await context.AccountSignUpStrategies.AddAsync(strategy);
        }
    }
}
