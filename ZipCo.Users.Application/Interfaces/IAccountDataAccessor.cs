using System.Threading.Tasks;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Application.Interfaces
{
    public interface IAccountDataAccessor
    {
        Task<Account> GetById(long accountId);
        Task<Account> GetByAccountNumber(string accountNumber);
        Task<PaginationResponse<Account>> ListAll(PaginationRequest paginationRequest, AccountStatusIds? accountStatusId, string accountNumber);
        Task Create(Account account);
        Task<int> GetNextAccountNumberSeq();

    }

}
