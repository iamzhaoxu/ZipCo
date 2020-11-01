using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Infrastructure.Persistence.DataContext;
using ZipCo.Users.Infrastructure.Persistence.Extensions;

namespace ZipCo.Users.Infrastructure.Persistence.Repositories
{

    public class AccountRepository : IAccountDataAccessor
    {
        private readonly UserContext _userContext;

        public AccountRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<Account> GetById(long accountId)
        {
            return await _userContext.Accounts
                            .Include(a => a.AccountStatus)
                            .Where(a => a.Id == accountId)
                            .FirstOrDefaultAsync();
        }

        public async Task<Account> GetByAccountNumber(string accountNumber)
        {
            return await _userContext.Accounts
                            .Include(a => a.AccountStatus)
                            .Where(a => a.AccountNumber == accountNumber)
                            .FirstOrDefaultAsync();
        }

        public async Task<PaginationResponse<Account>> ListAll(PaginationRequest paginationRequest, AccountStatusIds? accountStatusId, string accountNumber)
        {
            return  await _userContext.Accounts
                            .Include(a => a.AccountStatus)
                            .WhereWhenValueNotNull(accountStatusId, a => a.AccountStatusId == (long)accountStatusId)
                            .WhereWhenValueNotNull(accountNumber, a => a.AccountNumber.Contains(accountNumber))
                            .OrderByDescending(a => a.CreatedOn)
                            .ListByPaging(paginationRequest.PageSize, paginationRequest.PageNumber);
        }

        public async Task Create(Account account)
        {
           await _userContext.Accounts.AddAsync(account);
        }

        public async Task<int> GetNextAccountNumberSeq()
        {
            return await _userContext.NextValueForSequence(SequenceNames.AccountNumberSequence);
        }
    }
}
