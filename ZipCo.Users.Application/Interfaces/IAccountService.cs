using System.Threading.Tasks;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccountById(long accountId);
        Task<Account> SignUpAccount(Member member);
        Task<PaginationResponse<Account>> ListAccounts(ListAccountsQuery request);
    }
}
