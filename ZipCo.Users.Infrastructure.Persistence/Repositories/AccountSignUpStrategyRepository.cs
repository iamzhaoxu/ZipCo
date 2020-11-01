using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Infrastructure.Persistence.DataContext;

namespace ZipCo.Users.Infrastructure.Persistence.Repositories
{
    public class AccountSignUpStrategyRepository : IAccountSignUpStrategyDataAccessor
    {
        private readonly UserContext _userContext;

        public AccountSignUpStrategyRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<AccountSignUpStrategy> GetDefaultStrategy()
        {
            return await _userContext.AccountSignUpStrategies
                .Where(asus => asus.IsDefault)
                .FirstOrDefaultAsync();
        }
    }
}
