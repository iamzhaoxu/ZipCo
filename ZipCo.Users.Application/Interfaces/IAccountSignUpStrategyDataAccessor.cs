using System.Threading.Tasks;
using ZipCo.Users.Domain.Entities.AccountSignUp;

namespace ZipCo.Users.Application.Interfaces
{
    public interface IAccountSignUpStrategyDataAccessor
    {
        public Task<AccountSignUpStrategy> GetDefaultStrategy();
    }
}
