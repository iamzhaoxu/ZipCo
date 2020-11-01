using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ZipCo.Users.Infrastructure.Persistence.DataContext;

namespace ZipCo.Users.DbUpdater
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("UserContextMigrationDbConnecitonString");
            using (var context = new UserContext(connectionString))
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
                Console.WriteLine("Database has been migrated");
            }
        }
    }
}
