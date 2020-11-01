using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Infrastructure.Persistence.DataContext;

namespace ZipCo.Users.Test.Shared.InMemoryDb
{
    public static class InMemoryDbContextFactory
    {
        public static async Task<UserContext> CreateUserContext(string dbName = null)
        {
            dbName ??= "UserContext" + Guid.NewGuid();
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            var context = new UserContext(options);
            await PopulateSeedData(context);
            await context.SaveChangesAsync();
            return context;
        }

        private static async Task PopulateSeedData(UserContext context)
        {
            await PopulateAccountStatusData(context);
            await PopulateFrequency(context);
        }

        private static async Task PopulateFrequency(UserContext context)
        {
            await context.Frequencies.AddAsync(
                new Frequency
                {
                    Id = (long) FrequencyIds.Month,
                    Name = FrequencyIds.Month.ToString()
                }
            );

            await context.Frequencies.AddAsync(
                new Frequency
                {
                    Id = (long) FrequencyIds.Annual,
                    Name = FrequencyIds.Annual.ToString()
                }
            );
        }

        private static async Task PopulateAccountStatusData(UserContext context)
        {
            await context.AccountStatus.AddAsync(new AccountStatus
            {
                Id = (long) AccountStatusIds.Active,
                Name = AccountStatusIds.Active.ToString()
            });

            await context.AccountStatus.AddAsync(new AccountStatus
            {
                Id = (long) AccountStatusIds.Closed,
                Name = AccountStatusIds.Closed.ToString()
            });
        }
    }
}
