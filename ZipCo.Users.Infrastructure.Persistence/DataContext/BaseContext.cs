using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ZipCo.Users.Infrastructure.Persistence.DataContext
{
    public abstract class BaseContext : DbContext
    {
        protected string ConnectionString { get; }

        protected BaseContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }

        protected BaseContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<int> NextValueForSequence(string sequenceName)
        {
            var result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await Database.ExecuteSqlRawAsync($"SELECT @result = (NEXT VALUE FOR [{sequenceName}])", result);

            return (int)result.Value;
        }

        public void UndoChange()
        {
            var changedEntries = this.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();
            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
