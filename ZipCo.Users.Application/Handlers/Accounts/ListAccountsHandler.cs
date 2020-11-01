using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZipCo.Users.Application.Interfaces;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Application.Handlers.Accounts
{
    public class ListAccountsHandler: IRequestHandler<ListAccountsQuery, PaginationResponse<Account>>
    {
        private readonly IAccountService _accountService;

        public ListAccountsHandler(IAccountService accountService)
        {
           _accountService = accountService;
        }


        public async Task<PaginationResponse<Account>> Handle(ListAccountsQuery query, CancellationToken cancellationToken)
        {
           return await _accountService.ListAccounts(query);
        }
    }
}
