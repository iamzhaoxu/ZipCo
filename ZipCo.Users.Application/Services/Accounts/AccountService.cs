using System.Threading.Tasks;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Domain.Interfaces;

namespace ZipCo.Users.Application.Services.Accounts
{

    public class AccountService : IAccountService
    {
        private readonly IAccountDataAccessor _accountDataAccessor;
        private readonly IAccountSignUpStrategyDataAccessor _accountSignUpStrategyDataAccessor;
        private readonly IAccountSignUpService _accountSignUpEvaluator;
        private readonly IAccountFactory _accountFactory;

        public AccountService(IAccountDataAccessor accountDataAccessor,
            IAccountSignUpStrategyDataAccessor accountSignUpStrategyDataAccessor,
            IAccountSignUpService accountSignUpEvaluator,
            IAccountFactory accountFactory)
        {
            _accountDataAccessor = accountDataAccessor;
            _accountSignUpStrategyDataAccessor = accountSignUpStrategyDataAccessor;
            _accountSignUpEvaluator = accountSignUpEvaluator;
            _accountFactory = accountFactory;
        }

        public async Task<PaginationResponse<Account>> ListAccounts(ListAccountsQuery request)
        {
            return await _accountDataAccessor.ListAll(request.Pagination, request.AccountStatusId, request.AccountNumber);
        }

        public async Task<Account> GetAccountById(long accountId)
        {
            return await _accountDataAccessor.GetById(accountId);
        }

        public async Task<Account> SignUpAccount(Member member)
        {
            var strategy = await _accountSignUpStrategyDataAccessor.GetDefaultStrategy();
            var result = _accountSignUpEvaluator.Evaluate(member, strategy);
            if (!result.IsSuccess)
            {
                throw new BusinessException("Sign up account failed for member {memberId}. {error}",
                    BusinessErrors.BadRequest(result.ReasonPhase),
                    member.Id,
                    result.ReasonPhase);
            }
            var accountNumberSeq = await _accountDataAccessor.GetNextAccountNumberSeq();
            var accountNumber = $"ZIP{accountNumberSeq:D10}";
            var account = _accountFactory.Create(accountNumber, member.Id);
            await _accountDataAccessor.Create(account);
            return account;
        }
    }
}
