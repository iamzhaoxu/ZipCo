using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Domain.Entities.Members;


namespace ZipCo.Users.Infrastructure.Persistence.DataContext
{
    [ExcludeFromCodeCoverage]
    public class UserContext : BaseContext, IUserUnitOfWork
    { 

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatus> AccountStatus { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberAccount> MemberAccounts { get; set; }
        public DbSet<MemberSalary> MemberSalaries { get; set; }
        public DbSet<MemberExpense> MemberExpenses { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<AccountSignUpStrategy> AccountSignUpStrategies { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        { }

        public UserContext(string connectionString): base(connectionString)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence(SequenceNames.AccountNumberSequence)
                        .StartsAt(0)
                        .IncrementsBy(1);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
