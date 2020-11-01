using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Design;

namespace ZipCo.Users.Infrastructure.Persistence.DataContext
{
    [ExcludeFromCodeCoverage]
    internal class UserContextDesignTimeFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            return new UserContext("Server=localhost;Database=PayCo.User;User Id=sa;Password=yourStrong(!)Password;");
        }
    }
}
