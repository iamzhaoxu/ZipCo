using MediatR;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;


namespace ZipCo.Users.Application.Requests.Accounts.Queries
{
    public class ListAccountsQuery: IRequest<PaginationResponse<Account>>
    {
        public AccountStatusIds? AccountStatusId { get; set; }

        public string AccountNumber { get; set; }

        public PaginationRequest Pagination { get; set; } = new PaginationRequest();

    }
}
