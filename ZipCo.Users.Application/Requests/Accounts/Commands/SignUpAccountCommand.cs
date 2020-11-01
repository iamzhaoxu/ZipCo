using MediatR;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Application.Requests.Accounts.Commands
{
    public class SignUpAccountCommand : IRequest<SimpleResponse<Account>>
    {
        public long MemberId { get; set; }
    }
}
